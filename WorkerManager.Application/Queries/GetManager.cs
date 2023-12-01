using MediatR;
using WorkerManager.Application.Dto;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Application.Queries
{
    public record GetManager(Guid ManagerId) : IRequest<GetManagerDto>;
   
}
