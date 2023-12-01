using WorkerManager.Domain.Enums;
using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Entities
{
    public class Task
    {
        
        public TaskId Id { get; set; }
        public TaskName Name { get;  set; }
        public TaskDescription Description { get;  set; }
        public User Creator { get;  set; }
        public UserId? AssignedToUserId {  get;   set; }
        public Enums.TaskStatus TaskStatus { get;  set; } = Enums.TaskStatus.NotAssigned;

      
    }
}
