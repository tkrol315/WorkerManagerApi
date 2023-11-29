using MediatR;
using WorkerManager.Application.Services;

namespace WorkerManager.Application.Queries.Handlers
{
    public class GetTaskByNameAndManagerIdHandler : IRequestHandler<GetTaskByNameAndManagerId, Domain.Entities.Task>
    {
        private readonly IUserReadService _readService;

        public GetTaskByNameAndManagerIdHandler(IUserReadService readService)
        {
            _readService = readService;
        }

        public async Task<Domain.Entities.Task> Handle(GetTaskByNameAndManagerId query, CancellationToken cancellationToken)
        {
            var manager = await _readService.GetManagerWithTaskList(query.ManagerId);
            var task = manager.TaskList.GetTask(query.TaskName);
            return task;
        }
    }
}
