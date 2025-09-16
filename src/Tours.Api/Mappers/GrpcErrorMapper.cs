using FluentResults;
using Grpc.Core;

namespace Tours.Api.Mappers;

public static class GrpcErrorMapper
{
    public static RpcException ToRpcException(this ResultBase result)
    {
        if (result == null || result.IsSuccess)
        {
            return new RpcException(new Status(StatusCode.Internal, "Unexpected error."));
        }

        var error = result.Errors.FirstOrDefault();
        var message = error?.Message ?? "Unknown error";

        // Ako postoji metadata "code", mapiraj na odgovarajući StatusCode
        if (error?.Metadata.TryGetValue("code", out var codeObj) == true
            && int.TryParse(codeObj?.ToString(), out var httpCode))
        {
            return new RpcException(new Status(MapHttpCodeToGrpc(httpCode), message));
        }

        // Fallback ako nema metadata
        return new RpcException(new Status(StatusCode.Unknown, message));
    }

    private static StatusCode MapHttpCodeToGrpc(int httpCode)
    {
        return httpCode switch
        {
            400 => StatusCode.InvalidArgument,
            403 => StatusCode.PermissionDenied,
            404 => StatusCode.NotFound,
            409 => StatusCode.AlreadyExists,
            500 => StatusCode.Internal,
            _ => StatusCode.Unknown
        };
    }
}