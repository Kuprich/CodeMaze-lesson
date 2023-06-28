using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Repository;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
    { }

    public DbSet<Company>? Companies { get; set; }
    public DbSet<Employee>? Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) => 
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

}