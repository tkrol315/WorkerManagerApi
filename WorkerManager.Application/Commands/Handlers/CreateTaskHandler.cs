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
      

        public CreateTaskHandler(ITaskFactory factory, IUserRepository repository)
        {
            _factory = factory;
            _repository = repository;
          
        }

        public async Task<Unit> Handle(CreateTask command, CancellationToken cancellationToken)
        {
            var ( id, name, description, creatorId ) = command;
            var creator = await _repository.GetAsync(creatorId);
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
