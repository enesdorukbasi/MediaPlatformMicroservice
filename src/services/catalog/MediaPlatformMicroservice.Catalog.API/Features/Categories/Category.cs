using MediaPlatformMicroservice.Catalog.API.Features.Courses;
using MediaPlatformMicroservice.Catalog.API.Repositories;

namespace MediaPlatformMicroservice.Catalog.API.Features.Categories;

public class Category : BaseEntity
{
    public string Name { get; set; } = default!;

    public List<Course> Courses { get; set; } = new List<Course>();
}
