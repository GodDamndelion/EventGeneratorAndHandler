namespace EGAH.Context;

using EGAH.Context.Entities;
using Microsoft.EntityFrameworkCore;

public class MainDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Incident> Incidents { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Incident>().Property(i => i.FirstEventId).IsRequired();
        modelBuilder.Entity<Incident>().HasOne(i => i.FirstEvent).WithOne(e => e.Incident).HasForeignKey<Incident>(i => i.FirstEventId).OnDelete(DeleteBehavior.NoAction);
        //modelBuilder.Entity<Incident>().HasOne(i => i.SecondEvent).WithOne(e => e.Incident).HasForeignKey<Incident>(i => i.SecondEventId).OnDelete(DeleteBehavior.NoAction); // Невозможно из-за ограничения бд... Но без этого всё равно все со свем разобрались
    }
}
