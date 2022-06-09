#nullable disable
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
