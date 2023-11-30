using MediatR;
using WorkerManager.Application.Services;
using WorkerManager.Domain.Repositories;

namespace WorkerManager.Application.Commands.Handlers
{
    public class MarkTaskAsCompletedHandler : IRequestHandler<MarkTaskAsCompleted, Unit>
    {
        private readonly IUserRepository _repository;
       

        public MarkTaskAsCompletedHandler(IUserRepository repository)
        {
            _repository = repository;
           
        }

        public async Task<Unit> Handle(MarkTaskAsCompleted command, CancellationToken cancellationToken)
        {
            var worker = await _repository.GetAsync(command.WorkerId);
            var creator = await _repository.GetAsync(command.CreatorId);
            var task = creator.TaskList.GetTask(command.TaskName);
            task.SetTaskAsCompleted();
            worker.ClearAssignedTask();
            await _repository.UpdateAsync(worker);
            await _repository.UpdateAsync(creator);
            return Unit.Value;
        }
    }
}
