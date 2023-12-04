using TaskStatus = WorkerManager.Domain.Enums.TaskStatus;
namespace WorkerManager.Application.Dto
{
    public class GetTaskManagerDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? WorkerId { get; set; }
        public Guid? CompletedByWorkerWithId { get; set; }
        public TaskStatus TaskStatus { get; set; } = TaskStatus.NotAssigned;
    }
}
