using MediaPlatformMicroservice.Catalog.API.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MediaPlatformMicroservice.Catalog.API.Repositories;

public static class RepositoryExtension
{
    public static IServiceCollection AddDatabaseServiceExtension(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient, MongoClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MongoOptions>>().Value;
            return new MongoClient(options.ConnectionString);
        });

        services.AddScoped(sp =>
        {
            var mongoClient = sp.GetRequiredService<IMongoClient>();
            var options = sp.GetRequiredService<MongoOptions>();
            return AppDbContext.Create(mongoClient.GetDatabase(options.DatabaseName));
        });
        return services;
    }
}
