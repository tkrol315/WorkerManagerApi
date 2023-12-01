namespace WorkerManager.Application.Dto
{
    public class RegisterUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public uint RoleId { get; set; }
    }
}
