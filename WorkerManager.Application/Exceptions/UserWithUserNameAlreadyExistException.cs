using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Application.Exceptions
{
    public class UserWithUserNameAlreadyExistException : WorkerManagerException
    {
        public UserWithUserNameAlreadyExistException(string userName) : base($"User with user name '{userName}' already exists. ")
        {
        }
    }
}
