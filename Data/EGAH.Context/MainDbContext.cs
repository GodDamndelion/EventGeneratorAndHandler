namespace EGAH.Context;

using EGAH.Context.Entities;
using Microsoft.EntityFrameworkCore;

public class MainDbContext : DbContext
{
    public DbSet<Incident> Incidents { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


    }
}
