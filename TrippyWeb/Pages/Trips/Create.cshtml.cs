#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrippyWeb.Data;
using TrippyWeb.Model;

namespace TrippyWeb.Pages.Trips
{
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

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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
}
