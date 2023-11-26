using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Domain.Exceptions
{
    public class EmptyTaskIdException : WorkerManagerException
    {
        public EmptyTaskIdException() : base("Task id cannot be empty.")
        {
        }
    }
}
