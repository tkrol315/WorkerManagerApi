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

        public async Task<Worker?> GetAsync(Guid id)
            => await _context.Users
            .OfType<Worker>()
            .Include(u => u.AssignedTask)
            .FirstOrDefaultAsync(u => u.Id == id && u.RoleId == 0);

        public async System.Threading.Tasks.Task UpdateAsync(Worker worker)
        {
            _context.Users.Update(worker);
            await _context.SaveChangesAsync();
        }
    }
}
