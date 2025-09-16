using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Npgsql;
using Tours.Api.Mappers;
using Tours.Api.Startup;
using Tours.Core.UseCases.Interfaces;
using Tours.Infrastructure;
using Tours.Infrastructure.Database;
using Tours.Api.GrpcServices;

var builder = WebApplication.CreateBuilder(args);


builder.WebHost.ConfigureKestrel(o =>
{
    o.ListenAnyIP(8887, x => x.Protocols = HttpProtocols.Http2); // gRPC
    o.ListenAnyIP(8080, x => x.Protocols = HttpProtocols.Http1); // REST
});


builder.Services.AddGrpc();


builder.Services.AddControllers();
builder.Services.ConfigureSwagger(builder.Configuration);


builder.Services.ConfigureAuth();


builder.Services.ConfigureToursModule();
builder.Services.AddAutoMapper(typeof(ToursProfile).Assembly);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, Tours.Api.Authentication.CurrentUser>();


var connectionString = DbConnectionStringBuilder.Build("tours");
var dsb = new NpgsqlDataSourceBuilder(connectionString);
dsb.EnableDynamicJson();
var dataSource = dsb.Build();

builder.Services.AddDbContext<ToursContext>(opt =>
    opt.UseNpgsql(dataSource));

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ToursContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapGrpcService<TourExecutionGrpcService>();


app.MapControllers();

app.Run();