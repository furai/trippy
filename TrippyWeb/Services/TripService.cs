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

        public IQueryable<Trip>? TakeSlot(int? tripId, string? userId)
        {
            if (_context.TrippyUsers != null)
            {
                TrippyUser user = _context.TrippyUsers.Where(u => u.Id.Equals(userId)).First();
                if (_context.Trips != null && user != null)
                {
                    List<TrippyUser> passengers = _context.Trips
                    .Where(t => t.TripID == tripId)
                    .Where(t => t.FreeSpots > t.Passengers.Count())
                    .Select(t => t.Passengers)
                    .First();

                    passengers.Add(user);
                    return _context.Trips;
                }
            }
            return null;
        }
    }
}
