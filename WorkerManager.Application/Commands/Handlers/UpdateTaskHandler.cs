using MediatR;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Repositories;

namespace WorkerManager.Application.Commands.Handlers
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTask, Unit>
    {
        private readonly IManagerRepository _managerRepository;

        public UpdateTaskHandler(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        public async Task<Unit> Handle(UpdateTask command, CancellationToken cancellationToken)
        {
            var manager = await _managerRepository.GetAsync(command.managerId)
                  ?? throw new UserNotFoundException(command.managerId);
            var task = manager.Tasks.FirstOrDefault(t => t.Name.ToLower() == command.taskName.ToLower());
            if(task is null)
            {
                throw new TaskNotFoundException(command.taskName);
            }
            if(task.Name.ToLower()  == command.dto.Name.ToLower() ||
                 manager.Tasks.Any(t => t.Name.ToLower() == command.dto.Name.ToLower()))
            {
                throw new TaskAlreadyExistsException(command.managerId, command.dto.Name);
            }

            task.Name = command.dto.Name;
            task.Description = command.dto.Description;
            await _managerRepository.UpdateAsync(manager);
            return Unit.Value;
        }
    }
}
