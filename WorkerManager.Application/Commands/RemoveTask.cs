using MediatR;

namespace WorkerManager.Application.Commands
{
    public record RemoveTask(Guid ManagerId, string TaskName) : IRequest<Unit>;
   
}
