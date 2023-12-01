using MediatR;
using WorkerManager.Application.Dto;

namespace WorkerManager.Application.Commands
{
    public record RemoveTask(Guid ManagerId, string TaskName) : IRequest<Unit>;
   
}
