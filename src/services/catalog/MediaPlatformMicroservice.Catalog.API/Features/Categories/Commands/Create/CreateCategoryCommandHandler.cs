using MediaPlatformMicroservice.Catalog.API.Features.Categories.DTOs;
using MediaPlatformMicroservice.Catalog.API.Repositories;
using MediaPlatformMicroservice.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MediaPlatformMicroservice.Catalog.API.Features.Categories.Commands.Create;

public class CreateCategoryCommandHandler(AppDbContext dbContext) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
{
    public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var isExists = await dbContext.Categories.AnyAsync(x => x.Name == request.Name, cancellationToken);
        if (isExists)
            return ServiceResult<CreateCategoryResponse>.Error("Category already exists.", $"The category name '{request.Name}' already exists.", HttpStatusCode.BadRequest);

        var category = new Category { Name = request.Name };
        await dbContext.Categories.AddAsync(category, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id), "");
    }
}