using Microsoft.AspNetCore.Http;
using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Application.Exceptions
{
    public class UserIsNotCreatorException : WorkerManagerException
    {
        public UserIsNotCreatorException() 
            : base($"You cannot modify / remove tasks created by other managers.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
