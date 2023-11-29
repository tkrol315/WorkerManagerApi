using MediatR;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Application.Queries
{
    public record GetTaskByNameAndManagerId(Guid ManagerId, string TaskName) : IRequest<Domain.Entities.Task>;
}
