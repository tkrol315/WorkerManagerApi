using WorkerManager.Domain.ValueObjects;
namespace WorkerManager.Domain.Repositories
{
    public interface ITaskRepository
    {
        Task<WorkerManager.Domain.Entities.Task> GetAsync(TaskId id);
        Task AddAsync(WorkerManager.Domain.Entities.Task task);
        Task UpdateAsync(WorkerManager.Domain.Entities.Task task);
        Task DeleteAsync(WorkerManager.Domain.Entities.Task task);

    }
}
