using MediatR;
using WorkerManager.Application.Dto;

namespace WorkerManager.Application.Queries
{
    public record GetManagersTasks(Guid Id) : IRequest<IEnumerable<GetTaskDto>>;
   
}
