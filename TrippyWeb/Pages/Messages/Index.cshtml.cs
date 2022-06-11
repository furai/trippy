#nullable disable
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrippyWeb.Data;
using TrippyWeb.Model;

namespace TrippyWeb.Pages.Messages
{
    public class IndexModel : PageModel
    {
        private readonly TrippyWebDbContext _context;

        public IndexModel(TrippyWebDbContext context)
        {
            _context = context;
        }

        public IList<Message> MessageList { get; set; } = new List<Message>();
        public int? TripID { get; set; }


        public async Task OnGetTripChatAsync(int? tripid)
        {
            if (tripid != null)
            {
                MessageList = await _context.Messages.Include(m => m.Trip).Where(m => m.TripID == tripid).ToListAsync();
                TripID = tripid;
            }
            
        }
    }
}
