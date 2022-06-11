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
public class EditModel : PageModel
{
    private readonly TrippyWebDbContext _context;
    private readonly UserManager<TrippyUser> _userManager;

    public EditModel(TrippyWebDbContext context, UserManager<TrippyUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [BindProperty]
    public Trip Trip { get; set; }


    [BindProperty]
    public CreateModel.BufferedFileUpload FileUpload { get; set; }

    public string MapImage { get; set; }

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

        if (Trip.Map != null)
        {
            MapImage = "data:image/png;base64," + Convert.ToBase64String(Trip.Map, 0, Trip.Map.Length);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            var userid = await _userManager.GetUserIdAsync(user);
            if (userid != Trip.OwnerID)
            {
                TempData["success"] = "Can't edit. You're not owner of that trip.";
                return RedirectToPage("../Index");
            }
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (FileUpload.FormFile != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await FileUpload.FormFile.CopyToAsync(memoryStream);

                if (memoryStream.Length < 2097152)
                {
                    Trip.Map = memoryStream.ToArray();
                }
                else
                {
                    ModelState.AddModelError("FileUpload.FormFile", "The file is too large. Max size is 2 MB.");
                }
            }
        }

        if (!ModelState.IsValid || _context.Trips == null || Trip == null)
        {
            return Page();
        }

        _context.Attach(Trip).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            TempData["success"] = "Trip updated successfully!";
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TripExists(Trip.TripID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("../Index");
    }

    public async Task<IActionResult> OnGetDeleteImageAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Trip = await _context.Trips.FirstOrDefaultAsync(m => m.TripID == id);

        if (Trip == null)
        {
            return NotFound();
        }

        if (Trip.Map != null)
        {
            Trip.Map = null;
            await _context.SaveChangesAsync();
        }

        return Page();
    }

    private bool TripExists(int id)
    {
        return _context.Trips.Any(e => e.TripID == id);
    }
}
