using Tours.Core.Domain.Entities.TourExecution;
using Tours.Core.Domain.RepositoryInterfaces;
using Tours.Core.UseCases.Interfaces;

namespace Tours.Core.UseCases;

public class TourExecutionService : CrudService<TourExecution>, ITourExecutionService
{
    private readonly ITourExecutionRepository _tourExecutionRepository;

    public TourExecutionService(ITourExecutionRepository tourExecutionRepository, ICrudRepository<TourExecution> repository) : base(repository)
    {
        _tourExecutionRepository = tourExecutionRepository;
    }

    public TourExecution? GetByTourIdAndTouristId(long tourId, long touristId)
    {
        return _tourExecutionRepository.GetByTourIdAndTouristId(tourId, touristId);
    }
}

