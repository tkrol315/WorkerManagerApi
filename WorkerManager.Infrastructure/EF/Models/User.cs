using System.Reflection.Metadata.Ecma335;

namespace WorkerManager.Infrastructure.EF.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public Guid? AssignedTaskId { get; set; }
        public Task? AssignedTask { get; set; }
        public ICollection<Task>? Tasks { get; set; }
    }
}
