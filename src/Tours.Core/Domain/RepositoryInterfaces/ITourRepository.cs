using Tours.Core.Domain.Entities;
namespace Tours.Core.Domain.RepositoryInterfaces
{
  public interface ITourRepository
  {
    public Tour Create(Tour tour);
    public Tour GetById(long id);
    public List<Tour> GetByAuthor(long id);
  }
}