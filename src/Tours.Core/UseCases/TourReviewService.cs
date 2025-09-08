using FluentResults;
using Tours.Core.Domain.Entities.Tour;
using Tours.Core.Domain.Entities.TourExecution;
using Tours.Core.Domain.RepositoryInterfaces;
using Tours.Core.UseCases.Interfaces;

namespace Tours.Core.UseCases;

public class TourReviewService : CrudService<TourReview>, ITourReviewService
{
    private readonly ITourReviewRepository _tourReviewRepository;

    private readonly ITourExecutionService _tourExecutionService;

    private readonly ICurrentUser _currentUser;

    private readonly ITourService _tourService;

    public TourReviewService(ITourReviewRepository tourReviewRepository, ICrudRepository<TourReview> repository, ITourService tourService,ICurrentUser currentUser,  ITourExecutionService tourExecutionService) : base(repository)
    {
        _tourReviewRepository = tourReviewRepository;
        _tourService = tourService;
        _tourExecutionService = tourExecutionService;
        _currentUser = currentUser;
    }

    public override Result<TourReview> Create(TourReview review)
    {
        try
        {
            if (!_currentUser.IsAuthenticated || review.TouristId != _currentUser.UserId)
                return Result.Fail(FailureCode.Forbidden).WithError("You cannot create a review for another user.");
            

            var alreadyReviewed= CheckIfTouristAlreadyReviewed((long)review.TourId, (long)review.TouristId);
            if (alreadyReviewed) return Result.Fail(FailureCode.Forbidden).WithError("Already reviewed!");

            var tourExecution = _tourExecutionService.GetByTourIdAndTouristId(review.TourId, review.TouristId);
            if (tourExecution is null)
                return Result.Fail(FailureCode.Forbidden).WithError("Tour execution not found for this tourist and tour.");

            var tour = _tourService.Get(review.TourId).Value;
            var totalCheckpointNum = tour.GetCheckpointNum();
            var completion = tourExecution.CalculateCompletion(totalCheckpointNum);

            review.SetCompletion(completion);

            return base.Create(review);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    private bool CheckIfTouristAlreadyReviewed(long tourId, long touristId)
    {
        var existingReview = _tourReviewRepository.GetByTourIdAndTouristId(tourId, touristId);
        return existingReview != null;
    }
}

