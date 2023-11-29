using WorkerManager.Domain.Entities;
using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Factories
{
    public interface IUserFactory
    {
        User Create(UserId id, UserName userName, PasswordHash passwordHash, uint roleId);
    }
}
