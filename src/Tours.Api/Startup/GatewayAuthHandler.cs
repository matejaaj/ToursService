using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Tours.Api.Startup;

public class GatewayAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public GatewayAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock) { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var userId = Request.Headers["X-User-Id"].FirstOrDefault();
        var role = Request.Headers["X-User-Role"].FirstOrDefault();
        var personId = Request.Headers["X-Person-Id"].FirstOrDefault();

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
        {
            return Task.FromResult(AuthenticateResult.Fail("Missing auth headers from gateway."));
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Role, role),
        };

        if (!string.IsNullOrEmpty(personId))
        {
            claims.Add(new Claim("person_id", personId));
        }

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}