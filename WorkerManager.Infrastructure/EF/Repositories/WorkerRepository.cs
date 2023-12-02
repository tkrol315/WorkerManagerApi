using WorkerManager.Application.Repositories;
using WorkerManager.Domain.Entities;
using WorkerManager.Infrastructure.EF.Contexts;

namespace WorkerManager.Infrastructure.EF.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly WorkManagerDbContext _context;

        public WorkerRepository(WorkManagerDbContext context)
        {
            _context = context;
        }

        public async Task<Worker?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }



        public System.Threading.Tasks.Task UpdateAsync(Worker worker)
        {
            throw new NotImplementedException();
        }
    }
}
