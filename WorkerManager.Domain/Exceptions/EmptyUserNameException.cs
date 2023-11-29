using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Domain.Exceptions
{
    public class EmptyUsernameException : WorkerManagerException
    {
        public EmptyUsernameException() : base("User name cannot be empty.")
        {
        }
    }
}
