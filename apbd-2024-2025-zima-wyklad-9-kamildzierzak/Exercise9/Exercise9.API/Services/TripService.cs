using Exercise9.API.DTO;
using Exercise9.API.Repositories;

namespace Exercise9.API.Services;

public interface ITripService
{
    Task<PaginatedResult<TripDto>> GetTripsAsync(int page, int pageSize);
}

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;

    public TripService(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async Task<PaginatedResult<TripDto>> GetTripsAsync(int page, int pageSize)
    {
        var paginatedTrips = await _tripRepository.GetTripsAsync(page, pageSize);

        return new PaginatedResult<TripDto>
        {
            Page = paginatedTrips.Page,
            PageSize = paginatedTrips.PageSize,
            TotalPages = paginatedTrips.TotalPages,
            Items = paginatedTrips.Items.Select(t => new TripDto
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                Countries = t.IdCountries.Select(c => new CountryDto
                {
                    Name = c.Name,
                }).ToList(),
                Clients = t.ClientTrips.Select(ct => new ClientDto
                {
                    FirstName = ct.IdClientNavigation.FirstName,
                    LastName = ct.IdClientNavigation.LastName,
                }).ToList()
            }).ToList()
        };
    }
}
