using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Entities
{
    public class Task
    {
        
        public TaskId Id { get;}
        public TaskName Name { get; private set; }
        public TaskDescription Description { get; private set; }
        public User Creator { get; private set; }
        public UserId? AssignedToUserId {  get;  private set; } 
        public bool IsAssigned { get; private set; } = false;
        public bool IsCompleted { get; private set; } = false;

        internal Task(TaskId id, TaskName name, TaskDescription description, User creator)
        {
            Id = id;
            Name = name;
            Description = description;
            Creator = creator;
        }
        public void SetAssignedUser(UserId id)
        {
            AssignedToUserId = id;
            IsAssigned = true;
        }
        public void SetTaskAsCompleted()
        {
            IsCompleted = true;
        }
      
    }
}
