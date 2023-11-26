using WorkerManager.Domain.Entities;
using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Factories
{
    public interface IUserFactory
    {
        User CreateWorker(UserId id, UserName name, PasswordHash passwordHash);
        User CreateManager(UserId id, UserName name, PasswordHash passwordHash, TaskListId taskListId);
    }
}
