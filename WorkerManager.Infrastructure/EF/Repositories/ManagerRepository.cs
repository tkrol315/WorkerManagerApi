using Microsoft.EntityFrameworkCore;
using WorkerManager.Application.Repositories;
using WorkerManager.Domain.Entities;
using WorkerManager.Infrastructure.EF.Contexts;

namespace WorkerManager.Infrastructure.EF.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly WorkerManagerDbContext _context;

        public ManagerRepository(WorkerManagerDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(Guid id)
            => await _context.Users
            .AnyAsync(u => u.Id == id && u.RoleId == 1);

        public async Task<Manager?> GetAsync(Guid id)
            => await _context.Users
            .OfType<Manager>()
            .Include(u => u.Tasks)
            .FirstOrDefaultAsync(u => u.Id == id && u.RoleId == 1);

        public async System.Threading.Tasks.Task UpdateAsync(Manager manager)
        {
            _context.Update(manager);
            await _context.SaveChangesAsync();
        }
    }
}
