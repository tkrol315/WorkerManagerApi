using MediatR;
using WorkerManager.Application.Services;
using WorkerManager.Domain.Repositories;

namespace WorkerManager.Application.Commands.Handlers
{
    public class MarkTaskAsCompletedHandler : IRequestHandler<MarkTaskAsCompleted, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserReadService _readService;

        public MarkTaskAsCompletedHandler(IUserRepository userRepository, IUserReadService readService)
        {
            _userRepository = userRepository;
            _readService = readService;
        }

        public async Task<Unit> Handle(MarkTaskAsCompleted command, CancellationToken cancellationToken)
        {
            var worker = await _readService.GetWorkerWithAssignedTask(command.WorkerId);
            var creator = await _readService.GetManagerWithTaskList(command.CreatorId);
            var task = creator.TaskList.GetTask(command.TaskName);
            task.SetTaskAsCompleted();
            worker.ClearAssignedTask();
            await _userRepository.UpdateAsync(worker);
            await _userRepository.UpdateAsync(creator);
            return Unit.Value;
        }
    }
}
