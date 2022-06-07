using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrippyWeb.Model;

namespace TrippyWeb.Data;

public class TrippyWebDbContext : IdentityDbContext
{
    public TrippyWebDbContext(DbContextOptions<TrippyWebDbContext> options) : base(options) { }
    public DbSet<Trip>? Trips { get; set; }
    public DbSet<TrippyUser>? TrippyUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Trip>().ToTable("Trip");

        modelBuilder.Entity<TrippyUser>()
        .HasMany(tu => tu.OfferedTrips)
        .WithOne();

        modelBuilder.Entity<TrippyUser>()
        .HasOne(tu => tu.UsedTrip)
        .WithMany(t => t.Passengers)
        .HasForeignKey(tu => tu.TripId)
        .HasConstraintName("ForeignKey_TrippyUser_Trip")
        .isRequired();

        modelBuilder.Entity<Trip>()
        .HasMany(t => t.Passengers)
        .WithOne();

        modelBuilder.Entity<Trip>()
        .HasOne(t => t.Owner)
        .WithMany(tu => tu.OfferedTrips)
        .HasForeignKey(t => t.OwnerId)
        .HasConstraintName("ForeignKey_Trip_TrippyUser")
        .onDelete(DeleteBehavior.Cascade);

    }
}
