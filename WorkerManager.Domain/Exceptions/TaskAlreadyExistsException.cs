using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Domain.Exceptions
{
    public class TaskAlreadyExistsException : WorkerManagerException
    {
        public TaskAlreadyExistsException(string name) : base($"Task with name '{name}' already exists")
        {
        }
    }
}
