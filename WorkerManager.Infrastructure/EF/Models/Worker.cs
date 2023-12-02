namespace WorkerManager.Infrastructure.EF.Models
{
    public class Worker : User
    {
        public Guid? AssignedTaskId { get; set; }
        public Task? AssignedTask { get; set; }
    }
}
