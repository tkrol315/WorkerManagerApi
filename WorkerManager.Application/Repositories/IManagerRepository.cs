using WorkerManager.Domain.Entities;

namespace WorkerManager.Application.Repositories
{
    public interface IManagerRepository
    {
        Task<Manager?> GetAsync(Guid id);
        System.Threading.Tasks.Task UpdateAsync(Manager manager);
        Task<IEnumerable<Manager>> GetAllAsync();
    }
}
