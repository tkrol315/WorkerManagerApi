using MediatR;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Services;
using WorkerManager.Domain.Repositories;

namespace WorkerManager.Application.Queries.Handlers
{
    public class GetTasksByManagerIdHandler : IRequestHandler<GetTasksByManagerId, IEnumerable<Domain.Entities.Task>>
    {
        private readonly IUserRepository _repository;

        public GetTasksByManagerIdHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Domain.Entities.Task>> Handle(GetTasksByManagerId query, CancellationToken cancellationToken)
        {
            var manager = await _repository.GetAsync(query.ManagerId);
            if (manager is null)
            {
                throw new UserNotFoundException(query.ManagerId);
            };
            return manager.TaskList.GetTasks();
        }
    }
}
