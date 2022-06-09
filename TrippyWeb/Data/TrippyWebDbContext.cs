using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrippyWeb.Model;

namespace TrippyWeb.Data;

public class TrippyWebDbContext : IdentityDbContext
{
    public TrippyWebDbContext(DbContextOptions<TrippyWebDbContext> options) : base(options) { }
    public DbSet<Trip>? Trips { get; set; }
    public DbSet<TrippyUser>? TrippyUsers { get; set; }
    public DbSet<Stop>? Stops { get; set; }
    public DbSet<Review>? Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Trip>()
            .Property(t => t.EndDate)
            .HasComputedColumnSql("DATE_ADD(StartDate, INTERVAL DurationInMinutes MINUTE)", stored: true);

        modelBuilder.Entity<Message>()
            .Property(m => m.CreatedDate)
            .HasDefaultValueSql("now()");

        modelBuilder.Entity<Review>()
            .Property(r => r.CreatedDate)
            .HasDefaultValueSql("now()");
    }
}
