using Microsoft.AspNetCore.Http;
using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Application.Exceptions
{
    public class UserWithUsernameAlreadyExistException : WorkerManagerException
    {
        public UserWithUsernameAlreadyExistException(string userName) : base($"User with username '{userName}' already exists. ", StatusCodes.Status400BadRequest)
        {
        }
    }
}
