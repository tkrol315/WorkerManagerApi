namespace WorkerManager.Domain.Entities
{
    public class Task
    {
        
        public Guid Id { get; set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public User Creator { get;  set; }
        public Guid? AssignedToUserId {  get;   set; }
        public Enums.TaskStatus TaskStatus { get;  set; } = Enums.TaskStatus.NotAssigned;

      
    }
}
