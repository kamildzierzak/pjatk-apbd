using LegacyApp.src.Models;

namespace LegacyApp.src.Repositories;
public interface IClientRepository
{
    Client GetById(int clientId);
}