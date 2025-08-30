using AutoMapper;
using Tours.Core.Dtos;
using Tours.Core.Domain.Entities;
namespace Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<CheckpointDto, Checkpoint>().ReverseMap();
        CreateMap<TourDto, Tour>().ReverseMap();
    }

}