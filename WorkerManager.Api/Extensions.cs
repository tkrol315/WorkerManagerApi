using Microsoft.EntityFrameworkCore;
using WorkerManager.Infrastructure.EF.Contexts;
using WorkerManager.Infrastructure.EF.Seeder;

namespace WorkerManager.Api
{
    public static class Extensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<WorkerManagerDbContext>();
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
            }
        }

        public static void Seed(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<RoleSeeder>();
            seeder.Seed();

        }
    }
}
