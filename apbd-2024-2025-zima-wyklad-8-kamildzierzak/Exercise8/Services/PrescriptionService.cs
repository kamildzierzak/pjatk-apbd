using Exercise8.Context;
using Exercise8.DTO;
using Exercise8.Models;
using Microsoft.EntityFrameworkCore;

namespace Exercise8.Services;

public interface IPrescriptionService
{
    Task<bool> CreatePrescriptionAsync(PrescriptionRequestDto request);
}

public class PrescriptionService : IPrescriptionService
{
    private readonly LocaldbContext _context;

    public PrescriptionService(LocaldbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreatePrescriptionAsync(PrescriptionRequestDto request)
    {
        // Validate DueDate >= Date
        if (request.DueDate < request.Date)
        {
            throw new ArgumentException("DueDate cannot be earlier than Date.");
        }

        // Validate medicament count <= 10
        if (request.Medicaments.Count > 10)
        {
            throw new ArgumentException("A prescription cannot contain more than 10 medicaments.");
        }

        // Check if the patient exists and add new one if necessary
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

        // Check if the doctor exists
        var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.IdDoctor == request.Doctor.IdDoctor);

        if (doctor == null)
        {
            throw new ArgumentException("Doctor with the given id does not exist.");
        }

        // Check if all medicaments exists
        var medicamentIds = request.Medicaments.Select(m => m.IdMedicament).ToList();
        var existingMedicaments = await _context.Medicaments.Where(m => medicamentIds.Contains(m.IdMedicament)).ToListAsync();

        if (existingMedicaments.Count != medicamentIds.Count)
        {
            throw new ArgumentException("One or more medicaments don't exists.");
        }


        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = doctor.IdDoctor,
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        foreach (var medicamentDto in request.Medicaments)
        {
            _context.PrescriptionMedicaments.Add(
                new PrescriptionMedicament
                {
                    IdPrescription = prescription.IdPrescription,
                    IdMedicament = medicamentDto.IdMedicament,
                    Dose = medicamentDto.Dose,
                    Details = medicamentDto.Description,
                }
                );
        }

        await _context.SaveChangesAsync();

        return true;
    }
}
