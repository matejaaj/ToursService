using Microsoft.AspNetCore.Authorization;
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

            var result = tourService.Create(tourDto, userId);
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
            var result = tourService.CreateCheckpoint(checkpointDto, userId);
            return CreateResponse(result);
        }

        [HttpPut("checkpoint/{id:long}")]
        public ActionResult<CheckpointDto> UpdateCheckpoint([FromRoute] long id, [FromBody] CheckpointDto dto)
        {
            long userId = long.Parse(HttpContext.Request.Headers["X-User-Id"]);
            var result = tourService.UpdateCheckpoint(userId, id, dto);
            return CreateResponse(result);
        }

        [HttpDelete("checkpoint/{id:long}")]
        public ActionResult DeleteCheckpoint([FromRoute] long id)
        {
            long userId = long.Parse(HttpContext.Request.Headers["X-User-Id"]);
            var result = tourService.DeleteCheckpoint(id, userId);
            return CreateResponse(result);
        }


        [HttpGet("{tourId:long}/checkpoints")]
        public ActionResult<List<CheckpointDto>> GetCheckpointsByTour([FromRoute] long tourId)
        {
            var result = tourService.GetCheckpointsByTour(tourId);
            return CreateResponse(result);
        }
    }
}