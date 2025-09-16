using FluentResults;
using Tours.Core.Domain.Entities.TourExecution;

namespace Tours.Core.UseCases.Interfaces;

public interface ITourExecutionService 
{

    public TourExecution? GetByTourIdAndTouristId(long tourId, long touristId);

    public Result<List<TourExecution>> GetAllByTouristId(long touristId);

    public Result<TourExecution> UpdateTouristLocation(long tourExecutionId, double latitude, double longitude);

    public Result<TourExecution> StartTourExecution(long tourId, double latitude, double longitude);

    public Result<TourExecution> AbandonTourExecution(long tourExecutionId, double latitude, double longitude);

    public Result<TourExecution> CompleteTourExecution(long tourExecutionId, double latitude, double longitude);


}

