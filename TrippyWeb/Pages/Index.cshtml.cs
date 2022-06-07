using Microsoft.AspNetCore.Mvc.RazorPages;
using TrippyWeb.Model;
using TrippyWeb.Services;

namespace TrippyWeb.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ITripService _tripService;
    public IQueryable<Trip>? Records;

    public IndexModel(ILogger<IndexModel> logger, ITripService tripService)
    {
        _logger = logger;
        _tripService = tripService;
    }

    public void OnGet()
    {
        Records = _tripService.GetActiveTrips();
    }
}
