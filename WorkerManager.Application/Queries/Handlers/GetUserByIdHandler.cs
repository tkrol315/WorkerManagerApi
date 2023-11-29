using MediatR;
using WorkerManager.Application.Exceptions;
using WorkerManager.Domain.Entities;
using WorkerManager.Domain.Repositories;

namespace WorkerManager.Application.Queries.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserById, User>
    {
        private readonly IUserRepository _repository;

        public GetUserByIdHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> Handle(GetUserById query, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAsync(query.UserId);
            if (user is null)
            {
                throw new UserNotFoundException(query.UserId);
            }
            return user;
        }
    }
}
