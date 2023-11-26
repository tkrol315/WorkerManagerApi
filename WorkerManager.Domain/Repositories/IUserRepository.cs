using WorkerManager.Domain.Entities;
using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(UserId id);
        System.Threading.Tasks.Task AddAsync(User user);
        System.Threading.Tasks.Task UpdateAsync(User user);
        System.Threading.Tasks.Task RemoveAsync(User user);
    }
}
