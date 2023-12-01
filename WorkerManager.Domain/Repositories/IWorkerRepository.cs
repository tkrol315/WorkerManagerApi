using WorkerManager.Domain.Entities;

namespace WorkerManager.Domain.Repositories
{
    public interface IWorkerRepository
    {
        Task<Worker> GetAsync(Guid id);
        System.Threading.Tasks.Task UpdateAsync(Worker worker);
    }
}
