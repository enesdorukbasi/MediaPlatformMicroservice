using MediaPlatformMicroservice.Catalog.API.Options;
using MediaPlatformMicroservice.Catalog.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddOptionsExtension();
builder.Services.AddDatabaseServiceExtension();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();