using WorkerManager.Domain.Entities;
using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Factories
{
    public class TaskListFactory : ITaskListFactory
    {
        public TaskList Create(TaskListId id)
            => new(id);
    }
}
