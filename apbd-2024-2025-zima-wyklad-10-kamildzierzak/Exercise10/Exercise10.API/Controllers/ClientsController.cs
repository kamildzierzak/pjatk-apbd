using Exercise10.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Exercise10.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpDelete("{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        var (success, message) = await _clientService.DeleteClientAsync(idClient);

        if (!success) return BadRequest(new { Message = message });

        return Ok(new { Message = message });
    }
}
