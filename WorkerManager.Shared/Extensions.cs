using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace WorkerManager.Shared
{
    public static class Extensions
    {
        public static IServiceCollection AddShared(this IServiceCollection services)
        {
           services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
