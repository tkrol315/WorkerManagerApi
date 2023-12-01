using MediatR;
using WorkerManager.Application.Dto;

namespace WorkerManager.Application.Queries
{
    public record GetManagersTask(Guid Id, string TaskName) : IRequest<GetTaskDto>;
    
}
