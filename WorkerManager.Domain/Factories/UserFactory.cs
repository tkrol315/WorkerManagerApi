using WorkerManager.Domain.Entities;
using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Factories
{
    public class UserFactory : IUserFactory
    {
        public User Create(UserId id, Username username, PasswordHash passwordHash, RoleId roleId)
                => new(id, username, passwordHash, roleId);

    }
}
