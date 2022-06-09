#nullable disable
using GemBox.Pdf;
using GemBox.Pdf.Content;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrippyWeb.Data;
using TrippyWeb.Model;

namespace TrippyWeb.Pages.Trips
{
    public class DetailsModel : PageModel
    {
        private readonly TrippyWebDbContext _context;

        public DetailsModel(TrippyWebDbContext context)
        {
            _context = context;
        }

        public Trip Trip { get; set; }
        public string MapImage { get; set; }
        public FileResult PDF { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Trip = await _context.Trips.Include(t => t.Owner).FirstOrDefaultAsync(m => m.TripID == id);

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

        public async Task<IActionResult> OnPostDownloadPDFAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Trip = await _context.Trips.Include(t => t.Owner).FirstOrDefaultAsync(m => m.TripID == id);

            if (Trip == null)
            {
                return NotFound();
            }

            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            using (var document = new PdfDocument())
            {
                var page = document.Pages.Add();

                double margin = 50;

                using (var formattedText = new PdfFormattedText())
                {
                    formattedText.TextAlignment = PdfTextAlignment.Center;
                    formattedText.MaxTextWidth = 200;
                    formattedText.AppendLine("Trippy").AppendLine("Trip Plan");
                    page.Content.DrawText(formattedText,
                        new PdfPoint((page.CropBox.Width - formattedText.MaxTextWidth) / 2,
                            page.CropBox.Top - margin - formattedText.Height));

                    var heightOffset = formattedText.Height;

                    formattedText.Clear();


                    using (MemoryStream stream = new MemoryStream(Trip.Map))
                    {
                        var image = PdfImage.Load(stream);
                        var transform = PdfMatrix.Identity;
                        transform.Translate(margin, page.CropBox.Top - margin - image.Size.Height - heightOffset);
                        transform.Scale(image.Size.Width, image.Size.Height);
                        page.Content.DrawImage(image, transform);
                    }
                }


                using (MemoryStream stream = new MemoryStream())
                {
                    StreamWriter objstreamwriter = new StreamWriter(stream);
                    document.Save(stream);
                    objstreamwriter.Flush();
                    objstreamwriter.Close();
                    return File(stream.ToArray(), "application/pdf", "file.pdf");
                }
            }
        }
    }
}
