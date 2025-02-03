using Exercise10.API.DTO;
using Exercise10.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exercise10.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
    {
        var (success, message) = await _authService.RegisterUserAsync(registerUserDto);

        if (!success) return BadRequest(new { Message = message });

        return Ok(new { Message = message });
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
    {
        var result = await _authService.LoginUserAsync(loginUserDto);

        if (!result.Success) return Unauthorized(new { result.Message });

        return Ok(new
        {
            result.AccessToken,
            result.RefreshToken
        });
    }

    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto refreshTokenRequestDto)
    {
        var result = await _authService.RefreshTokenAsync(refreshTokenRequestDto);

        if (!result.Success) return BadRequest(new { result.Message });

        return Ok(new
        {
            result.AccessToken,
            result.RefreshToken
        });
    }
}
