using AutoMapper;
using Tours.Core.Domain.Entities;
using Tours.Core.Domain.Entities.Tour;
using Tours.Core.Domain.Entities.TourExecution;

using Tours.Api.Dtos;

namespace Tours.Api.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<ReviewDto, Review>().ReverseMap();
        CreateMap<EquipmentDto, Equipment>().ReverseMap();


        CreateMap<TourDto, Tour>().ForMember(dest => dest.Equipment, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Equipment, opt => opt.MapFrom(src => src.Equipment));
        //CreateMap<Tour, TourDto>().ForMember(dest => dest.IsPublished, opt => opt.MapFrom(src => !src.IsNotPublished()));
      //  CreateMap<Tour, TourDto>().ForMember(dest => dest.IsPublished, opt => opt.MapFrom(src => src.IsPublished()));


        CreateMap<Price, PriceDto>().ReverseMap();
        CreateMap<TourDuration, TourDurationDto>().ReverseMap();
        CreateMap<Distance, DistanceDto>()
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit.ToString())) // Enum to String
            .ReverseMap()
            .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => Enum.Parse<DistanceUnit>(src.Unit, true)));
        //CreateMap<TourCardDto, Tour>().ReverseMap();
        CreateMap<TourReviewDto, Review>().ReverseMap();
        CreateMap<TourCardDto, Tour>()
            .ReverseMap().ForMember(dest => dest.Distance,
                opt => opt.MapFrom(src => src.TotalLength.Length + src.TotalLength.Unit.ToString()));

        CreateMap<TourExecutionDto, TourExecution>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Position,
                opt => opt.MapFrom(src => new TouristPosition(src.Longitude, src.Latitude)));
        CreateMap<TourExecution, TourExecutionDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Position.Longitude))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Position.Latitude));

        CreateMap<CheckpointDto, Checkpoint>().ReverseMap();

    }

}