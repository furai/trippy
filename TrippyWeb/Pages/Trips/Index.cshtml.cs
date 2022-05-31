#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrippyWeb.Data;
using TrippyWeb.Model;

namespace TrippyWeb.Pages.Trips
{
    public class IndexModel : PageModel
    {
        private readonly TrippyWeb.Data.TrippyWebDbContext _context;

        public IndexModel(TrippyWeb.Data.TrippyWebDbContext context)
        {
            _context = context;
        }

        public IList<Trip> Trip { get;set; }

        public async Task OnGetAsync()
        {
            Trip = await _context.Trips.ToListAsync();
        }
    }
}
