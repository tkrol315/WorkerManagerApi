using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WorkerManager.Application.Authentication;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
         
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddJwt(configuration);
            return services;
        }
    }
}
