using WorkerManager.Domain.Entities;
using WorkerManager.Domain.Repositories;
using WorkerManager.Infrastructure.EF.Contexts;

namespace WorkerManager.Infrastructure.EF.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WorkManagerDbContext _context;

        public UserRepository(WorkManagerDbContext context)
        {
            _context = context;
        }

        public System.Threading.Tasks.Task AddAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AlreadyExistsByUserNameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByUserNameAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}
