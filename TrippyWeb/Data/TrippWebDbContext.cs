using Microsoft.EntityFrameworkCore;
using TrippyWeb.Model;

namespace TrippyWeb.Data;

public class TrippyWebDbContext : DbContext
{
    public TrippyWebDbContext(DbContextOptions<TrippyWebDbContext> options) : base(options) { }
    public DbSet<Trip>? Trip { get; set; }
}
