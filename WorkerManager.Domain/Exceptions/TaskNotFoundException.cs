using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Domain.Exceptions
{
    public class TaskNotFoundException : WorkerManagerException
    {
        public TaskNotFoundException(string name) : base($"Task with name '{name}' not found.")
        {
        }
    }
}
