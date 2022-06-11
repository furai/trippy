#nullable disable
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrippyWeb.Data;
using TrippyWeb.Model;

namespace TrippyWeb.Pages.Trips
{
    public class IndexModel : PageModel
    {
        private readonly TrippyWebDbContext _context;

        public IndexModel(TrippyWebDbContext context)
        {
            _context = context;
        }

        public List<Trip> TripsList { get; set; }

        public async Task OnGetAsync(string filter)
        {
            TripsList = await _context.Trips.Include(t => t.Owner).Include(t => t.Passengers).ToListAsync();
            TripsList = filter switch
            {
                "Active" => TripsList.Where(t => (t.FreeSpots - t.Passengers.Count) > 0 && t.StartDate > DateTime.Now).ToList(),
                "Full" => TripsList.Where(t => (t.FreeSpots - t.Passengers.Count) < 1).ToList(),
                "New" => TripsList.Where(t => t.StartDate > DateTime.Now).ToList(),
                "Expired" => TripsList.Where(t => t.StartDate < DateTime.Now).ToList(),
                _ => TripsList
            };
        }
    }
}
