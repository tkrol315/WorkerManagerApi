using Microsoft.AspNetCore.Http;
using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Application.Exceptions
{
    public class RoleIdOutOfRangeException : WorkerManagerException
    {
        public RoleIdOutOfRangeException() : base("Role Id must be in range <1,2>.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
