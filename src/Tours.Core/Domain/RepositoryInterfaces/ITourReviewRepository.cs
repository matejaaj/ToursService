using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tours.Core.Domain.Entities.Tour;
using Tours.Core.UseCases;

namespace Tours.Core.Domain.RepositoryInterfaces;

public interface ITourReviewRepository : ICrudRepository<TourReview>
{
    public TourReview GetByTourIdAndTouristId(long tourId, long touristId);
}

