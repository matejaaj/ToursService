using Tours.Core.Domain.Entities;
namespace Tours.Core.Domain.RepositoryInterfaces
{
  public interface ICheckpointRepository
  {
    public Checkpoint Create(Checkpoint tour);
  }
}