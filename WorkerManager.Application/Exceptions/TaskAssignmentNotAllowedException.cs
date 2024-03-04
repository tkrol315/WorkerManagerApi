using Microsoft.AspNetCore.Http;
using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Application.Exceptions
{
    public class TaskAssignmentNotAllowedException : WorkerManagerException
    {
        public TaskAssignmentNotAllowedException() : base("You cannot assign task that you didn't created.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
