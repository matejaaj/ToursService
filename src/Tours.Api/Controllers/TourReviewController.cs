using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tours.Api.Dtos;
using Tours.Core.Domain.Entities.Tour;
using Tours.Core.UseCases.Interfaces;

namespace Tours.Api.Controllers;

[Authorize(Policy = "touristPolicy")]
[Route("api/tourist/reviews")]

public class TourReviewController : BaseApiController
{
    private readonly ITourReviewService _tourReviewService;
    public TourReviewController(ITourReviewService tourReviewService, IMapper mapper) : base(mapper)
    {
        _tourReviewService = tourReviewService;
    }
    [HttpPost]
    public ActionResult Create([FromBody] ReviewDto dto)
    { 
        var review = Mapper.Map<TourReview>(dto);
        var result = _tourReviewService.Create(review);
        return CreateMappedResponse<ReviewDto, TourReview>(result);
    }

}

