using MediatR;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Repositories;
using WorkerManager.Application.Services;

namespace WorkerManager.Application.Commands.Handlers
{
    public class RemoveTaskHandler : IRequestHandler<RemoveTask, Unit>
    {
        private readonly IManagerRepository _repository;
        private readonly IUserContextService _userContextService;
        public RemoveTaskHandler(IManagerRepository repository, IUserContextService userContextService)
        {

            _repository = repository;
            _userContextService = userContextService;
        }
        public async Task<Unit> Handle(RemoveTask command, CancellationToken cancellationToken)
        {
            var manager = await _repository.GetAsync(command.ManagerId)
                ?? throw new UserNotFoundException(command.ManagerId);

            var task = manager.Tasks.FirstOrDefault(t => t.Name.ToLower() == command.TaskName.ToLower())
                ?? throw new TaskNotFoundException(command.TaskName);

            if (manager.Id != _userContextService.UserId)
                throw new UserIsNotCreatorException();
            
            manager.Tasks.Remove(task);
            await _repository.UpdateAsync(manager);
            return Unit.Value;
        }
    }
}
