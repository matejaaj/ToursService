using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tours.Api.Dtos;
using Tours.Core.UseCases.Interfaces;
using Tours.Core.Domain.Entities.Tour;
namespace Tours.Api.Controllers
{
  [ApiController]
  [Route("api/tours")]
  [AuthorizeRole("Author")]
  public class AuthenticationController : BaseApiController
  {
    private readonly ITourService tourService;

    private readonly IMapper mapper;

    public AuthenticationController(ITourService tourService, IMapper mapper) : base(mapper)
    {
      this.tourService = tourService;
    }

    [HttpPost]
    public ActionResult<TourDto> Create([FromBody] TourDto tourDto)
    {
      Tour tour = mapper.Map<Tour>(tourDto);
      var userId = long.Parse(HttpContext.Request.Headers["X-User-Id"]);
      var result = tourService.Create(tour,userId);
      return CreateMappedResponse<TourDto, Tour>(result);
    }
    [HttpGet("author")]
    public ActionResult<TourDto> GetByAuthor()
    {
      var userId = long.Parse(HttpContext.Request.Headers["X-User-Id"]);

      var result = tourService.GetByAuthor(userId);
      return CreateMappedResponse<TourDto, Tour>(result);
    }
    [HttpPost("checkpoint")]
    public ActionResult<CheckpointDto> CreateCheckpoint([FromBody] CheckpointDto checkpointDto)
    {
      Checkpoint checkpoint = mapper.Map<Checkpoint>(checkpointDto);
      var userId = long.Parse(HttpContext.Request.Headers["X-User-Id"]);
      var result = tourService.CreateCheckpoint(checkpoint,userId);
      return CreateMappedResponse<CheckpointDto, Checkpoint>(result);
    }
  }
}