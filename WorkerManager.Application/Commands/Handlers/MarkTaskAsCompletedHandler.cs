using MediatR;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Repositories;

namespace WorkerManager.Application.Commands.Handlers
{
    public class MarkTaskAsCompletedHandler : IRequestHandler<MarkTaskAsCompleted, Unit>
    {
        private readonly IWorkerRepository _repository;
       

        public MarkTaskAsCompletedHandler(IWorkerRepository repository)
        {
            _repository = repository;
           
        }

        public async Task<Unit> Handle(MarkTaskAsCompleted command, CancellationToken cancellationToken)
        {
            var worker = await _repository.GetAsync(command.WorkerId)
                ?? throw new UserNotFoundException(command.WorkerId);
            if(worker.AssignedTask is null)
                throw new AssignedTaskNotFoundException();
            worker.AssignedTask.TaskStatus = Domain.Enums.TaskStatus.Finished;
            await _repository.UpdateAsync(worker);

            return Unit.Value;
        }
    }
}
