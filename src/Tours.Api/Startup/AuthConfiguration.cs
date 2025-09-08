using Microsoft.AspNetCore.Authentication;

namespace Tours.Api.Startup;

public static class AuthConfiguration
{
    public static IServiceCollection ConfigureAuth(this IServiceCollection services)
    {
        ConfigureAuthentication(services);
        ConfigureAuthorizationPolicies(services);
        return services;
    }

    private static void ConfigureAuthentication(IServiceCollection services)
    {
        services.AddAuthentication("Gateway")
            .AddScheme<AuthenticationSchemeOptions, GatewayAuthHandler>("Gateway", null);
    }

    private static void ConfigureAuthorizationPolicies(IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            //options.AddPolicy("administratorPolicy", policy => policy.RequireRole("administrator"));
            //options.AddPolicy("authorPolicy", policy => policy.RequireRole("author"));
            options.AddPolicy("touristPolicy", policy => policy.RequireRole("tourist"));
            //options.AddPolicy("touristOrAuthorPolicy", policy => policy.RequireRole("tourist", "author"));
            //options.AddPolicy("authorOrAdministratorOrTouristPolicy", policy => policy.RequireRole("tourist", "author", "administrator"));
            //options.AddPolicy("authorOrAdministratorPolicy", policy => policy.RequireRole("author", "administrator"));
        });
    }
}