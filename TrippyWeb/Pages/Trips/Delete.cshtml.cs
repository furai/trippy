#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    private readonly UserManager<TrippyUser> _userManager;

    public DeleteModel(TrippyWebDbContext context, UserManager<TrippyUser> userManager)
    {
        _context = context;
        _userManager = userManager;
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

        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            var userid = await _userManager.GetUserIdAsync(user);
            if (userid != Trip.OwnerID)
            {
                TempData["success"] = "Can't delete. You're not owner of that trip.";
                return RedirectToPage("../Index");
            }
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
