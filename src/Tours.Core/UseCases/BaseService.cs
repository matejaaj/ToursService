using Tours.Core.Domain.Entities;
using AutoMapper;
using FluentResults;

namespace Tours.Core.UseCases;

    public abstract class BaseService<TDto, TDomain> where TDomain : Entity
    {
        private readonly IMapper _mapper;

        protected BaseService(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected TDomain MapToDomain(TDto dto)
        {
            return _mapper.Map<TDomain>(dto);
        }

        protected List<TDomain> MapToDomain(List<TDto> dtos)
        {
            return dtos.Select(dto => _mapper.Map<TDomain>(dto)).ToList();
        }

        protected TDto MapToDto(TDomain result)
        {
            return _mapper.Map<TDto>(result);
        }

        protected Result<List<TDto>> MapToDto(Result<List<TDomain>> result)
        {
            if (result.IsFailed) return Result.Fail(result.Errors);
            return result.Value.Select(_mapper.Map<TDto>).ToList();
        }

        protected Result<PagedResult<TDto>> MapToDto(Result<PagedResult<TDomain>> result)
        {
            if (result.IsFailed) return Result.Fail(result.Errors);

            var items = result.Value.Results.Select(_mapper.Map<TDto>).ToList();
            return new PagedResult<TDto>(items, result.Value.TotalCount);
        }
    }

