using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Entities
{
    public abstract class User
    {
        protected UserId Id { get; }
        private UserName _name;
        private PasswordHash _passwordHash;
        private uint _roleId;
       
        protected User(UserId id, UserName name, PasswordHash passwordHash, uint roleId)
        {
            Id = id;
            _name = name;
            _passwordHash = passwordHash;
            _roleId = roleId;
        }
       
    }
}
