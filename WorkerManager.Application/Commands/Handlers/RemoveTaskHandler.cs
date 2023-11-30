using MediatR;
using System.Runtime.InteropServices;
using WorkerManager.Application.Services;
using WorkerManager.Domain.Repositories;

namespace WorkerManager.Application.Commands.Handlers
{
    public class RemoveTaskHandler : IRequestHandler<RemoveTask, Unit>
    {
        private readonly IUserRepository _repository;
        public RemoveTaskHandler(IUserRepository repository)
        {
           
            _repository = repository;
        }
        public async Task<Unit> Handle(RemoveTask command, CancellationToken cancellationToken)
        {
            var creator = await _repository.GetAsync(command.ManagerId);
            creator.TaskList.RemoveTask(command.TaskName);
            await _repository.UpdateAsync(creator);
            return Unit.Value;
        }
    }
}
