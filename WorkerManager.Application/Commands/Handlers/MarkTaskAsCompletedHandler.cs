using MediatR;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Repositories;
using WorkerManager.Application.Services;

namespace WorkerManager.Application.Commands.Handlers
{
    public class MarkTaskAsCompletedHandler : IRequestHandler<MarkTaskAsCompleted, Unit>
    {
        private readonly IWorkerRepository _repository;
        private readonly IUserContextService _userContextService;

        public MarkTaskAsCompletedHandler(IWorkerRepository repository, IUserContextService userContextService)
        {
            _repository = repository;
            _userContextService = userContextService;
        }

        public async Task<Unit> Handle(MarkTaskAsCompleted command, CancellationToken cancellationToken)
        {
            var worker = await _repository.GetAsync(command.WorkerId)
                ?? throw new UserNotFoundException(command.WorkerId);
            if(worker.AssignedTask is null)
                throw new AssignedTaskNotFoundException();
            if(_userContextService.UserId != worker.Id && _userContextService.UserId != worker.AssignedTask.ManagerId)
                throw new CannotMarkTaskAsCompletedException();
            worker.AssignedTask.TaskStatus = Domain.Enums.TaskStatus.Finished;
            worker.AssignedTask.CompletedByWorkerWithId = worker.Id;
            worker.AssignedTask = null;
            await _repository.UpdateAsync(worker);

            return Unit.Value;
        }
    }
}
