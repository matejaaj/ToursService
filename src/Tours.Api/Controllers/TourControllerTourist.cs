using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tours.Api.Dtos;
using Tours.Core.UseCases.Interfaces;
using Tours.Core.Domain.Entities.Tour;
namespace Tours.Api.Controllers
{
  [ApiController]
  [Route("api/tours/tourist")]
  [AuthorizeRole("Tourist")]
  public class TourControllerTourist : BaseApiController
  {
    private readonly ITourService tourService;
    public TourControllerTourist(ITourService tourService, IMapper mapper) : base(mapper)
    {
      this.tourService = tourService;
    }

    [HttpGet("{tourId:long}")]
    public ActionResult<TouristTourPreviewDto> GetPublished([FromRoute]  long tourId)
    {
      var userId = long.Parse(HttpContext.Request.Headers["X-User-Id"]);
      var result = tourService.GetById(userId,tourId);
      return CreateMappedResponse<TouristTourPreviewDto, Tour>(result);
    }
  }
}