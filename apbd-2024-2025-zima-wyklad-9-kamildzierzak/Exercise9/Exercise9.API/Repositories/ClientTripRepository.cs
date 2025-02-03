using Exercise9.API.Context;
using Exercise9.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Exercise9.API.Repositories;

public interface IClientTripRepository
{
    Task<bool> IsClientAssignedToTripAsync(int idClient, int idTrip);
    Task AssignClientToTripAsync(int idClient, int idTrip, DateTime registeredAt, DateTime? paymentTime);
}

public class ClientTripRepository : IClientTripRepository
{
    private readonly TestdbContext _context;

    public ClientTripRepository(TestdbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsClientAssignedToTripAsync(int idClient, int idTrip)
    {
        return await _context.ClientTrips.AnyAsync(ct => ct.IdClient == idClient && ct.IdTrip == idTrip);
    }

    public async Task AssignClientToTripAsync(int idClient, int idTrip, DateTime registeredAt, DateTime? paymentTime)
    {
        var clientTrip = new ClientTrip
        {
            IdClient = idClient,
            IdTrip = idTrip,
            RegisteredAt = registeredAt,
            PaymentDate = paymentTime
        };

        _context.ClientTrips.Add(clientTrip);
        await _context.SaveChangesAsync();
    }
}
