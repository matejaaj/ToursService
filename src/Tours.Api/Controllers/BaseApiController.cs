using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Tours.Core.UseCases;

namespace Tours.Api.Controllers;

[ApiController]
public abstract class BaseApiController : ControllerBase
{
    protected readonly IMapper Mapper;

    protected BaseApiController(IMapper mapper)
    {
        Mapper = mapper;
    }

    // =========== ERROR HANDLING ===========
    protected ActionResult CreateErrorResponse(List<IError> errors)
    {
        var code = 500;
        if (ContainsErrorCode(errors, 400)) code = 400;
        if (ContainsErrorCode(errors, 403)) code = 403;
        if (ContainsErrorCode(errors, 404)) code = 404;
        if (ContainsErrorCode(errors, 409)) code = 409;
        return CreateErrorObject(errors, code);
    }

    private static bool ContainsErrorCode(List<IError> errors, int code)
    {
        return errors.Any(e =>
        {
            e.Metadata.TryGetValue("code", out var errorCode);
            if (errorCode == null) return false;
            return (int)errorCode == code;
        });
    }

    private ObjectResult CreateErrorObject(List<IError> errors, int code)
    {
        var sb = new StringBuilder();
        foreach (var error in errors)
        {
            sb.Append(error);
            error.Metadata.TryGetValue("subCode", out var subCode);
            if (subCode != null)
            {
                sb.Append(';');
                sb.Append(subCode);
            }
            sb.AppendLine();
        }
        return Problem(statusCode: code, detail: sb.ToString());
    }

    // =========== PLAIN RESPONSES ===========
    protected ActionResult CreateResponse(Result result)
    {
        return result.IsSuccess ? Ok() : CreateErrorResponse(result.Errors.ToList());
    }

    protected ActionResult CreateResponse<T>(Result<T> result)
    {
        return result.IsSuccess ? Ok(result.Value) : CreateErrorResponse(result.Errors.ToList());
    }

    // =========== MAPPED RESPONSES (DTO <-> DOMAIN) ===========
    protected ActionResult CreateMappedResponse<TDto, TDomain>(Result<TDomain> result)
    {
        return result.IsSuccess
            ? Ok(Mapper.Map<TDto>(result.Value))
            : CreateErrorResponse(result.Errors.ToList());
    }

    protected ActionResult CreateMappedResponse<TDto, TDomain>(Result<List<TDomain>> result)
    {
        return result.IsSuccess
            ? Ok(result.Value.Select(x => Mapper.Map<TDto>(x)).ToList())
            : CreateErrorResponse(result.Errors.ToList());
    }

    protected ActionResult CreateMappedResponse<TDto, TDomain>(Result<PagedResult<TDomain>> result)
    {
        if (result.IsFailed) return CreateErrorResponse(result.Errors.ToList());

        var items = result.Value.Results.Select(x => Mapper.Map<TDto>(x)).ToList();
        var paged = new PagedResult<TDto>(items, result.Value.TotalCount);
        return Ok(paged);
    }
}
