using Exercise10.API.Context;
using Exercise10.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Exercise10.API.Repositories;

public interface IClientRepository
{
    Task<bool> HasTripsAsync(int idClient);
    Task<bool> DeleteClientAsync(int idClient);
    Task<bool> ClientExistsAsync(string pesel);
    Task<Client> GetClientByPeselAsync(string pesel);
    Task<int> AddClientAsync(Client client);
}

public class ClientRepository : IClientRepository
{
    private readonly TestdbContext _context;

    public ClientRepository(TestdbContext context)
    {
        _context = context;
    }

    public async Task<bool> HasTripsAsync(int idClient)
    {
        return await _context.ClientTrips.AnyAsync(c => c.IdClient == idClient);
    }

    public async Task<bool> DeleteClientAsync(int idClient)
    {
        var client = await _context.Clients.FindAsync(idClient);

        if (client == null) return false;

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ClientExistsAsync(string pesel)
    {
        return await _context.Clients.AnyAsync(c => c.Pesel == pesel);
    }

    public async Task<Client> GetClientByPeselAsync(string pesel)
    {
        return await _context.Clients.FirstOrDefaultAsync(c => c.Pesel == pesel);
    }

    public async Task<int> AddClientAsync(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        return client.IdClient;
    }
}
