#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrippyWeb.Data;
using TrippyWeb.Model;

namespace TrippyWeb.Pages.Trips
{
    public class DetailsModel : PageModel
    {
        private readonly TrippyWebDbContext _context;

        public DetailsModel(TrippyWebDbContext context)
        {
            _context = context;
        }

        public Trip Trip { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Trip = await _context.Trips.Include(t => t.Owner).FirstOrDefaultAsync(m => m.TripID == id);

            if (Trip == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
