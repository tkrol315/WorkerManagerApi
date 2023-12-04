using Microsoft.EntityFrameworkCore;
using WorkerManager.Application.Repositories;
using WorkerManager.Domain.Entities;
using WorkerManager.Infrastructure.EF.Contexts;

namespace WorkerManager.Infrastructure.EF.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly WorkerManagerDbContext _context;

        public WorkerRepository(WorkerManagerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Worker>> GetAllAsync()
            => await _context.Workers
            .Include(w => w.AssignedTask)
            .ToListAsync();

        public async Task<Worker?> GetAsync(Guid id)
            => await _context.Workers
            .Include(u => u.AssignedTask)
            .FirstOrDefaultAsync(u => u.Id == id);

        public async System.Threading.Tasks.Task UpdateAsync(Worker worker)
        {
            _context.Workers.Update(worker);
            await _context.SaveChangesAsync();
        }
    }
}
