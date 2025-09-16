using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tours.Core.Domain.Entities.TourExecution;
using Tours.Core.UseCases;

namespace Tours.Core.Domain.RepositoryInterfaces;

public interface ITourExecutionRepository : ICrudRepository<TourExecution>
{ 
    public TourExecution? GetByTourIdAndTouristId(long tourId, long touristId);

    List<TourExecution> GetAllByTouristId(long touristId);
}

