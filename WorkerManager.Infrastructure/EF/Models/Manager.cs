namespace WorkerManager.Infrastructure.EF.Models
{
    public class Manager : User
    {
        public ICollection<Task>? Tasks { get; set; }
    }
}
