using Microsoft.EntityFrameworkCore;
using TrippyWeb.Model;

namespace TrippyWeb.Data;

public class TrippWebDbContext : DbContext
{
    public TrippWebDbContext(DbContextOptions<TrippWebDbContext> options) : base(options) { }
    public DbSet<Trip>? Trip { get; set; }
}
