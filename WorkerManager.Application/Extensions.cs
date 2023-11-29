using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WorkerManager.Domain.Entities;
using WorkerManager.Domain.Factories;

namespace WorkerManager.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddSingleton<ITaskListFactory, TaskListFactory>();
            services.AddSingleton<ITaskFactory, Domain.Factories.TaskFactory>();
            services.AddSingleton<IUserFactory, UserFactory>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            return services;
        }
    }
}
