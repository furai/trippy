#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrippyWeb.Data;
using TrippyWeb.Model;

namespace TrippyWeb.Pages.Trips;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly TrippyWebDbContext _context;

    public DeleteModel(TrippyWebDbContext context)
    {
        _context = context;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Trip = await _context.Trips.FindAsync(id);

        if (Trip != null)
        {
            _context.Trips.Remove(Trip);
            await _context.SaveChangesAsync();
            TempData["success"] = "Trip deleted successfully!";
        }

        return RedirectToPage("../Index");
    }
}
