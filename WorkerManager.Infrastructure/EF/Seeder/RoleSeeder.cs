using Microsoft.EntityFrameworkCore;
using WorkerManager.Domain.Entities;
using WorkerManager.Infrastructure.EF.Contexts;

namespace WorkerManager.Infrastructure.EF.Seeder
{
    public class RoleSeeder
    {
        private readonly WorkerManagerDbContext _context;

        public RoleSeeder(WorkerManagerDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task Seed()
        {
            if(!await _context.Roles.AnyAsync())
            {
                var roles = new List<Role>()
                {
                    new()
                    {
                        Id = 1,
                        Name = "Worker"
                    },
                    new()
                    {
                        Id= 2,
                        Name = "Manager"
                    }
                };
                await _context.Roles.AddRangeAsync(roles);
                await _context.SaveChangesAsync();
            }
        }
    }
}
