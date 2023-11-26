using WorkerManager.Domain.Entities;
using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Factories
{
    public interface ITaskFactory
    {
        Entities.Task Create(TaskId id, TaskName name, TaskDescription description, User creator);
    }
}
