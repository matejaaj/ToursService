using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Tours.Core.Domain.Entities.Tour;
using Tours.Core.Domain.Entities.TourExecution;
using Tours.Core.Domain.RepositoryInterfaces;
using Tours.Core.UseCases.Interfaces;

namespace Tours.Core.UseCases;

public class TourExecutionService : CrudService<TourExecution>, ITourExecutionService
{
    private readonly ITourExecutionRepository _tourExecutionRepository;

    private readonly ITourService _tourService;

    private readonly ICurrentUser _currentUserContext;

    public TourExecutionService(ICurrentUser currentUserContext,ITourExecutionRepository tourExecutionRepository, ITourService tourService, ICrudRepository<TourExecution> repository) : base(repository)
    {
        _tourExecutionRepository = tourExecutionRepository;
        _tourService = tourService;
        _currentUserContext = currentUserContext;
    }

    public TourExecution? GetByTourIdAndTouristId(long tourId, long touristId)
    {
        return _tourExecutionRepository.GetByTourIdAndTouristId(tourId, touristId);
    }

    public Result<List<TourExecution>> GetAllByTouristId(long touristId)
    {
        if (_currentUserContext.PersonId != touristId)
        {
            return Result.Fail(FailureCode.Forbidden)
                .WithError("You cannot access executions of another tourist.");
        }

        var executions = _tourExecutionRepository.GetAllByTouristId(touristId);
        return Result.Ok(executions);
    }


    public Result<TourExecution> UpdateTouristLocation(long tourExecutionId, double latitude, double longitude)
    {
        if (!ValidateLocation(latitude, longitude))
        {
            return Result.Fail(FailureCode.InvalidArgument);
        }

        var tourExecution = _tourExecutionRepository.Get(tourExecutionId);
        if (tourExecution == null) return Result.Fail(FailureCode.NotFound);

        if (tourExecution.TouristId != _currentUserContext.PersonId)
        {
            return Result.Fail(FailureCode.Forbidden).WithError("You cannot update position for another user");
        }

        if (tourExecution.Status != TourExecutionStatus.ONGOING)
        {
            return Result.Fail(FailureCode.Forbidden).WithError("You must start the tour first.");
        }


        var tourResult = _tourService.Get(tourExecution.TourId);
        var tour = tourResult.Value;
        if (tour == null)
        {
            return Result.Fail(FailureCode.NotFound);
        }

        var checkpoints = tour.Checkpoints;
        tourExecution.TryCompleteNearestCheckpoint(checkpoints, latitude, longitude, thresholdMeters: 10);


        return _tourExecutionRepository.Update(tourExecution);
    }


    public Result<TourExecution> StartTourExecution(long tourId, double latitude, double longitude)
    {
        if (!ValidateLocation(latitude, longitude))
        {
            return Result.Fail(FailureCode.InvalidArgument);
        }

        var tour = _tourService.Get(tourId).Value;
        if (tour == null)
        {
            return Result.Fail(FailureCode.NotFound);
        }

        if (tour.Status != Status.Archived && tour.Status != Status.Published)
        {
            return Result.Fail(FailureCode.Forbidden).WithError("Tour must be either archived or published.");
        }

        var existingExecution = _tourExecutionRepository.GetByTourIdAndTouristId(tourId, _currentUserContext.PersonId.Value);
        if (existingExecution != null)
        {
            return Result.Fail(FailureCode.Conflict).WithError("You have already started this tour.");
        }

        var newExecution = new TourExecution(tourId, _currentUserContext.PersonId.Value, longitude, latitude);

        return _tourExecutionRepository.Create(newExecution);
    }

    public Result<TourExecution> AbandonTourExecution(long tourExecutionId, double latitude, double longitude)
    {
        TourExecution tourExecution;
        try
        {
            tourExecution = _tourExecutionRepository.Get(tourExecutionId);
        }
        catch (KeyNotFoundException)
        {
            return Result.Fail(FailureCode.NotFound);
        }

        if (tourExecution.TouristId != _currentUserContext.PersonId)
        {
            return Result.Fail(FailureCode.Forbidden)
                .WithError("You cannot abandon a tour for another user");
        }

        try
        {
            tourExecution.AbandonTour(latitude, longitude);
            return _tourExecutionRepository.Update(tourExecution);
        }
        catch (InvalidOperationException ex)
        {
            return Result.Fail(FailureCode.Forbidden).WithError(ex.Message);
        }
    }


    public Result<TourExecution> CompleteTourExecution(long tourExecutionId, double latitude, double longitude)
    {
        TourExecution tourExecution;
        try
        {
            tourExecution = _tourExecutionRepository.Get(tourExecutionId);
        }
        catch (KeyNotFoundException)
        {
            return Result.Fail(FailureCode.NotFound);
        }

        if (tourExecution.TouristId != _currentUserContext.PersonId)
        {
            return Result.Fail(FailureCode.Forbidden)
                .WithError("You cannot complete a tour for another user");
        }

        try
        {
            tourExecution.CompleteTour(latitude, longitude);
            return _tourExecutionRepository.Update(tourExecution);
        }
        catch (InvalidOperationException ex)
        {
            return Result.Fail(FailureCode.Forbidden).WithError(ex.Message);
        }
    }




    private bool ValidateLocation(double latitude, double longitude)
    {
        return longitude is >= -180 and <= 180 && latitude is >= -90 and <= 90;
    }
}

