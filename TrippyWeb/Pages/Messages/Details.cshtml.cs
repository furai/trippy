#nullable disable
using GemBox.Document;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrippyWeb.Data;
using TrippyWeb.Model;
using TrippyWeb.Services;

namespace TrippyWeb.Pages.Messages
{
    public class DetailsModel : PageModel
    {
        private readonly TrippyWebDbContext _context;
        private readonly ITripService _tripService;
        private readonly UserManager<TrippyUser> _userManager;

        public DetailsModel(TrippyWebDbContext context, UserManager<TrippyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public TrippyUser TrippyUser { get; set; }
        public Message Message { get; set; }
        public string UserName { get; set; }
        public bool IsPassenger { get; set; }

        public async Task<IActionResult> OnGetAsync(int? tripid)
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? messageId)
        {
            if (messageId == null)
            {
                return Page();
            }

            return Page();
        }
    }
}
