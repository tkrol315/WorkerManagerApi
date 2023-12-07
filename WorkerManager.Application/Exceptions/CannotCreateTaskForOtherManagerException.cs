using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Application.Exceptions
{
    public class CannotCreateTaskForOtherManagerException : WorkerManagerException
    {
        public CannotCreateTaskForOtherManagerException(Guid authorizedManagerId, Guid managerId )
            : base($"Manager with id '{authorizedManagerId}' cannot create task for manager with id '{managerId}'.")
        {
        }
    }
}
