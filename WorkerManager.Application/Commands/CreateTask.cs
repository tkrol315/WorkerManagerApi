using MediatR;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Application.Commands
{
    public record CreateTask(Guid Id, string Name, string Description, Guid CreatorId) : IRequest<Unit>;
}
