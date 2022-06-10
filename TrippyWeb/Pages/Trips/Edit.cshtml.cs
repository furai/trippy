#nullable disable
using Microsoft.AspNetCore.Authorization;
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

    public EditModel(TrippyWebDbContext context)
    {
        _context = context;
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

        Trip = await _context.Trips.FirstOrDefaultAsync(m => m.TripID == id);

        if (Trip == null)
        {
            return NotFound();
        }

        if (Trip.Map != null)
        {
            MapImage = "data:image/png;base64," + Convert.ToBase64String(Trip.Map, 0, Trip.Map.Length);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
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

        return RedirectToPage("./Index");
    }

    private bool TripExists(int id)
    {
        return _context.Trips.Any(e => e.TripID == id);
    }
}
