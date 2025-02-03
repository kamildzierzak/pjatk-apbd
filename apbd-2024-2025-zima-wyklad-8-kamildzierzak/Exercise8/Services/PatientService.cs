using Exercise8.Context;
using Exercise8.DTO;
using Microsoft.EntityFrameworkCore;

namespace Exercise8.Services;

public interface IPatientService
{
    Task<PatientDto> GetPatientDataAsync(int id);
}

public class PatientService : IPatientService
{
    private readonly LocaldbContext _context;

    public PatientService(LocaldbContext localdbContext)
    {
        _context = localdbContext;
    }

    public async Task<PatientDto> GetPatientDataAsync(int id)
    {
        var patient = await _context.Patients
            .Include(p => p.Prescriptions)
                .ThenInclude(p => p.PrescriptionMedicaments)
                .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
                .ThenInclude(p => p.Doctor)
            .Where(p => p.IdPatient == id)
            .FirstOrDefaultAsync();

        if (patient == null) return null;

        return new PatientDto
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthdate = patient.Birthdate,
            Prescriptions = patient.Prescriptions
                .OrderBy(prescription => prescription.DueDate)
                .Select(prescription => new PrescriptionDto
                {
                    IdPrescription = prescription.IdPrescription,
                    Date = prescription.Date,
                    DueDate = prescription.DueDate,
                    Medicaments = prescription.PrescriptionMedicaments.Select(pm => new MedicamentDto
                    {
                        IdMedicament = pm.Medicament.IdMedicament,
                        Name = pm.Medicament.Name,
                        Dose = pm.Dose,
                        Description = pm.Details,
                    }),
                    Doctor = new DoctorDto
                    {
                        IdDoctor = prescription.Doctor.IdDoctor,
                        FirstName = prescription.Doctor.FirstName,
                        LastName = prescription.Doctor.LastName,
                    }
                })
        };
    }
}
