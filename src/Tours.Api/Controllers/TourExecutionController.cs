using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tours.Api.Dtos;
using Tours.Core.Domain.Entities.Tour;
using Tours.Core.Domain.Entities.TourExecution;
using Tours.Core.UseCases.Interfaces;

namespace Tours.Api.Controllers;


[Authorize(Policy = "touristPolicy")]
[Route("api/tours/tour-executions")]
public class TourExecutionController : BaseApiController
{
    private readonly ITourExecutionService _tourExecutionService;

    private readonly IMapper mapper;

    public TourExecutionController(ITourExecutionService tourExecutionService, IMapper mapper) : base(mapper)
    {
        _tourExecutionService = tourExecutionService;
    }


    [HttpPut("/{tourExecutionId:long}/update-tourist-location")]
    public ActionResult<TourExecutionDto> Update([FromBody] TouristLocationDto dto, long tourExecutionId)
    {
        var result = _tourExecutionService.UpdateTouristLocation(tourExecutionId, dto.Latitude, dto.Longitude);
        return CreateMappedResponse<TourExecutionDto, TourExecution>(result);
    }

    [HttpPost("/{tourId:long}")]

    public ActionResult<TourExecutionDto> StartTour([FromBody] TouristLocationDto dto, long tourId)
    {
        var result =_tourExecutionService.StartTour(tourId, dto.Latitude, dto.Longitude);
        return CreateMappedResponse<TourExecutionDto, TourExecution>(result);
    }


    [HttpPost("/{tourExecutionId:long}")]

    public ActionResult<TourExecutionDto> StartTour(long tourExecutionId)
    {
        var result = _tourExecutionService.StartTour(tourId, dto.Latitude, dto.Longitude);
        return CreateMappedResponse<TourExecutionDto, TourExecution>(result);
    }


}

