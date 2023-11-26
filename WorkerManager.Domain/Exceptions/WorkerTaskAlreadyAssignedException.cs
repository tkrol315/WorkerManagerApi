using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Domain.Exceptions
{
    public class WorkerTaskAlreadyAssignedException : WorkerManagerException
    {
        public WorkerTaskAlreadyAssignedException(Guid workerId) : base($"Worker '{workerId}' has already assigned task")
        {
        }
    }
}
