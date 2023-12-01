using MediatR;


namespace WorkerManager.Application.Commands
{
    public record MarkTaskAsCompleted(Guid WorkerId) : IRequest<Unit>;

}
