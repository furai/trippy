#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrippyWeb.Data;
using TrippyWeb.Model;


namespace TrippyWeb.Pages.Trips
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly TrippyWebDbContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(TrippyWebDbContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
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
                _logger.LogInformation("Model is invalid.");

                return Page();
            }

            _context.Trips.Add(Trip);
            await _context.SaveChangesAsync();
            TempData["success"] = "Trip created successfully!";

            return RedirectToPage("./Index");
        }
    }
}
