namespace WorkerManager.Application.Dto
{
    public class GetManagerDto : GetUserDto
    {
        public List<GetTaskDto> Tasks { get; set; }
    }
}
