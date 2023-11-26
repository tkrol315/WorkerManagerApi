using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Entities
{
    public class Task
    {
        
        public TaskId Id { get;}
        public TaskName Name { get; private set; }
        public TaskDescription Description { get; private set; }
        public User Creator { get; private set; }
        public UserId? AssignedToUserId {  get;  set; } 
        public bool IsAssigned { get; set; } = false;
        public bool IsCompleted { get; set; } = false;

        internal Task(TaskId id, TaskName name, TaskDescription description, User creator)
        {
            Id = id;
            Name = name;
            Description = description;
            Creator = creator;
        }
    }
}
