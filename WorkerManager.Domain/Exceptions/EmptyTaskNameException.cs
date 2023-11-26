using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Domain.Exceptions
{
    public class EmptyTaskNameException : WorkerManagerException
    {
        public EmptyTaskNameException() : base("Task name cannot be empty.")
        {
        }
    }
}
