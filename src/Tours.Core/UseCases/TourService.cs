using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using Tours.Core.Domain.Entities.Tour;
using Tours.Core.Domain.RepositoryInterfaces;
using Tours.Core.UseCases.Interfaces;

namespace Tours.Core.UseCases;

public class TourService : CrudService<Tour>, ITourService
{
    private readonly ITourRepository _tourRepository;
    public TourService(ITourRepository tourRepository, ICrudRepository<Tour> repository) : base(repository)
    {
        _tourRepository = tourRepository;
    }

    public  Result<Tour> Get(long id)
    {
        return _tourRepository.Get(id);
    }
}

