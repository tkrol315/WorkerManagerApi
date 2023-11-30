using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkerManager.Domain.Repositories;
using WorkerManager.Infrastructure.EF.Contexts;
using WorkerManager.Infrastructure.EF.Options;
using WorkerManager.Infrastructure.EF.Repositories;
using WorkerManager.Shared.options;

namespace WorkerManager.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var postgresOptions = configuration.GetOptions<PostgresOptions>("Postgres");
            services.AddDbContext<WorkManagerDbContext>(ctx => ctx.UseNpgsql(postgresOptions.ConnectionString));
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
