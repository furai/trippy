#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrippyWeb.Data;
using TrippyWeb.Model;

namespace TrippyWeb.Pages.Trips
{
    public class CreateModel : PageModel
    {
        private readonly TrippyWebDbContext _context;

        public CreateModel(TrippyWebDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Trip Trip { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Trips.Add(Trip);
            await _context.SaveChangesAsync();
            TempData["success"] = "Trip created successfully!";

            return RedirectToPage("./Index");
        }
    }
}
