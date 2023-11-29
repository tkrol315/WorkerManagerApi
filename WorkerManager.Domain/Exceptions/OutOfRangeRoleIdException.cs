using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Domain.Exceptions
{
    public class OutOfRangeRoleIdException : WorkerManagerException
    {
        public OutOfRangeRoleIdException() : base("Role Id must be in range <0,1>.")
        {
        }
    }
}
