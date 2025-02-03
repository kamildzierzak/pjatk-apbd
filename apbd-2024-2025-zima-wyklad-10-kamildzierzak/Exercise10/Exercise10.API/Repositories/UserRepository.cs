using Exercise10.API.Context;
using Exercise10.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Exercise10.API.Repositories;

public interface IUserRepository
{
    Task<bool> UserExistsAsync(string login);
    Task CreateUserAsync(User user);
    Task<User> GetUserAsync(string login);
    Task<User> GetUserByRefreshTokenAsync(string refreshToken);
    Task UpdateUserAsync(User user);
}

public class UserRepository : IUserRepository
{
    private readonly TestdbContext _context;

    public UserRepository(TestdbContext context)
    {
        _context = context;
    }

    public async Task<bool> UserExistsAsync(string login)
    {
        return await _context.Users.AnyAsync(u => u.Login == login);
    }

    public async Task CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User> GetUserAsync(string login)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
    }

    public async Task<User> GetUserByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

}
