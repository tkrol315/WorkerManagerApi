using TaskStatus = WorkerManager.Domain.Enums.TaskStatus;
namespace WorkerManager.Application.Dto
{
    public class GetTaskDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? AssignedToUserId { get; set; }
        public TaskStatus TaskStatus { get; set; } = TaskStatus.NotAssigned;
    }
}
