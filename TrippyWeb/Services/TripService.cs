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

        public IQueryable<Trip>? JoinToTrip(int? tripId, string? userId)
        {
            if (_context.TrippyUsers != null)
            {
                if (_context.Trips != null)
                {
                    Trip t = _context.Trips.Where(t => t.TripID == tripId).First();
                
                    if (t.FreeSpots > t.Passengers?.Count())
                    {
                        TrippyUser user = _context.TrippyUsers.Where(u => u.UserName.Equals(userId)).First();
                        t.Passengers.Add(user);

                    }

                    return _context.Trips;
                }
            }
            return null;
        }
    }
}
