using AutoMapper;
using FluentResults;
using Tours.Core.Dtos;
using Tours.Core.Public;
using Tours.Core.Domain.Entities;
using Tours.Core.Domain.RepositoryInterfaces;

namespace Tours.Core.UseCases
{
public class TourService : ITourService
{
  private readonly ITourRepository _tourRepository;
  private readonly ICheckpointRepository _checkpointRepository;
  private readonly IMapper _mapper;

  public TourService(ITourRepository tourRepository,ICheckpointRepository checkpointRepository, IMapper mapper)
  {
    _tourRepository = tourRepository;
      _checkpointRepository = checkpointRepository;
    _mapper = mapper;
  }

  public Result<TourDto> Create(TourDto tourDto,long userId)
  {
    Tour tour = _mapper.Map<Tour>(tourDto);
    tour.AuthorId = userId;
    Tour savedTour = _tourRepository.Create(tour);
    return Result.Ok(_mapper.Map<TourDto>(savedTour));
  }

    public Result<CheckpointDto> CreateCheckpoint(CheckpointDto checkpointDto, long userId)
    {
        Tour tour = _tourRepository.GetById(checkpointDto.TourId);
        if (tour.AuthorId != userId)
        {
        return Result.Fail(FailureCode.Forbidden);
        }
        Checkpoint checkpoint = _mapper.Map<Checkpoint>(checkpointDto);
        Checkpoint savedCheckpoint = _checkpointRepository.Create(checkpoint);
        return Result.Ok(_mapper.Map<CheckpointDto>(savedCheckpoint));
    }

    public Result<List<TourDto>> GetByAuthor(long id)
    {
      List<Tour> tours = _tourRepository.GetByAuthor(id);
      return Result.Ok(_mapper.Map<List<TourDto>>(tours));
    }
  }


}