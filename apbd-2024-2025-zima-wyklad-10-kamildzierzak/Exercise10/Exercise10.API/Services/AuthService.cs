using Exercise10.API.DTO;
using Exercise10.API.Helpers;
using Exercise10.API.Models;
using Exercise10.API.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Exercise10.API.Services;

public interface IAuthService
{
    Task<(bool Success, string Message)> RegisterUserAsync(RegisterUserDto registerUserDto);
    Task<(bool Success, string Message, string? AccessToken, string? RefreshToken)> LoginUserAsync(LoginUserDto loginUserDto);
    Task<(bool Success, string Message, string? AccessToken, string? RefreshToken)> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto);
}

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<(bool Success, string Message, string? AccessToken, string? RefreshToken)> LoginUserAsync(LoginUserDto loginUserDto)
    {
        var user = await _userRepository.GetUserAsync(loginUserDto.Login);
        if (user == null) return (false, "Invalid login credentials.", null, null);

        var hashedPassword = AuthHelpers.GetHashedPasswordWithSalt(loginUserDto.Password, user.Salt);
        if (hashedPassword != user.PasswordHash)
            return (false, "Invalid login credentials.", null, null);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.Role, "admin"),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var accessToken = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
            );

        var refreshToken = AuthHelpers.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);
        await _userRepository.UpdateUserAsync(user);

        return (true, "User logged in successfully.", new JwtSecurityTokenHandler().WriteToken(accessToken), user.RefreshToken);
    }

    public async Task<(bool Success, string Message)> RegisterUserAsync(RegisterUserDto registerUserDto)
    {
        if (await _userRepository.UserExistsAsync(registerUserDto.Login))
        {
            return (false, "User already exists.");
        }

        var (passwordHash, salt) = AuthHelpers.GetHashedPasswordAndSalt(registerUserDto.Password);

        var user = new User
        {
            Login = registerUserDto.Login,
            PasswordHash = passwordHash,
            Salt = salt,
            RefreshToken = AuthHelpers.GenerateRefreshToken(),
            RefreshTokenExpiryTime = DateTime.Now.AddDays(1)
        };

        await _userRepository.CreateUserAsync(user);
        return (true, "User registered successfully.");
    }

    public async Task<(bool Success, string Message, string? AccessToken, string? RefreshToken)> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto)
    {
        var user = await _userRepository.GetUserByRefreshTokenAsync(refreshTokenRequestDto.RefreshToken);

        if (user == null)
        {
            return (false, "Invalid or expired refresh token.", null, null);
        }

        if (user.RefreshTokenExpiryTime < DateTime.Now)
        {
            return (false, "Refresh token has expired.", null, null);
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.Role, "admin"),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var accessToken = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        var refreshToken = AuthHelpers.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);
        await _userRepository.UpdateUserAsync(user);

        return (true, "Tokens refreshed successfully.", new JwtSecurityTokenHandler().WriteToken(accessToken), refreshToken);
    }
}
