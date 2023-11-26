using WorkerManager.Domain.Entities;
using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Repositories
{
    public interface ITaskListRepository
    {
        Task<TaskList> GetAsync(TaskListId id);
        System.Threading.Tasks.Task AddAsync(TaskList taskList);
        System.Threading.Tasks.Task UpdateAsync(TaskList taskList);
        System.Threading.Tasks.Task RemoveAsync(TaskList taskList);
    }
}
