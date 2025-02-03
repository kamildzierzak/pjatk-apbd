using Exercise8.Models;
using Microsoft.EntityFrameworkCore;

namespace Exercise8.Context;

public class LocaldbContext : DbContext
{
    public LocaldbContext(DbContextOptions options) : base(options)
    {
    }

    protected LocaldbContext()
    {
    }

    public DbSet<Medicament> Medicaments { get; set; }

    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    public DbSet<Prescription> Prescriptions { get; set; }

    public DbSet<Patient> Patients { get; set; }

    public DbSet<Doctor> Doctors { get; set; }

}