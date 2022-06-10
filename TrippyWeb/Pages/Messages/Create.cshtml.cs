#nullable disable
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrippyWeb.Data;
using TrippyWeb.Model;

namespace TrippyWeb.Pages.Messages;

[Authorize]
public class CreateModel : PageModel
{
    private readonly TrippyWebDbContext _context;
    private readonly ILogger<CreateModel> _logger;
    private readonly UserManager<TrippyUser> _userManager;

    public CreateModel(TrippyWebDbContext context, ILogger<CreateModel> logger, UserManager<TrippyUser> userManager)
    {
        _userManager = userManager;
        _context = context;
        _logger = logger;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Message Message { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        Message.UserName = user.Name;
        Message.CreatedDate = DateTime.Now;

        if (ModelState.IsValid || _context.Messages == null || Message == null)
        {
            _logger.LogInformation("Model is invalid.");
             _logger.LogInformation("model: " + !ModelState.IsValid);
                 _logger.LogInformation("messages: " + (_context.Messages == null));
                     _logger.LogInformation( "message" + (Message == null));

            return Page();
        }

        _context.Messages.Add(Message);
        await _context.SaveChangesAsync();
        TempData["success"] = "Message created successfully!";

        return RedirectToPage("./Index");
    }
}
