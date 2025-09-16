using AutoMapper;
using Grpc.Core;
using Tours.Api.Dtos;
using Tours.Api.Mappers; // <- važno
using Tours.Core.UseCases.Interfaces;

namespace Tours.Api.GrpcServices
{
    public class TourExecutionGrpcService : TourExecutionService.TourExecutionServiceBase
    {
        private readonly ITourExecutionService _tourExecutionService;
        private readonly IMapper _mapper;

        public TourExecutionGrpcService(ITourExecutionService tourExecutionService, IMapper mapper)
        {
            _tourExecutionService = tourExecutionService;
            _mapper = mapper;
        }

        public override Task<TourExecutionResponse> AbandonTourExecution(AbandonTourExecutionRequest request, ServerCallContext context)
        {
            var result = _tourExecutionService.AbandonTourExecution(request.TourExecutionId, request.Latitude, request.Longitude);
            if (result.IsFailed || result.Value == null) throw result.ToRpcException();
            return Task.FromResult(ToResponse(_mapper.Map<TourExecutionDto>(result.Value)));
        }

        public override Task<TourExecutionResponse> CompleteTourExecution(CompleteTourExecutionRequest request, ServerCallContext context)
        {
            var result = _tourExecutionService.CompleteTourExecution(request.TourExecutionId, request.Latitude, request.Longitude);
            if (result.IsFailed || result.Value == null) throw result.ToRpcException();
            return Task.FromResult(ToResponse(_mapper.Map<TourExecutionDto>(result.Value)));
        }

        private static TourExecutionResponse ToResponse(TourExecutionDto dto)
        {
            var resp = new TourExecutionResponse
            {
                Id = dto.Id,
                TourId = dto.TourId,
                TouristId = dto.TouristId,
                Status = dto.Status,
                LastActivity = ToUnixTimeSeconds(dto.LastActivity),
                Completion = dto.Completion,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude
            };

            if (dto.CompletedCheckpoints != null)
            {
                resp.CompletedCheckpoints.AddRange(dto.CompletedCheckpoints.Select(cp => new CompletedCheckpointResponse
                {
                    CheckpointId = cp.CheckpointId,
                    CompletionTime = ToUnixTimeSeconds(cp.CompletionTime)
                }));
            }

            return resp;
        }

        private static long ToUnixTimeSeconds(DateTime dt)
            => new DateTimeOffset(dt.ToUniversalTime()).ToUnixTimeSeconds();
    }
}
