using Microsoft.EntityFrameworkCore;

namespace Exercise6.Data;

public class ApplicationDatabaseContext : DbContext
{
    public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base(options) { }
}
