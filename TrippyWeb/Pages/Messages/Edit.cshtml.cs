#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrippyWeb.Data;
using TrippyWeb.Model;

namespace TrippyWeb.Pages.Messages;

[Authorize]
public class EditModel : PageModel
{
    private readonly TrippyWebDbContext _context;

    public EditModel(TrippyWebDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Message Message { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
       
        return RedirectToPage("./Index");
    }

}
