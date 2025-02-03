using LegacyApp.src.Models;

namespace LegacyApp.src.Repositories;
public class UserRepository : IUserRepository
{
    public void AddUser(User user)
    {
        UserDataAccess.AddUser(user);
    }
}