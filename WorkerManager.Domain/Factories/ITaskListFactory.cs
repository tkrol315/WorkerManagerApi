using WorkerManager.Domain.Entities;
using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Factories
{
    public interface ITaskListFactory
    {
        TaskList Create(TaskListId id);
    }
}
