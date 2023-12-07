using Microsoft.EntityFrameworkCore;
using WorkerManager.Application.Repositories;
using WorkerManager.Domain.Entities;
using WorkerManager.Infrastructure.EF.Contexts;

namespace WorkerManager.Infrastructure.EF.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WorkerManagerDbContext _context;

        public UserRepository(WorkerManagerDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task AddAsync(User user)
        {
           await _context.Users.AddAsync(user);
           await _context.SaveChangesAsync();
        }

        public async Task<bool> AlreadyExistsByUserNameAsync(string username)
            => await _context.Users
            .AnyAsync(u => u.Username == username);
        

        public async Task<User?> GetUserByNameAsync(string username)
            => await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Username == username);
    }
}
