#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrippyWeb.Data;
using TrippyWeb.Model;

namespace TrippyWeb.Pages.Messages;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly TrippyWebDbContext _context;

    public DeleteModel(TrippyWebDbContext context)
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

        Message = await _context.Messages.FirstOrDefaultAsync(m => m.MessageID == id);

        if (Message == null)
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

        Message = await _context.Messages.FindAsync(id);

        if (Message != null)
        {
            _context.Messages.Remove(Message);
            await _context.SaveChangesAsync();
            TempData["success"] = "Message deleted successfully!";
        }

        return RedirectToPage("./Index");
    }
}
