using MediaPlatformMicroservice.Catalog.API.Features.Categories;
using MediaPlatformMicroservice.Catalog.API.Features.Courses;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection;

namespace MediaPlatformMicroservice.Catalog.API.Repositories;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Category> Categories { get; set; }

    public static AppDbContext Create(IMongoDatabase database)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseMongoDB
            (database.Client, database.DatabaseNamespace.DatabaseName);
        return new AppDbContext(optionsBuilder.Options);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}