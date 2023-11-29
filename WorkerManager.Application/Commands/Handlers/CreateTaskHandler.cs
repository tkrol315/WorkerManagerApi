using MediatR;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Services;
using WorkerManager.Domain.Factories;
using WorkerManager.Domain.Repositories;

namespace WorkerManager.Application.Commands.Handlers
{
    public class CreateTaskHandler : IRequestHandler<CreateTask, Unit>
    {
        private readonly ITaskFactory _factory;
        private readonly IUserRepository _repository;
        private readonly IUserReadService _readService;

        public CreateTaskHandler(ITaskFactory factory, IUserRepository repository, IUserReadService readService)
        {
            _factory = factory;
            _repository = repository;
            _readService = readService;
        }

        public async Task<Unit> Handle(CreateTask command, CancellationToken cancellationToken)
        {
            var ( id, name, description, creatorId ) = command;
            var creator = await _readService.GetManagerWithTaskList(creatorId);
            if (creator is null)
            {
                throw new UserNotFoundException(creatorId);
            }
            var newTask = _factory.Create(id, name, description, creator);
            creator.TaskList.AddTask(newTask);
            await _repository.UpdateAsync(creator);
            return Unit.Value;
        }
    }
}
