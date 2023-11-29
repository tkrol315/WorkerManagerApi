using MediatR;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Application.Queries
{
    public record GetTasksByManagerId(Guid ManagerId) : IRequest<IEnumerable<Domain.Entities.Task>>;
   
}
