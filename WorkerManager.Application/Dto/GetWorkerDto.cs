namespace WorkerManager.Application.Dto
{
    public class GetWorkerDto : GetUserDto
    {
        public Task? AssignedTask { get; set; }
    }
}
