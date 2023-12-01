using MediatR;
using WorkerManager.Application.Dto;

namespace WorkerManager.Application.Queries
{
    public record GetWorker(Guid Id) : IRequest<GetWorkerDto>;
   
}
