using FluentResults;
using Tours.Core.Domain.Entities.Tour;

namespace Tours.Core.UseCases.Interfaces;

public interface ITourReviewService
{
    public Result<TourReview> Create(TourReview review);
}

