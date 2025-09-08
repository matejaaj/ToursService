using Tours.Core.Domain.Entities.Tour;
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
  }
}