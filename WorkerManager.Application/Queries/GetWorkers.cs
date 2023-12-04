using MediatR;
using WorkerManager.Application.Dto;

namespace WorkerManager.Application.Queries
{
    public record GetWorkers : IRequest<IEnumerable<GetWorkerDto>>;
  
}
