using Microsoft.AspNetCore.Mvc;
using Tours.Core.Dtos;
using Tours.Core.Public;
namespace Tours.Api.Controllers
{
  [ApiController]
  [Route("api/tours")]
  [AuthorizeRole("Author")]
  public class AuthenticationController : BaseApiController
  {
    private readonly ITourService tourService;

    public AuthenticationController(ITourService tourService)
    {
      this.tourService = tourService;
    }

    [HttpPost]
    public ActionResult<TourDto> Create([FromBody] TourDto tourDto)
    {
      var userId = long.Parse(HttpContext.Request.Headers["X-User-Id"]);

      var result = tourService.Create(tourDto,userId);
      return CreateResponse(result);
    }
    [HttpGet("author")]
    public ActionResult<TourDto> GetByAuthor()
    {
      var userId = long.Parse(HttpContext.Request.Headers["X-User-Id"]);

      var result = tourService.GetByAuthor(userId);
      return CreateResponse(result);
    }
    [HttpPost("checkpoint")]
    public ActionResult<TourDto> CreateCheckpoint([FromBody] CheckpointDto checkpointDto)
    {
      var userId = long.Parse(HttpContext.Request.Headers["X-User-Id"]);
      var result = tourService.CreateCheckpoint(checkpointDto,userId);
      return CreateResponse(result);
    }
  }
}