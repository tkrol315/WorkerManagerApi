using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkerManager.Infrastructure.EF.Contexts;
using WorkerManager.Infrastructure.EF.Options;
using WorkerManager.Shared.options;

namespace WorkerManager.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var postgresOptions = configuration.GetOptions<PostgresOptions>("Postgres");
            services.AddDbContext<WorkManagerDbContext>(ctx => ctx.UseNpgsql(postgresOptions.ConnectionString));

            return services;
        }
    }
}
