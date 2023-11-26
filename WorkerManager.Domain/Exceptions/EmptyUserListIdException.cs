using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Domain.Exceptions
{
    public class EmptyUserListIdException : WorkerManagerException
    {
        public EmptyUserListIdException() : base("User list id cannot be empty.")
        {
        }
    }
}
