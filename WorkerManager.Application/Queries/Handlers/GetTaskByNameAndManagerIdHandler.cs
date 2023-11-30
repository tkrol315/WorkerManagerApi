using MediatR;
using WorkerManager.Application.Services;
using WorkerManager.Domain.Repositories;

namespace WorkerManager.Application.Queries.Handlers
{
    public class GetTaskByNameAndManagerIdHandler : IRequestHandler<GetTaskByNameAndManagerId, Domain.Entities.Task>
    {
        private readonly IUserRepository _repository;

        public GetTaskByNameAndManagerIdHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Entities.Task> Handle(GetTaskByNameAndManagerId query, CancellationToken cancellationToken)
        {
            var manager = await _repository.GetAsync(query.ManagerId);
            var task = manager.TaskList.GetTask(query.TaskName);
            return task;
        }
    }
}
