using System.Security.Claims;
using Tours.Core.UseCases.Interfaces;

namespace Tours.Api.Authentication;
public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _http;

    public CurrentUser(IHttpContextAccessor http) => _http = http;

    private ClaimsPrincipal? User => _http.HttpContext?.User;
    private IHeaderDictionary? Headers => _http.HttpContext?.Request.Headers;

    public bool IsAuthenticated =>
        (User?.Identity?.IsAuthenticated ?? false)
        || Headers?.ContainsKey("x-user-id") == true;

    public long UserId
    {
        get
        {
            var raw = User?.FindFirstValue(ClaimTypes.NameIdentifier)
                      ?? Headers?["x-user-id"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(raw) || !long.TryParse(raw, out var id))
                throw new UnauthorizedAccessException("Missing or invalid UserId.");

            return id;
        }
    }

    public string Role
        => User?.FindFirstValue(ClaimTypes.Role)
           ?? Headers?["x-user-role"].FirstOrDefault()
           ?? throw new UnauthorizedAccessException("Missing Role.");

    public long? PersonId
    {
        get
        {
            var raw = User?.FindFirst("person_id")?.Value
                      ?? Headers?["x-person-id"].FirstOrDefault();
            return long.TryParse(raw, out var pid) ? pid : (long?)null;
        }
    }
}
