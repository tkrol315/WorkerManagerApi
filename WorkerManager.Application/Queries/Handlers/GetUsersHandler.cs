using MediatR;
using WorkerManager.Application.Services;
using WorkerManager.Domain.Entities;
using WorkerManager.Domain.Repositories;

namespace WorkerManager.Application.Queries.Handlers
{
    public class GetUsersHandler : IRequestHandler<GetUsers, IEnumerable<User>>
    {
        private readonly IUserRepository _repository;

        public GetUsersHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<User>> Handle(GetUsers query, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllAsync();
            return users;
        }
    }
}
