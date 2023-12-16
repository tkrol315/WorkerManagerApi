using MediatR;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Repositories;
using WorkerManager.Application.Services;

namespace WorkerManager.Application.Commands.Handlers
{
    public class AssignTaskHandler : IRequestHandler<AssignTask, Unit>
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IWorkerRepository _workerRepository;
        private readonly IUserContextService _userContextService;


        public AssignTaskHandler(IManagerRepository managerRepository, IWorkerRepository workerRepository,
            IUserContextService userContextService)
        {
            _managerRepository = managerRepository;
            _workerRepository = workerRepository;
            _userContextService = userContextService;
        }

        public async Task<Unit> Handle(AssignTask command, CancellationToken cancellationToken)
        {
            var manager = await _managerRepository.GetAsync(command.ManagerId)
                ?? throw new UserNotFoundException(command.ManagerId);
            var task = manager.Tasks.FirstOrDefault(t => t.Name.ToLower() == command.TaskName.ToLower())
               ?? throw new TaskNotFoundException(command.TaskName);
            if (task.WorkerId is not null)
                throw new TaskAlreadyAssignedException(task.WorkerId);
            var worker = await _workerRepository.GetAsync(command.WorkerId)
                ?? throw new UserNotFoundException(command.WorkerId);
            if (worker.AssignedTask is not null)
                throw new WorkerHasAlreadyAssignedTaskException(worker.Id);
            if (manager.Id != _userContextService.UserId)
                throw new TaskAssignmentNotAllowedException();



            task.TaskStatus = Domain.Enums.TaskStatus.InProgress;
            task.WorkerId = command.WorkerId;
            worker.AssignedTask = task;

            await _workerRepository.UpdateAsync(worker);
            await _managerRepository.UpdateAsync(manager);

            return Unit.Value;
        }
    }
}
