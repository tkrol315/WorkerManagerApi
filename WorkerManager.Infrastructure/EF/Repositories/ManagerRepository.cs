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

        public async Task<IEnumerable<Manager>> GetAllAsync()
            => await _context.Managers
            .Include(m => m.Tasks)
            .ToListAsync();

        public async Task<Manager?> GetAsync(Guid id)
            => await _context.Managers
            .Include(u => u.Tasks)
            .FirstOrDefaultAsync(u => u.Id == id);

        public async System.Threading.Tasks.Task UpdateAsync(Manager manager)
        {
            _context.Managers.Update(manager);
            await _context.SaveChangesAsync();
        }
    }
}
