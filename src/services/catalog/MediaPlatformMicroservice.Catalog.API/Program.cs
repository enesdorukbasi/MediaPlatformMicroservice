using MediaPlatformMicroservice.Catalog.API;
using MediaPlatformMicroservice.Catalog.API.Features.Categories.Commands.Create;
using MediaPlatformMicroservice.Catalog.API.Features.Categories.Endpoints;
using MediaPlatformMicroservice.Catalog.API.Options;
using MediaPlatformMicroservice.Catalog.API.Repositories;
using MediaPlatformMicroservice.Shared.Extensions;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddOptionsExtension();
builder.Services.AddDatabaseServiceExtension();
builder.Services.AddCommonServiceExtension(typeof(CatalogAssembly));

var app = builder.Build();

app.AddCategoryGroupEndpointExtension();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();