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
        public Result<CheckpointDto> UpdateCheckpoint(long userId, long id, CheckpointDto dto);
        public Result DeleteCheckpoint(long checkpointId, long userId);
        public Result<List<CheckpointDto>> GetCheckpointsByTour(long tourId);
    }
}
