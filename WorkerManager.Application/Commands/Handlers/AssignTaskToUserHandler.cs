using MediatR;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Services;
using WorkerManager.Domain.Repositories;

namespace WorkerManager.Application.Commands.Handlers
{
    public class AssignTaskToUserHandler : IRequestHandler<AssignTaskToUser, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserReadService _readService;

        public AssignTaskToUserHandler(IUserRepository userRepository, IUserReadService readService)
        {
            _userRepository = userRepository;
            _readService = readService;
        }

        public async Task<Unit> Handle(AssignTaskToUser command, CancellationToken cancellationToken)
        {
            var worker = await _readService.GetWorkerWithAssignedTask(command.UserId);
            if (worker is null)
            {
                throw new UserNotFoundException(command.UserId);
            }
            var creator = await _readService.GetManagerWithTaskList(command.TaskCreatorId);
            var task = creator.TaskList.GetTask(command.TaskName);
            if (task.AssignedToUserId is not null)
            {
                throw new TaskAlreadyAssignedException(task.Id);
            }
            if (!worker.AssignedTask.IsCompleted && worker.AssignedTask is not null)
            {
                throw new UserAlreadyAssignedToTaskException(worker.Id);
            }
            task.SetAssignedUser(worker.Id);
            worker.SetAssignedTask(task);
            await _userRepository.UpdateAsync(worker);
            await _userRepository.UpdateAsync(creator);
            return Unit.Value;
        }
    }
}
