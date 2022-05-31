using Microsoft.EntityFrameworkCore;
using TrippyWeb.Model;

namespace TrippyWeb.Data;

public class TrippyWebDbContext : DbContext
{
    public TrippyWebDbContext(DbContextOptions<TrippyWebDbContext> options) : base(options) { }
    public DbSet<Trip>? Trips { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Trip>().ToTable("Trip");
    }
}
