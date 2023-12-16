using AutoMapper;
using MediatR;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Repositories;
using WorkerManager.Application.Services;

namespace WorkerManager.Application.Commands.Handlers
{
    public class CreateTaskHandler : IRequestHandler<CreateTask, Unit>
    {
        private readonly IManagerRepository _repository;
        private readonly IUserContextService _userContextService;

        public CreateTaskHandler(IManagerRepository repository, IUserContextService userContextService)
        {
            _repository = repository;
            _userContextService = userContextService;
        }

        public async Task<Unit> Handle(CreateTask command, CancellationToken cancellationToken)
        {
            var manager = await _repository.GetAsync(command.Id)
                ?? throw new UserNotFoundException(command.Id);
            if(manager.Tasks.Any(t => t.Name.ToLower() == command.taskDto.Name.ToLower()))
            {
                throw new TaskAlreadyExistsException(command.Id, command.taskDto.Name); 
            }
            var newTask = new Domain.Entities.Task()
            {
                Name = command.taskDto.Name,
                Description = command.taskDto.Description,
            };
            if(manager.Id != _userContextService.UserId)
                throw new CannotCreateTaskForOtherManagerException((Guid)_userContextService.UserId, manager.Id);


            newTask.Manager = manager;
            newTask.TaskStatus = Domain.Enums.TaskStatus.NotAssigned;
            manager.Tasks.Add(newTask);
            await _repository.UpdateAsync(manager);
            return Unit.Value;
        }
    }
}
