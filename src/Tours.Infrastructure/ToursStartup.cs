using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tours.Core.Domain.Entities.Tour;
using Tours.Core.Domain.Entities.TourExecution;
using Tours.Core.Domain.RepositoryInterfaces;
using Tours.Core.UseCases;
using Tours.Core.UseCases.Interfaces;
using Tours.Infrastructure.Database;
using Tours.Infrastructure.Database.Repositories;

namespace Tours.Infrastructure;

public static class ToursStartup
{
    public static IServiceCollection ConfigureToursModule(this IServiceCollection services)
    {
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }

    private static void SetupCore(IServiceCollection services)
    {

        services.AddScoped<ITourService, TourService>();
        services.AddScoped<ITourExecutionService, TourExecutionService>();
        services.AddScoped<ITourReviewService, TourReviewService>();

    }

    private static void SetupInfrastructure(IServiceCollection services)
    {



        services.AddScoped(typeof(ICrudRepository<TourReview>), typeof(CrudRepository<TourReview, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<Tour>), typeof(CrudRepository<Tour, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<TourExecution>), typeof(CrudRepository<TourExecution, ToursContext>));

        services.AddScoped<ITourReviewRepository, TourReviewRepository>();
        services.AddScoped<ITourRepository, TourRepository>();
        services.AddScoped<ITourExecutionRepository, TourExecutionRepository>();


        services.AddDbContext<ToursContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("tours"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "tours")));
    }

}

