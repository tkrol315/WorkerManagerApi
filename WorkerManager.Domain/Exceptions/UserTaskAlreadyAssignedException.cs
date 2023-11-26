using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Domain.Exceptions
{
    public class UserTaskAlreadyAssignedException : WorkerManagerException
    {
        public UserTaskAlreadyAssignedException(Guid workerId) : base($"User '{workerId}' has already assigned task")
        {
        }
    }
}
