namespace TrippyWeb.Services
{
    public class TripService : ITripService
    {
        private readonly TrippyWebDbContext _context;

        public TripService(TrippyWebDbContext context)
        {
            _context = context;
        }

        public IQueryable<Trip> GetActiveTrips() 
        {
            return _context.Trip.Where(t => t.IsActive);
        }

        public IQueryable<Trip> TakeSlot()
        {
            throw new NotImplementedException();
        }
        
    }
}