using Exercise8.Services;
using Microsoft.AspNetCore.Mvc;

namespace Exercise8.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientData(int id)
    {
        var patientData = await _patientService.GetPatientDataAsync(id);

        if (patientData == null)
        {
            return NotFound($"Patient with id {id} not found.");
        }

        return Ok(patientData);
    }
}
