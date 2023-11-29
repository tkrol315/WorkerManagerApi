using MediatR;
namespace WorkerManager.Application.Commands
{
    public record AssignTaskToUser(Guid UserId, Guid TaskCreatorId, string TaskName) : IRequest<Unit>;
    
}
