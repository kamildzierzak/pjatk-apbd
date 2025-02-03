using Exercise7.Context;
using Exercise7.DTO;
using Exercise7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exercise7.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly LocaldbContext _context;

    public TripsController(LocaldbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips()
    {
        var trips = await _context.Trips
            .Include(t => t.IdCountries)
            .Include(t => t.ClientTrips)
            .OrderByDescending(t => t.DateFrom)
            .Select(t => new TripDTO
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                Countries = t.IdCountries.Select(c => new CountryDTO
                {
                    Name = c.Name,
                }).ToList(),
                Clients = t.ClientTrips.Select(ct => new ClientDTO
                {
                    FirstName = ct.IdClientNavigation.FirstName,
                    LastName = ct.IdClientNavigation.LastName
                }).ToList()

            })
            .ToListAsync();

        return Ok(trips);

    }


    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] AssignClientDto clientDto)
    {
        var trip = await _context.Trips.FindAsync(idTrip);

        if (trip == null)
        {
            return NotFound($"The trip with id {idTrip} not found.");
        }

        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Pesel == clientDto.Pesel);

        if (client == null)
        {
            client = new Client
            {
                FirstName = clientDto.FirstName,
                LastName = clientDto.LastName,
                Email = clientDto.Email,
                Telephone = clientDto.Telephone,
                Pesel = clientDto.Pesel,
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        var existingAssignment = await _context.ClientTrips.FirstOrDefaultAsync(ct => ct.IdClient == client.IdClient && ct.IdTrip == idTrip);

        if (existingAssignment != null)
        {
            return BadRequest("The client is already assigned to the trip.");
        }

        var clientTrip = new ClientTrip
        {
            IdClient = client.IdClient,
            IdTrip = idTrip,
            RegisteredAt = DateTime.UtcNow,
            PaymentDate = clientDto.PaymentDate
        };

        _context.ClientTrips.Add(clientTrip);
        await _context.SaveChangesAsync();

        return Ok("The client has been successfully assigned to the trip.");
    }
}