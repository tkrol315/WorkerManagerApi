using MediatR;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Repositories;

namespace WorkerManager.Application.Commands.Handlers
{
    public class RemoveTaskHandler : IRequestHandler<RemoveTask, Unit>
    {
        private readonly IManagerRepository _repository;
        public RemoveTaskHandler(IManagerRepository repository)
        {
           
            _repository = repository;
        }
        public async Task<Unit> Handle(RemoveTask command, CancellationToken cancellationToken)
        {
            var manager = await _repository.GetAsync(command.ManagerId)
                ?? throw new UserNotFoundException(command.ManagerId);

            var task = manager.Tasks.FirstOrDefault(t => t.Name.ToLower() == command.TaskName.ToLower())
                ?? throw new TaskNotFoundException(command.TaskName);

            manager.Tasks.Remove(task);
            await _repository.UpdateAsync(manager);
            return Unit.Value;
        }
    }
}
