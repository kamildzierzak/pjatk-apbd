using Exercise8.DTO;
using Exercise8.Services;
using Microsoft.AspNetCore.Mvc;

namespace Exercise8.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionsController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrescription([FromBody] PrescriptionRequestDto request)
    {
        try
        {
            await _prescriptionService.CreatePrescriptionAsync(request);
            return Ok("Prescription created successfully.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
