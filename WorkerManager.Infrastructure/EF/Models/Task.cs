namespace WorkerManager.Infrastructure.EF.Models
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? WorkerId { get; set; }
        public Worker? Worker { get; set; }
        public Guid ManagerId { get; set; }
        public Manager Manager { get; set; }
      
    }
}
