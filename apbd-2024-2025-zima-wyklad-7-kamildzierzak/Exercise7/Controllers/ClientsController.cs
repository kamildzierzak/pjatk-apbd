using Exercise7.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{

    private readonly LocaldbContext _context;

    public ClientsController(LocaldbContext context)
    {
        _context = context;
    }

    [HttpDelete("{idClient:int}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        var client = await _context.Clients.FindAsync(idClient);

        if (client == null)
        {
            return NotFound($"Client with id {idClient} not found.");
        }

        bool hasTrips = await _context.ClientTrips.AnyAsync(ct => ct.IdClient == idClient);

        if (hasTrips)
        {
            return BadRequest("The client cannot be deleted because he has trips assigned.");
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();

        return Ok("The client has been deleted");
    }

}