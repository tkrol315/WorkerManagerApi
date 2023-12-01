namespace WorkerManager.Domain.Entities
{
    public class Manager : User
    {
        public List<Task> Tasks { get; set; } = new();
       
    }
}
