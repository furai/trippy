namespace TrippyWeb.Services
{
    public interface ITripService
    {
         public IQueryable<Trip> GetActiveTrips();
         public IQueryable<Trip> TakeSlot();
    }
}