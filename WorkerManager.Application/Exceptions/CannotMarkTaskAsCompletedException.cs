using Microsoft.AspNetCore.Http;
using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Application.Exceptions
{
    public class CannotMarkTaskAsCompletedException : WorkerManagerException
    {
        public CannotMarkTaskAsCompletedException() 
            : base("Only the creator or assigned worker can mark the task as completed.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
