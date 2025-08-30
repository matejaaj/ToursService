using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Tours.Core.Dtos;

namespace Tours.Core.Public
{
    public interface ITourService
    {
        public Result<TourDto> Create(TourDto tourDto,long userId);
        Result<CheckpointDto> CreateCheckpoint(CheckpointDto checkpointDto, long userId);
        public Result<List<TourDto>> GetByAuthor(long id);
    }
}
