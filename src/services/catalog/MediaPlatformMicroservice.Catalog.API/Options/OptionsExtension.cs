using System.Runtime.CompilerServices;

namespace MediaPlatformMicroservice.Catalog.API.Options;

public static class OptionsExtension
{
    public static IServiceCollection AddOptionsExtension(this IServiceCollection services)
    {
        services.AddOptions<MongoOptions>().BindConfiguration(nameof(MongoOptions)).ValidateDataAnnotations();
        return services;
    }
}
