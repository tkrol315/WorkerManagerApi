using MediatR;
using WorkerManager.Application.Dto;

namespace WorkerManager.Application.Commands
{
    public record CreateTask(Guid Id, CreateTaskDto taskDto) : IRequest<Unit>;
}
