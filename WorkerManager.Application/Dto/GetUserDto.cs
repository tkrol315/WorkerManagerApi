using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Application.Dto
{
    public abstract class GetUserDto
    {
        public UserId Id { get; set; }
        public Username Username { get; set; }
        public uint RoleId { get; set; }
    }
}
