namespace WorkerManager.Domain.Entities
{
    public class Worker : User
    {
        public Task? AssignedTask { get; set; }
        
    }
}
