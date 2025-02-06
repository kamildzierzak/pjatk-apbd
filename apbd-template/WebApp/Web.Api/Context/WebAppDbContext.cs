using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace Web.Api.Context;

public class WebAppDbContext: DbContext
{
    public WebAppDbContext(DbContextOptions<WebAppDbContext> options) : base(options) { }

    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Composite Primary Key for PrescriptionMedicament
        modelBuilder.Entity<PrescriptionMedicament>()
            .HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });

        base.OnModelCreating(modelBuilder);
    }
}
