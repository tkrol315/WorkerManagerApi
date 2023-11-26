using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Domain.Exceptions
{
    public class EmptyUserNameException : WorkerManagerException
    {
        public EmptyUserNameException() : base("User name cannot be empty.")
        {
        }
    }
}
