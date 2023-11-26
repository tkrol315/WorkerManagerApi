using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Entities
{
    public class Task
    {
        public TaskId Id { get;}
        public TaskName Name { get; private set; }
        public TaskDescription Description { get; private set; }
        public UserId? WorkerId {  get; set; } 
        public bool IsAssigned { get; set; } = false;
        public bool IsCompleted { get; set; } = false;

        public void UpdateTask(Task task)
        {
            Name = new TaskName(task.Name);
            Description = new TaskDescription(task.Description);
            IsAssigned = task.IsAssigned;
            IsCompleted = task.IsCompleted;
        }


    }
}
