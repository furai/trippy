using Microsoft.EntityFrameworkCore;
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

        public bool JoinToTrip(int? tripId, string? userId)
        {
            if (_context.TrippyUsers != null)
            {
                if (_context.Trips != null)
                {
                    var t = _context.Trips.Include(t => t.Passengers).FirstOrDefault(m => m.TripID == tripId);

                    if (t != null && t.FreeSpots > t.Passengers.Count && t.Passengers.Where(u => u.UserName.Equals(userId)).FirstOrDefault() == null)
                    {
                        TrippyUser user = _context.TrippyUsers.Where(u => u.UserName.Equals(userId)).First();
                        t.Passengers.Add(user);
                    }
                    else
                    {
                        return false;
                    }

                    return true;
                }
            }
            return false;
        }

        public bool LeaveTrip(int? tripId, string? userId)
        {
            if (_context.TrippyUsers != null)
            {
                if (_context.Trips != null)
                {
                    var t = _context.Trips.Include(t => t.Passengers).FirstOrDefault(m => m.TripID == tripId);

                    if (t != null && t.Passengers.Where(u => u.UserName.Equals(userId)).FirstOrDefault() != null)
                    {
                        TrippyUser user = _context.TrippyUsers.Where(u => u.UserName.Equals(userId)).First();
                        t.Passengers.Remove(user);
                    }
                    else
                    {
                        return false;
                    }

                    return true;
                }
            }
            return false;
        }
    }
}
