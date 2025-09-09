using Tours.Core.Domain.Entities;
using Tours.Core.Domain.RepositoryInterfaces;
namespace Tours.Infrastructure.Database.Repositories
{
    public class CheckpointDatabaseRepository : ICheckpointRepository
    {
        private readonly ToursContext _context;

        public CheckpointDatabaseRepository(ToursContext context)
        {
            _context = context;
        }

        public Checkpoint Create(Checkpoint checkpoint)
        {
            _context.Checkpoints.Add(checkpoint);
            _context.SaveChanges();
            return checkpoint;
        }

        public Checkpoint GetById(long id)
        {
            return _context.Checkpoints.Find(id);
        }
        public void Delete(long id)
        {
            var checkpoint = _context.Checkpoints.Find(id);
            if (checkpoint != null)
            {
                _context.Checkpoints.Remove(checkpoint);
                _context.SaveChanges();
            }
        }
        public Checkpoint Update(Checkpoint checkpoint)
        {
            var existing = _context.Checkpoints.Find(checkpoint.Id);
            if (existing == null) throw new ArgumentException("Checkpoint not found.");

            existing.Name = checkpoint.Name;
            existing.Description = checkpoint.Description;
            existing.ImageBase64 = checkpoint.ImageBase64;
            existing.Latitude = checkpoint.Latitude;
            existing.Longitude = checkpoint.Longitude;
            existing.TourId = checkpoint.TourId;

            _context.SaveChanges();
            return existing;
        }

        public List<Checkpoint> GetByTourId(long tourId)
        {
            return _context.Checkpoints.Where(c => c.TourId == tourId).ToList();
        }

    }
}