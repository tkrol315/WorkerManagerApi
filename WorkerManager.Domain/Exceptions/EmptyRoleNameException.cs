using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Domain.Exceptions
{
    public class EmptyRoleNameException : WorkerManagerException
    {
        public EmptyRoleNameException() : base("Role name cannot be empty.")
        {
        }
    }
}
