using Tours.Core.Domain.Entities.TourExecution;

namespace Tours.Core.UseCases.Interfaces;

public interface ITourExecutionService 
{

    public TourExecution? GetByTourIdAndTouristId(long tourId, long touristId);
}

