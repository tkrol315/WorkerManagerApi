using MediatR;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Application.Queries
{
    public record GetUserById(Guid UserId) : IRequest<User>;
}
