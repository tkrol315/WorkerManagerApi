using MediatR;
using WorkerManager.Application.Dto;

namespace WorkerManager.Application.Queries
{
    public record GetManager(Guid ManagerId) : IRequest<GetManagerDto>;
   
}
