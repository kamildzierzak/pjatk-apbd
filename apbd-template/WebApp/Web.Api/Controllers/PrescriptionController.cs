using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Api.Context;
using WebApp.Dto;
using WebApp.Models;

namespace WebApp.Controllers;

[Route("api/prescriptions")]
[ApiController]
public class PrescriptionController : ControllerBase
{
    private readonly WebAppDbContext _context;

    public PrescriptionController(WebAppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrescription([FromBody] NewPrescriptionRequest request)
    {
        if (request == null)
            return BadRequest("Invalid request data");

        if (request.DueDate < request.Date)
            return BadRequest("DueDate must be greater than or equal to Date");

        if (request.Medicaments.Count > 10)
            return BadRequest("A prescription cannot have more than 10 medicaments");

        // Check if the doctor exists
        var doctor = await _context.Doctors.FindAsync(request.IdDoctor);
        if (doctor == null)
            return NotFound("Doctor not found");

        // Check if the patient exists, if not, create one
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.IdPatient == request.Patient.IdPatient);
        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                Birthdate = request.Patient.Birthdate
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        // Create the prescription
        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = request.IdDoctor
        };
        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        // Check if all medicaments exist
        var medicamentIds = request.Medicaments.Select(m => m.IdMedicament).ToList();
        var existingMedicaments = await _context.Medicaments
            .Where(m => medicamentIds.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync();

        if (existingMedicaments.Count != request.Medicaments.Count)
        {
            return BadRequest("One or more medicaments do not exist");
        }

        // Add prescription-medicament relationships
        foreach (var medicament in request.Medicaments)
        {
            _context.PrescriptionMedicaments.Add(new PrescriptionMedicament
            {
                IdPrescription = prescription.IdPrescription,
                IdMedicament = medicament.IdMedicament,
                Dose = medicament.Dose,
                Details = medicament.Details
            });
        }

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPrescription), new { id = prescription.IdPrescription }, prescription);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Prescription>> GetPrescription(int id)
    {
        var prescription = await _context.Prescriptions
            .Include(p => p.Patient)
            .Include(p => p.Doctor)
            .Include(p => p.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .FirstOrDefaultAsync(p => p.IdPrescription == id);

        if (prescription == null)
            return NotFound();

        return prescription;
    }
}