using WorkerManager.Domain.ValueObjects;
using TaskStatus = WorkerManager.Domain.Enums.TaskStatus;
namespace WorkerManager.Application.Dto
{
    public class GetTaskDto
    {
        public TaskName Name { get; set; }
        public TaskDescription Description { get; set; }
        public UserId? AssignedToUserId { get; set; }
        public TaskStatus TaskStatus { get; set; } = TaskStatus.NotAssigned;
    }
}
