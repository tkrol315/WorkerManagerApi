using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Domain.Exceptions
{
    public class EmptyUserPasswordHashException : WorkerManagerException
    {
        public EmptyUserPasswordHashException() : base("User password hash cannot be empty.")
        {
        }
    }
}
