using System.Security.Claims;
using Tours.Core.UseCases.Interfaces;

namespace Tours.Api.Authentication;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _http;

    public CurrentUser(IHttpContextAccessor http) => _http = http;

    private ClaimsPrincipal? User => _http.HttpContext?.User;

    public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

    public long UserId
    {
        get
        {
            var raw = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(raw) || !long.TryParse(raw, out var id))
                throw new UnauthorizedAccessException("Missing or invalid UserId claim.");
            return id;
        }
    }

    public string Role
        => User?.FindFirstValue(ClaimTypes.Role)
           ?? throw new UnauthorizedAccessException("Missing Role claim.");

    public long? PersonId
    {
        get
        {
            var raw = User?.FindFirst("person_id")?.Value;
            return long.TryParse(raw, out var pid) ? pid : (long?)null;
        }
    }
}