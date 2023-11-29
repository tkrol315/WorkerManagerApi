using MediatR;
using System.Runtime.InteropServices;
using WorkerManager.Application.Services;
using WorkerManager.Domain.Repositories;

namespace WorkerManager.Application.Commands.Handlers
{
    public class RemoveTaskHandler : IRequestHandler<RemoveTask, Unit>
    {
        private readonly IUserRepository _repository;
        private readonly IUserReadService _readService;
        public RemoveTaskHandler(IUserReadService readService, IUserRepository repository)
        {
            _readService = readService;
            _repository = repository;
        }
        public async Task<Unit> Handle(RemoveTask command, CancellationToken cancellationToken)
        {
            var creator = await _readService.GetManagerWithTaskList(command.ManagerId);
            creator.TaskList.RemoveTask(command.TaskName);
            await _repository.UpdateAsync(creator);
            return Unit.Value;
        }
    }
}
