using Microsoft.AspNetCore.Http;
using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Application.Exceptions
{
    public class UserWithUserIdAlreadyExistsException : WorkerManagerException
    {
        public UserWithUserIdAlreadyExistsException(Guid id) : base($"User with Id '{id}' already exists. ", StatusCodes.Status400BadRequest){}

     
    }
}
