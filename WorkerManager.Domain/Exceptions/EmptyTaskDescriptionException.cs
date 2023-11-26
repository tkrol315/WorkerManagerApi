using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Domain.Exceptions
{
    public class EmptyTaskDescriptionException : WorkerManagerException
    {
        public EmptyTaskDescriptionException() : base("Task description cannot be empty.")
        {
        }
    }
}
