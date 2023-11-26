using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Domain.Exceptions
{
    public class EmptyUserListGuidException : WorkerManagerException
    {
        public EmptyUserListGuidException() : base("User list id cannot be empty.")
        {
        }
    }
}
