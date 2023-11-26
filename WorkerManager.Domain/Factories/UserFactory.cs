using WorkerManager.Domain.Entities;
using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Factories
{
    public class UserFactory : IUserFactory
    {
        public User CreateWorker(UserId id, UserName name, PasswordHash passwordHash)
            => new(id, name, passwordHash);

        public User CreateManager(UserId id, UserName name, PasswordHash passwordHash, TaskListId taskListId)
            => new(id, name, passwordHash, taskListId);
    }
}
