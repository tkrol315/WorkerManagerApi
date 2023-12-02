namespace WorkerManager.Application.Dto
{
    public abstract class GetUserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public uint RoleId { get; set; }
    }
}
