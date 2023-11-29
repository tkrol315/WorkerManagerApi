namespace WorkerManager.Infrastructure.EF.Models
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public Guid CreatorId { get; set; }
        public User Creator { get; set; }
      
    }
}
