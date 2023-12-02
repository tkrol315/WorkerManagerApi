using MediatR;

namespace WorkerManager.Application.Commands
{
    public record AssignTask(Guid ManagerId, Guid WorkerId, string TaskName) : IRequest<Unit>;
    
}
