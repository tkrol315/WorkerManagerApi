using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Entities
{
    public class Role
    {
        public RoleId Id { get; private set; }
        public RoleName Name { get; private set; }

    }
}
