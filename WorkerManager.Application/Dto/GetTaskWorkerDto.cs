using WorkerManager.Domain.Enums;

namespace WorkerManager.Application.Dto
{
    public class GetTaskWorkerDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Domain.Enums.TaskStatus TaskStatus { get; set; } = Domain.Enums.TaskStatus.NotAssigned;
    }
}
