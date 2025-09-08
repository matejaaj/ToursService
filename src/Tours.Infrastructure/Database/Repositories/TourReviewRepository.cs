using Tours.Core.Domain.Entities.Tour;
using Tours.Core.Domain.RepositoryInterfaces;

namespace Tours.Infrastructure.Database.Repositories;

public class TourReviewRepository : CrudRepository<TourReview, ToursContext>, ITourReviewRepository
{
    public TourReviewRepository(ToursContext dbContext) : base(dbContext) { }

    public TourReview? GetByTourIdAndTouristId(long tourId, long touristId)
    {
        var review = DbContext.Reviews
            .FirstOrDefault(r => r.TourId == tourId && r.TouristId == touristId);
        
        return review;
    }
}

