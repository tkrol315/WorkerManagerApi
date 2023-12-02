using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkerManager.Application.Authentication;
using WorkerManager.Application.Repositories;
using WorkerManager.Application.Services;
using WorkerManager.Infrastructure.EF.Contexts;
using WorkerManager.Infrastructure.EF.Options;
using WorkerManager.Infrastructure.EF.Repositories;
using WorkerManager.Infrastructure.Services;
using WorkerManager.Shared.options;

namespace WorkerManager.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var postgresOptions = configuration.GetOptions<PostgresOptions>("Postgres");
            services.AddDbContext<WorkerManagerDbContext>(ctx => ctx.UseNpgsql(postgresOptions.ConnectionString));
            services.AddJwt(configuration);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWorkerRepository, WorkerRepository>();
            services.AddScoped<IManagerRepository, ManagerRepository>();
            services.AddScoped<IJwtService, JwtService>();
            return services;
        }
    }
}
