using Microsoft.AspNetCore.Http;
using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Application.Exceptions
{
    public class UserAlreadyAssignedToTaskException : WorkerManagerException
    {
        public UserAlreadyAssignedToTaskException(Guid workerId) : base($"User '{workerId}' has already assigned task", StatusCodes.Status400BadRequest)
        {
        }
    }
}
