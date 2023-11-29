using WorkerManager.Domain.Entities;
using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Factories
{
    public class UserFactory : IUserFactory
    {
        public User CreateWorker(UserId id, UserName name, PasswordHash passwordHash, uint roleId)
            => new(id, name, passwordHash, roleId);

        public User CreateManager(UserId id, UserName name, PasswordHash passwordHash, uint roleId, TaskListId taskListId)
            => new(id, name, passwordHash, roleId, taskListId);
    }
}
