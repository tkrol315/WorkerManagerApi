using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Application.Exceptions
{
    public class TaskAlreadyAssignedException : WorkerManagerException
    {
        public TaskAlreadyAssignedException(Guid TaskId) : base($"Task '{TaskId} have already assigned worker'")
        {
        }
    }
}
