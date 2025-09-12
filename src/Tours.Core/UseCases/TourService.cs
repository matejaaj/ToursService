using AutoMapper;
using FluentResults;
using Tours.Core.UseCases.Interfaces;
using Tours.Core.Domain.Entities.Tour;
using Tours.Core.Domain.RepositoryInterfaces;


namespace Tours.Core.UseCases
{
  public class TourService : ITourService
  {
    private readonly ITourRepository _tourRepository;
    private readonly ICheckpointRepository _checkpointRepository;
    private readonly IMapper _mapper;

    public TourService(ITourRepository tourRepository, ICheckpointRepository checkpointRepository, IMapper mapper)
    {
      _tourRepository = tourRepository;
      _checkpointRepository = checkpointRepository;
      _mapper = mapper;
    }

    public Result<Tour> Create(Tour tour, long userId)
    {
      tour.AuthorId = userId;
      Tour savedTour = _tourRepository.Create(tour);
      return Result.Ok(savedTour);
    }

    public Result<Checkpoint> CreateCheckpoint(Checkpoint checkpoint, long userId)
    {
      Tour tour = _tourRepository.GetById(checkpoint.TourId.Value);
      if (tour.AuthorId != userId)
      {
        return Result.Fail(FailureCode.Forbidden);
      }

      Checkpoint savedCheckpoint = _checkpointRepository.Create(checkpoint);
      return Result.Ok(savedCheckpoint);
    }

    public Result<List<Tour>> GetByAuthor(long id)
    {
      List<Tour> tours = _tourRepository.GetByAuthor(id);
      return Result.Ok(tours);
    }
    
    public  Result<Tour> Get(long id)
    {
        return _tourRepository.GetById(id);
    }

  }


}
