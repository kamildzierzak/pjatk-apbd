
using Exercise5.Model;
using Microsoft.EntityFrameworkCore;

namespace Exercise5.Data;
public class ApplicationDatabaseContext : DbContext
{
    public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base(options) { }

    public DbSet<Animal> Animal { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>().HasData(
            new Animal { AnimalId = 1, Name = "Pies", Description = "Najlepszy pies na świecie!", Category = "Ssak", Area = "Świat" },
            new Animal { AnimalId = 2, Name = "Pingwin", Description = "Ptak nielot, ale pływa.", Category = "Ptak", Area = "Antarktyda" },
            new Animal { AnimalId = 3, Name = "Słoń", Description = "Duży z trąbą.", Category = "Ssak", Area = "Afryka, Azja" },
            new Animal { AnimalId = 4, Name = "Rekin", Description = "Szczęki trzy!", Category = "Ryba", Area = "Oceany" },
            new Animal { AnimalId = 5, Name = "Orzeł", Description = "Ptak co fruwa.", Category = "Ptak", Area = "Świat" }
            );
    }
}