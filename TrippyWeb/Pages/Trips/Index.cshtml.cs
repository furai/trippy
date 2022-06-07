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

        public IList<Trip> TripsList { get; set; }

        public async Task OnGetAsync()
        {
            TripsList = await _context.Trips.Include(t => t.Owner).ToListAsync();
        }
    }
}
