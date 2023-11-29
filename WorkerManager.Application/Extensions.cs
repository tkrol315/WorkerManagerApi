using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WorkerManager.Application.Authentication;
using WorkerManager.Domain.Entities;
using WorkerManager.Domain.Factories;

namespace WorkerManager.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ITaskListFactory, TaskListFactory>();
            services.AddSingleton<ITaskFactory, Domain.Factories.TaskFactory>();
            services.AddSingleton<IUserFactory, UserFactory>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddJwt(configuration);
            return services;
        }
    }
}
