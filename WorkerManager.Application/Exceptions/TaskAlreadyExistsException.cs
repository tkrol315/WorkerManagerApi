using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Application.Exceptions
{
    public class TaskAlreadyExistsException : WorkerManagerException
    {
        public TaskAlreadyExistsException(Guid id, string taskName) 
            : base($"Manager with id '{id}' already have task named '{taskName}'")
        {
        }
    }
}
