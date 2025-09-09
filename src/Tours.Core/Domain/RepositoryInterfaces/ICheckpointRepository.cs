using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Tours.Core.Domain.Entities;
namespace Tours.Core.Domain.RepositoryInterfaces
{
  public interface ICheckpointRepository
  {
    public Checkpoint Create(Checkpoint tour);
    public Checkpoint GetById(long id);
    public void Delete(long id);
    public Checkpoint Update(Checkpoint checkpoint);
    public List<Checkpoint> GetByTourId(long tourId);
    }
}