#nullable disable
using GemBox.Document;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrippyWeb.Data;
using TrippyWeb.Model;
using TrippyWeb.Services;

namespace TrippyWeb.Pages.Trips
{
    public class DetailsModel : PageModel
    {
        private readonly TrippyWebDbContext _context;
        private readonly ITripService _tripService;
        private readonly UserManager<TrippyUser> _userManager;

        public DetailsModel(TrippyWebDbContext context, ITripService tripService, UserManager<TrippyUser> userManager)
        {
            _context = context;
            _tripService = tripService;
            _userManager = userManager;
        }

        public TrippyUser TrippyUser { get; set; }
        public Trip Trip { get; set; }
        public string MapImage { get; set; }
        public FileResult PDF { get; set; }

        public async Task<IActionResult> OnGetAsync(int? tripid)
        {
            if (tripid == null)
            {
                return NotFound();
            }

            Trip = await _context.Trips.Include(t => t.Owner).Include(t => t.Passengers).FirstOrDefaultAsync(m => m.TripID == tripid);

            if (Trip == null)
            {
                return NotFound();
            }

            if (Trip.Map != null)
            {
                MapImage = "data:image/png;base64," + Convert.ToBase64String(Trip.Map, 0, Trip.Map.Length);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDownloadPDFAsync(int? tripid)
        {
            if (tripid == null)
            {
                return NotFound();
            }

            Trip = await _context.Trips.Include(t => t.Owner).FirstOrDefaultAsync(m => m.TripID == tripid);

            if (Trip == null)
            {
                return NotFound();
            }

            if (Trip.Map != null)
            {
                MapImage = "data:image/png;base64," + Convert.ToBase64String(Trip.Map, 0, Trip.Map.Length);
            }

            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            var html = $@"
<html>
<style>
  @page {{
    size: A4 portrait;
    margin: 1cm 1cm 1cm;
    mso-header-margin: 1cm;
    mso-footer-margin: 1cm;
  }}

  body {{
    background: #FFFFFF;
    border: 1pt solid black;
    padding: 20pt;
  }}

  br {{
    page-break-before: always;
  }}

  p {{ margin: 0; }}
  header {{ color: #000000; text-align: center; font-weight: 900; }}
  .img-wrapper {{
    text-align: center;
    max-width: 100%;
  }}

  dt {{
    font-weight: 900;
  }}
</style>

<body>
  <header>
    <h1>Trippy</h1>
    <h2>Trip Plan</h2>
  </header>
  <main>
    {(MapImage != null ? $"<div class=\"img-wrapper\"><img height=\"300px\" src=\"{MapImage}\" ></div>" : "")}
    <dl>
        <dt>
           Beginning
        </dt>
        <dd>
            {Trip.Beginning}
        </dd>
        <dt>
            Destination
        </dt>
        <dd>
            {Trip.Destination}
        </dd>
        <dt>
           Duration In Minutes
        </dt>
        <dd>
            {Trip.DurationInMinutes}
        </dd>
        <dt>
           NonSmoking
        </dt>
        <dd>
           {(Trip.NonSmoking ? "true" : "false")}
        </dd>
        <dt>
            Start Date
        </dt>
        <dd>
            {Trip.StartDate}
        </dd>
        <dt>
            End Date
        </dt>
        <dd>
            {Trip.EndDate}
        </dd>
        <dt>
            Owner
        </dt>
        <dd>
            {Trip.Owner.Name}
        </dd>
        <dt>
            Price
        </dt>
        <dd>
            {Trip.Price} PLN
        </dd>
    </dl>
  </main>
</body>
</html>";

            var htmlLoadOptions = new HtmlLoadOptions();
            using (var htmlStream = new MemoryStream(htmlLoadOptions.Encoding.GetBytes(html)))
            {
                var document = DocumentModel.Load(htmlStream, htmlLoadOptions);

                using (MemoryStream stream = new MemoryStream())
                {
                    StreamWriter objstreamwriter = new StreamWriter(stream);
                    document.Save(stream, SaveOptions.PdfDefault);
                    objstreamwriter.Flush();
                    objstreamwriter.Close();
                    return File(stream.ToArray(), "application/pdf", "file.pdf");
                }
            }
        }

        public async Task<IActionResult> OnPostJoinToTripAsync(int? tripid)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            if (tripid == null)
            {
                return Page();
            }

            Trip = await _context.Trips.Include(t => t.Owner).FirstOrDefaultAsync(m => m.TripID == tripid);

            if (Trip == null)
            {
                return NotFound();
            }

            if (Trip.Map != null)
            {
                MapImage = "data:image/png;base64," + Convert.ToBase64String(Trip.Map, 0, Trip.Map.Length);
            }

            var user = await _userManager.GetUserAsync(User);
            var userId = await _userManager.GetUserNameAsync(user);

            var joined = _tripService.JoinToTrip(tripid, userId);

            if (!joined)
            {
                TempData["success"] = "Can't join trip.";
                return Page();
            }

            await _context.SaveChangesAsync();
            TempData["success"] = "Successfully joined trip.";

            return Page();
        }
    }
}
