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

        public void Seed()
        {
            if(!_context.Roles.Any())
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
                _context.Roles.AddRange(roles);
                _context.SaveChanges();
            }
        }
    }
}
