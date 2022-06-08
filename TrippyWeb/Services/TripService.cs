using TrippyWeb.Data;
using TrippyWeb.Model;

namespace TrippyWeb.Services
{
    public class TripService : ITripService
    {
        private readonly TrippyWebDbContext _context;

        public TripService(TrippyWebDbContext context)
        {
            _context = context;
        }

        public IQueryable<Trip>? GetActiveTrips()
        {
            if (_context.Trips != null)
            {
                return _context.Trips;
            }

            return null;
        }

        public IQueryable<Trip> TakeSlot()
        {
            throw new NotImplementedException();
        }
    }
}
