using Exercise9.API.DTO;
using Exercise9.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Exercise9.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripService;
    private readonly IClientTripService _clientTripService;

    public TripsController(ITripService tripService, IClientTripService clientTripService)
    {
        _tripService = tripService;
        _clientTripService = clientTripService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var trips = await _tripService.GetTripsAsync(page, pageSize);
        return Ok(trips);
    }

    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] AssignClientToTripDto assignClientToTripDto)
    {
        var (success, message) = await _clientTripService.AssignClientToTripAsync(idTrip, assignClientToTripDto);

        if (!success)
        {
            return BadRequest(new { Message = message });
        }

        return Ok(new { Message = message });
    }
}
