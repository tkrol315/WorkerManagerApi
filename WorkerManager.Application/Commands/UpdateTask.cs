using MediatR;
using WorkerManager.Application.Dto;

namespace WorkerManager.Application.Commands
{
    public record UpdateTask(Guid managerId, string taskName, UpdateTaskDto dto) : IRequest<Unit>;
}
