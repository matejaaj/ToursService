using Tours.Core.Domain.Entities.TourExecution;
using Tours.Core.Domain.RepositoryInterfaces;

namespace Tours.Infrastructure.Database.Repositories;

public class TourExecutionRepository : CrudRepository<TourExecution, ToursContext>, ITourExecutionRepository
{
    public TourExecutionRepository(ToursContext dbContext) : base(dbContext) { }
    
    public TourExecution? GetByTourIdAndTouristId(long tourId, long touristId)
    {
        return DbContext.TourExecutions.FirstOrDefault(te => te.TourId == tourId && te.TouristId == touristId); 
    }

    public List<TourExecution> GetAllByTouristId(long touristId)
    {
        return DbContext.TourExecutions
            .Where(te => te.TouristId == touristId)
            .ToList();
    }

}

