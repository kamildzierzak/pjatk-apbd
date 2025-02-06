using Microsoft.EntityFrameworkCore;
using Web.Api.Context;
using WebApp.Models;

namespace Web.Api.Data;

public static class DbInitializer
{
    public static void SeedData(WebAppDbContext context)
    {
        // Ensure database is created
        context.Database.Migrate();

        // Check if data already exists
        if (context.Doctors.Any() || context.Patients.Any() || context.Medicaments.Any())
        {
            return; // Database has already been seeded
        }

        // Seed Doctors
        var doctors = new[]
        {
            new Doctor { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
            new Doctor { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" }
        };
        context.Doctors.AddRange(doctors);

        // Seed Patients
        var patients = new[]
        {
            new Patient { FirstName = "Alice", LastName = "Brown", Birthdate = new DateTime(1990, 5, 14) },
            new Patient { FirstName = "Bob", LastName = "Johnson", Birthdate = new DateTime(1985, 9, 23) }
        };
        context.Patients.AddRange(patients);

        // Seed Medicaments
        var medicaments = new[]
        {
            new Medicament { Name = "Paracetamol", Description = "Pain relief", Type = "Tablet" },
            new Medicament { Name = "Ibuprofen", Description = "Anti-inflammatory", Type = "Capsule" }
        };
        context.Medicaments.AddRange(medicaments);

        // Save changes
        context.SaveChanges();

        // Seed Prescriptions
        var prescriptions = new[]
        {
            new Prescription { Date = DateTime.Now, DueDate = DateTime.Now.AddDays(30), IdPatient = 1, IdDoctor = 1 },
            new Prescription { Date = DateTime.Now, DueDate = DateTime.Now.AddDays(30), IdPatient = 2, IdDoctor = 2 }
        };
        context.Prescriptions.AddRange(prescriptions);
        context.SaveChanges();

        // Seed Prescription_Medicament (Many-to-Many)
        var prescriptionMedicaments = new[]
        {
            new PrescriptionMedicament { IdMedicament = 1, IdPrescription = 1, Dose = 500, Details = "Take twice a day" },
            new PrescriptionMedicament { IdMedicament = 2, IdPrescription = 2, Dose = 200, Details = "Take after meals" }
        };
        context.PrescriptionMedicaments.AddRange(prescriptionMedicaments);
        context.SaveChanges();
    }
}
