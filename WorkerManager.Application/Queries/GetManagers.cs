using MediatR;
using WorkerManager.Application.Dto;

namespace WorkerManager.Application.Queries
{
    public record GetManagers : IRequest<IEnumerable<GetManagerDto>>;
    
}
