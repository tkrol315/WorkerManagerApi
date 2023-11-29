using MediatR;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Application.Queries
{
  
    public record GetUsers : IRequest<IEnumerable<User>>;
}
