using MediatR;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Services;

namespace WorkerManager.Application.Queries.Handlers
{
    public class GetTasksByManagerIdHandler : IRequestHandler<GetTasksByManagerId, IEnumerable<Domain.Entities.Task>>
    {
        private readonly IUserReadService _readService;

        public GetTasksByManagerIdHandler(IUserReadService readService)
        {
            _readService = readService;
        }

        public async Task<IEnumerable<Domain.Entities.Task>> Handle(GetTasksByManagerId query, CancellationToken cancellationToken)
        {
            var manager = await _readService.GetManagerWithTaskList(query.ManagerId);
            if (manager is null)
            {
                throw new UserNotFoundException(query.ManagerId);
            };
            return manager.TaskList.GetTasks();
        }
    }
}
