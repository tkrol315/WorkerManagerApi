using Microsoft.AspNetCore.Authorization;

namespace WorkerManager.Api.Authorization
{
    public static class Extensions
    {
        public static IServiceCollection AddCustomAuthorizaton(this IServiceCollection services) {

            services.AddAuthorization(options =>
            {
                options.AddPolicy("TaskCreator",
                    builder => builder.AddRequirements(new IsCreatorRequirement(true)));
            });
            services.AddScoped<IAuthorizationHandler, IsCreatorRequirementHandler>();

            return services;
        }
    }
}
