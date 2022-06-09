using TrippyWeb.Model;

namespace TrippyWeb.Services
{
    public interface ITripService
    {
        public IQueryable<Trip>? GetActiveTrips();
        public IQueryable<Trip>? JoinToTrip(int? tripId, string? userId);
    }
}
