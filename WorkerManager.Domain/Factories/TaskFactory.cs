using WorkerManager.Domain.Entities;
using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Factories
{
    public class TaskFactory : ITaskFactory
    {
        public Entities.Task Create(TaskId id, TaskName name, TaskDescription description, User creator)
            => new(id, name, description, creator);
    }
}
