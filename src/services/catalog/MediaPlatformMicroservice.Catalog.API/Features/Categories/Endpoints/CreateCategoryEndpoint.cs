using MediaPlatformMicroservice.Catalog.API.Features.Categories.Commands.Create;
using MediaPlatformMicroservice.Catalog.API.Features.Categories.DTOs;
using MediaPlatformMicroservice.Shared;
using MediaPlatformMicroservice.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediaPlatformMicroservice.Catalog.API.Features.Categories.Endpoints;

public static class CreateCategoryEndpoint
{
    public static RouteGroupBuilder CreateCategoryGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator, CancellationToken cancellationToken) => 
        (await mediator.Send(command)).ToGenericResult());
        return group;
    }
}
public static class CategoryEndpointExtension
{
    public static void AddCategoryGroupEndpointExtension(this WebApplication app)
    {
        app.MapGroup("app/categories").CreateCategoryGroupItemEndpoint().RequireAuthorization();
    }
}