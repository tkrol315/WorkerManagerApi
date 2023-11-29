using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Application.Exceptions
{
    public class UserNotFoundException : WorkerManagerException
    {
        public UserNotFoundException(Guid id) : base($"User with id '{id}' not found.")
        {
        }
    }
}
