#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrippyWeb.Model;

namespace TrippyWeb.Pages.Trips;

public class CreateModel : PageModel
{
    private readonly TrippyWeb.Data.TrippyWebDbContext _context;

    public CreateModel(TrippyWeb.Data.TrippyWebDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();

    }

    [BindProperty]
    public Trip Trip { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Trip.Add(Trip);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
