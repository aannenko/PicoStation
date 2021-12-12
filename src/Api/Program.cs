using Microsoft.AspNetCore.Mvc;
using PicoStation.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<SerialPortServiceOptions>(
    builder.Configuration.GetSection(nameof(SerialPortService)));

builder.Services.Configure<PrometheusServiceOptions>(
    builder.Configuration.GetSection(nameof(PrometheusService)));

builder.Services.AddSingleton<SerialPortService>();
builder.Services.AddSingleton<PrometheusService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/metrics", ([FromServices] PrometheusService service) =>
    service.GetMetrics()).WithName("GetPrometheusMetrics");

app.Run();
