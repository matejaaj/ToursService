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


    [HttpPut("{tourExecutionId:long}/update-tourist-location")]
    public ActionResult<TourExecutionDto> Update([FromBody] TouristPositionDto dto, long tourExecutionId)
    {
        var result = _tourExecutionService.UpdateTouristLocation(tourExecutionId, dto.Latitude, dto.Longitude);
        return CreateMappedResponse<TourExecutionDto, TourExecution>(result);
    }

    [HttpPost("{tourId:long}")]

    public ActionResult<TourExecutionDto> StartTourExecution([FromBody] TouristPositionDto dto, long tourId)
    {
        var result =_tourExecutionService.StartTourExecution(tourId, dto.Latitude, dto.Longitude);
        return CreateMappedResponse<TourExecutionDto, TourExecution>(result);
    }


    [HttpPut("abandon/{tourExecutionId:long}")]

    public ActionResult<TourExecutionDto> AbandonTourExecution([FromBody] TouristPositionDto dto, long tourExecutionId)
    {
        var result = _tourExecutionService.AbandonTourExecution(tourExecutionId, dto.Latitude, dto.Longitude);
        return CreateMappedResponse<TourExecutionDto, TourExecution>(result);
    }

    [HttpPut("complete/{tourExecutionId:long}")]

    public ActionResult<TourExecutionDto> CompleteTourExectuion([FromBody] TouristPositionDto dto, long tourExecutionId)
    {
        var result = _tourExecutionService.CompleteTourExecution(tourExecutionId, dto.Latitude, dto.Longitude);
        return CreateMappedResponse<TourExecutionDto, TourExecution>(result);
    }


    [HttpGet("by-tourist/{touristId:long}")]
    public ActionResult<List<TourExecutionDto>> GetAllByTouristId(long touristId)
    {
        var result = _tourExecutionService.GetAllByTouristId(touristId);
        return CreateMappedResponse<TourExecutionDto, TourExecution>(result);
    }

}

