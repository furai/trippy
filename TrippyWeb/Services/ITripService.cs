using TrippyWeb.Model;

namespace TrippyWeb.Services
{
    public interface ITripService
    {
        public IQueryable<Trip>? GetActiveTrips();
        public bool JoinToTrip(int? tripId, string? userId);
        public bool LeaveTrip(int? tripId, string? userId);
    }
}
