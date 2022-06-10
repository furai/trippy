#nullable disable
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrippyWeb.Data;
using TrippyWeb.Model;

namespace TrippyWeb.Pages.Trips;

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
    public Trip Trip { get; set; } = default!;

    [BindProperty]
    public BufferedFileUpload FileUpload { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        var username = await _userManager.GetUserNameAsync(user);

        Trip.Owner = user;

        if (FileUpload.FormFile != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await FileUpload.FormFile.CopyToAsync(memoryStream);

                if (memoryStream.Length < 2097152)
                {
                    Trip.Map = memoryStream.ToArray();

                    await _context.SaveChangesAsync();
                }
                else
                {
                    ModelState.AddModelError("FileUpload.FormFile", "The file is too large. Max size is 2 MB.");
                }
            }
        }

        var enddate = Trip.StartDate.AddMinutes(Trip.DurationInMinutes);
        var overlappingTripsOwner = _context.Trips
            .Where(t => t.Owner == user)
            .Where(t => t.StartDate < enddate && t.EndDate > Trip.StartDate).ToList();

        if (overlappingTripsOwner.Count > 0)
        {
            ModelState.AddModelError("Trip.StartDate", "You have created another trip that overlaps with this one.");
            ModelState.AddModelError("Trip.DurationInMinutes", "You have created another trip that overlaps with this one.");
        }

        var overlappingTripsJoined = _context.Trips
            .Include(t => t.Passengers)
            .Where(
                t => t.Passengers.Any(p => p.UserName.Equals(username))
            ).Where(t => t.StartDate < enddate && t.EndDate > Trip.StartDate).ToList();

        if (overlappingTripsJoined.Count > 0)
        {
            ModelState.AddModelError("Trip.StartDate", "You're taking part in another trip during that time.");
            ModelState.AddModelError("Trip.DurationInMinutes", "You're taking part in another trip during that time.");
        }

        if (!ModelState.IsValid || _context.Trips == null || Trip == null)
        {
            _logger.LogInformation("Model is invalid.");

            return Page();
        }

        _context.Trips.Add(Trip);
        await _context.SaveChangesAsync();
        TempData["success"] = "Trip created successfully!";

        return RedirectToPage("./Index");
    }

    public class BufferedFileUpload
    {
        [Display(Name = "Trip Image")]
        public IFormFile FormFile { get; set; }
    }
}
