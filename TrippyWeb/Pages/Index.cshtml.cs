#nullable disable
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrippyWeb.Data;
using TrippyWeb.Model;

namespace TrippyWeb.Pages.Trips
{
    public class IndexModel : PageModel
    {
        private readonly TrippyWebDbContext _context;

        [BindProperty(SupportsGet = true)]
        [Display(Name = "Filters:")]
        public string Filter { get; set; }

        [BindProperty(SupportsGet = true)]
        [Display(Name = "NonSmoking:")]
        public string NonSmoking { get; set; }

        [BindProperty(SupportsGet = true)]
        [Display(Name = "Min Price:")]
        [Range(0.0, int.MaxValue, ErrorMessage = "Value for {0} must be a positive integer.")]
        public int Price { get; set; } = 0;

        public IndexModel(TrippyWebDbContext context)
        {
            _context = context;
        }

        public List<Trip> TripsList { get; set; }

        public async Task OnGetAsync()
        {
            TripsList = await _context.Trips.Include(t => t.Owner).Include(t => t.Passengers).Where(t => t.Price >= Price).ToListAsync();
            TripsList = Filter switch
            {
                "Active" => TripsList.Where(t => (t.FreeSpots - t.Passengers.Count) > 0 && t.StartDate > DateTime.Now).ToList(),
                "Full" => TripsList.Where(t => (t.FreeSpots - t.Passengers.Count) < 1).ToList(),
                "New" => TripsList.Where(t => t.StartDate > DateTime.Now).ToList(),
                "Expired" => TripsList.Where(t => t.StartDate < DateTime.Now).ToList(),
                _ => TripsList
            };

            TripsList = NonSmoking switch
            {
                "Yes" => TripsList.Where(t => t.NonSmoking == true).ToList(),
                "No" => TripsList.Where(t => t.NonSmoking == false).ToList(),
                _ => TripsList
            };
        }
    }
}
