using Tours.Core.Domain.Entities.TourExecution;
using Tours.Core.Domain.RepositoryInterfaces;

namespace Tours.Infrastructure.Database.Repositories;

public class TourExecutionDatabaseRepository : CrudDatabaseRepository<TourExecution, ToursContext>, ITourExecutionRepository
{
    public TourExecutionDatabaseRepository(ToursContext dbContext) : base(dbContext) { }
    
    public TourExecution? GetByTourIdAndTouristId(int tourId, int touristId)
    {
        return DbContext.TourExecutions.FirstOrDefault(te => te.TourId == tourId && te.TouristId == touristId && te.Status == TourExecutionStatus.ONGOING); //changed it so that it only finds ongoing tours
    }



}

