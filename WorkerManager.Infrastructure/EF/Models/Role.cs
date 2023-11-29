namespace WorkerManager.Infrastructure.EF.Models
{
    public class Role
    {
        public uint Id { get; set; }
        public string Name{ get; set; }
        public ICollection<User> Users { get; set; }
    }
}
