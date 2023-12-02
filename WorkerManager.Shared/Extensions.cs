using Microsoft.Extensions.DependencyInjection;
using WorkerManager.Shared.Services;

namespace WorkerManager.Shared
{
    public static class Extensions
    {
        public static IServiceCollection AddShared(this IServiceCollection services)
        {
            services.AddHostedService<AppInitializer>();
            return services;
        }
    }
}
