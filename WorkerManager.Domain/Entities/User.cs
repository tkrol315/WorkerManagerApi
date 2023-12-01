using WorkerManager.Domain.Exceptions;
using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Entities
{
    public abstract class User
    {
        public UserId Id { get; set; }
        public Username Username { get; set; }
        public PasswordHash PasswordHash { get; set; }
        public uint RoleId { get; set; }
      
    }   
}
