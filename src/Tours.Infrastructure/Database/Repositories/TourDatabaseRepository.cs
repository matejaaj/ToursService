using Tours.Core.Domain.Entities.Tour;
using Tours.Core.Domain.RepositoryInterfaces;
namespace Tours.Infrastructure.Database.Repositories
{
    public class TourDatabaseRepository : ITourRepository
    {
        private readonly ToursContext _context;

        public TourDatabaseRepository(ToursContext context)
        {
            _context = context;
        }

        public Tour Create(Tour tour)
        {
            _context.Tours.Add(tour);
            _context.SaveChanges();
            return tour;
        }

        public List<Tour> GetByAuthor(long id)
        {
            return _context.Tours.Where(tour => tour.AuthorId == id).ToList();
        }

        public Tour GetById(long id)
        {
            return _context.Tours.Find(id);
        }
  }
}