using Microsoft.EntityFrameworkCore;
using Tours.Infrastructure;
using Tours.Api.Startup;
using Tours.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.ConfigureSwagger(builder.Configuration);
builder.Services.ConfigureAuth();
builder.Services.ConfigureToursModule();

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
app.UseAuthorization();

app.MapControllers();

app.Run();


