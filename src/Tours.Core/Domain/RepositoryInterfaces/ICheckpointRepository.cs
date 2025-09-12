using Tours.Core.Domain.Entities.Tour;
namespace Tours.Core.Domain.RepositoryInterfaces
{
  public interface ICheckpointRepository
  {
    public Checkpoint Create(Checkpoint tour);
  }
}