using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Domain.Exceptions
{
    public class EmptyTaskListIdException : WorkerManagerException
    {
        public EmptyTaskListIdException() : base("Task list id cannot be empty.")
        {
        }
    }
}
