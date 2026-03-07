using Microsoft.EntityFrameworkCore;

namespace Aspnet_Crud;

public class CompanyContext : DbContext
{
    public DbSet<Employee> employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optBuilder)
    {
        string? user = Environment.GetEnvironmentVariable("USER");
        string? password = Environment.GetEnvironmentVariable("PSQL_PASS");

        optBuilder.UseNpgsql($"Host=localhost;Username={user};Password={password};Database=company");
    }
}
