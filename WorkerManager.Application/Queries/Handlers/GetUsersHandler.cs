using MediatR;
using WorkerManager.Application.Services;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Application.Queries.Handlers
{
    public class GetUsersHandler : IRequestHandler<GetUsers, IEnumerable<User>>
    {
        private readonly IUserReadService _readService;

        public GetUsersHandler(IUserReadService readService)
        {
            _readService = readService;
        }

        public async Task<IEnumerable<User>> Handle(GetUsers query, CancellationToken cancellationToken)
        {
            var users = await _readService.GetUsers();
            return users;
        }
    }
}
