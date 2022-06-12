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

    public async Task<IActionResult> OnPostAsync(int? tripid)
    {
        _logger.LogInformation("trip id: " + tripid);
        if (tripid == null)
        {
            TempData["error"] = "Error occured trip id is NULL!";
            return RedirectToPage("../Trips/Index");
        }

        var user = await _userManager.GetUserAsync(User);

        Message.UserName = user.Name;
        Message.CreatedDate = DateTime.Now;

        var trip = _context.Trips.Where(t => t.TripID == tripid).First();
        Message.Trip = trip;

        Message.TripID = (int)tripid;

        _context.Messages.Add(Message);
        await _context.SaveChangesAsync();
        TempData["success"] = "Message created successfully!";

        return RedirectToPage("./Index", new {tripid = tripid});
    }

}
