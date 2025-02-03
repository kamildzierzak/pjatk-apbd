using Exercise10.API.Context;
using Exercise10.API.DTO;
using Exercise10.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Exercise10.API.Repositories;

public interface ITripRepository
{
    Task<PaginatedResult<Trip>> GetTripsAsync(int page, int pageSize);
    Task<Trip> GetTripByIdAsync(int idTrip);
}

public class TripRepository : ITripRepository
{
    private readonly TestdbContext _context;

    public TripRepository(TestdbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedResult<Trip>> GetTripsAsync(int page, int pageSize)
    {
        var query = _context.Trips
            .OrderByDescending(t => t.DateFrom)
            .Include(t => t.IdCountries)
            .Include(t => t.ClientTrips)
                .ThenInclude(ct => ct.IdClientNavigation)
            .OrderByDescending(t => t.DateFrom);

        var totalItems = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedResult<Trip>
        {
            Page = page,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling((double)totalItems / pageSize),
            Items = items
        };
    }

    public async Task<Trip> GetTripByIdAsync(int idTrip)
    {
        return await _context.Trips.FindAsync(idTrip);
    }
}
