using DCXAirAPI.Application.Cqrs.Journey.Queries;
using DCXAirAPI.Application.Interfaces.Journey;
using DCXAirAPI.Application.Interfaces.Repositories;
using DCXAirAPI.Application.Services.Journey;
using DCXAirAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var Configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetRouteQuery).Assembly));


builder.Services.AddCors();

builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IJsonRepository, JsonRepository>(provider => new JsonRepository(Configuration["RouteJSON"]));
builder.Services.AddScoped<IJourneyService, JourneyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
