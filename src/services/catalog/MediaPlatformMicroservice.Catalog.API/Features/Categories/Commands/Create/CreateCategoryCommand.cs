using MassTransit;
using MediaPlatformMicroservice.Catalog.API.Features.Categories.DTOs;
using MediaPlatformMicroservice.Shared;
using MediatR;

namespace MediaPlatformMicroservice.Catalog.API.Features.Categories.Commands.Create;

public record CreateCategoryCommand(string Name) : IRequest<ServiceResult<CreateCategoryResponse>>;
