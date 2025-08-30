using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Tours.Api.Startup;

public static class SwaggerConfiguration
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(setup =>
        {
            setup.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Tour Service API",
                Version = "v1",
                Description = "Explorer microservices – API (expects gateway-forwarded headers)"
            });

            var roleScheme = new OpenApiSecurityScheme
            {
                Name = "X-User-Role",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Description = "User role provided by the API Gateway (e.g. tourist, author, administrator).",
                Reference = new OpenApiReference
                {
                    Id = "X-User-Role",
                    Type = ReferenceType.SecurityScheme
                }
            };

            var userIdScheme = new OpenApiSecurityScheme
            {
                Name = "X-User-Id",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Description = "User ID provided by the API Gateway (e.g. 123).",
                Reference = new OpenApiReference
                {
                    Id = "X-User-Id",
                    Type = ReferenceType.SecurityScheme
                }
            };

            var personIdScheme = new OpenApiSecurityScheme
            {
                Name = "X-Person-Id",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Description = "Person ID provided by the API Gateway (optional).",
                Reference = new OpenApiReference
                {
                    Id = "X-Person-Id",
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(roleScheme.Reference.Id, roleScheme);
            setup.AddSecurityDefinition(userIdScheme.Reference.Id, userIdScheme);
            setup.AddSecurityDefinition(personIdScheme.Reference.Id, personIdScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { roleScheme, Array.Empty<string>() },
                { userIdScheme, Array.Empty<string>() }
            });
        });

        return services;
    }
}
