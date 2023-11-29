using MediatR;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Application.Commands
{
    public record MarkTaskAsCompleted(Guid WorkerId, Guid CreatorId, string TaskName) : IRequest<Unit>;

}
