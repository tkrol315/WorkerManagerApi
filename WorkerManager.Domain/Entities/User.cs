using WorkerManager.Domain.Exceptions;
using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Entities
{
    public class User
    {
        public UserId Id { get; private set; }
        public Username Username { get; private set; }
        public PasswordHash PasswordHash { get; set; }
        public TaskList? TaskList { get; private set; }
        public uint RoleId { get; private set; }
        public Task? AssignedTask { get; private set; }

        internal User(UserId id, Username username, PasswordHash passwordHash, uint roleId)
        {
            Id = id;
            Username = username;
            RoleId = roleId;
            if(RoleId == 1)
            {
                TaskList = new(Guid.NewGuid());
            }
        }
      

        public void ClearAssignedTask()
        {
            AssignedTask = null;
        }
        public void SetAssignedTask(Task task)
        {
            AssignedTask = task;
        }
        
    }   
}
