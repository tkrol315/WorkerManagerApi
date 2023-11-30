using Microsoft.EntityFrameworkCore;
using WorkerManager.Domain.Entities;
using WorkerManager.Domain.Repositories;
using WorkerManager.Domain.ValueObjects;
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

        public async System.Threading.Tasks.Task AddAsync(User user)
            => await _context.Users.AddAsync(user);

        public async Task<IEnumerable<User>> GetAllAsync()
            => _context.Users;

        public async Task<User> GetAsync(UserId id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            return user.RoleId == 0 
                ? await _context.Users.Include(u => u.AssignedTask).FirstOrDefaultAsync(u => u.Id == id)
                : await _context.Users.Include(u => u.TaskList).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async System.Threading.Tasks.Task RemoveAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
