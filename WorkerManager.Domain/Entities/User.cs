namespace WorkerManager.Domain.Entities
{
    public abstract class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }
        public uint RoleId { get; set; }
      
    }   
}
