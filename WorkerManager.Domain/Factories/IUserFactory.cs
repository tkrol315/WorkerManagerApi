using WorkerManager.Domain.Entities;
using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Factories
{
    public interface IUserFactory
    {
        User Create(UserId id, Username username, PasswordHash passwordHash, RoleId roleId);
    }
}
