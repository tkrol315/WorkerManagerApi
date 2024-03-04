using Microsoft.AspNetCore.Http;
using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Application.Exceptions
{
    public class WorkerHasAlreadyAssignedTaskException : WorkerManagerException
    {
        public WorkerHasAlreadyAssignedTaskException(Guid workerId) : base($"Worker with id '{workerId}' has already assigned task", StatusCodes.Status400BadRequest)
        {
        }
    }
}
