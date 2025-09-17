using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tours.Api.Dtos;
using Tours.Core.Domain.Entities.Tour;
using Tours.Core.UseCases.Interfaces;

namespace Tours.Api.Controllers;
[ApiController]
[Route("api/tours/all")]
public class TourControllerAll : BaseApiController
{
        private readonly ITourService tourService;

        private readonly IMapper mapper;

        public TourControllerAll(ITourService tourService, IMapper mapper) : base(mapper)
        {
            this.tourService = tourService;
        }

        
        [HttpGet("published/{tourId:long}")]
        public ActionResult<TourPreviewDto> GetPublished([FromRoute]  long tourId)
        {
            var result = tourService.GetPublished(tourId);
            return CreateMappedResponse<TourDto, Tour>(result);
        }
}