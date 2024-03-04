using WorkerManager.Domain.Entities;

namespace WorkerManager.Application.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByNameAsync(string username);
        Task<bool> AlreadyExistsByUserNameAsync(string username);
        Task<bool> AlreadyExistsByUserIdAsync(Guid id);
        System.Threading.Tasks.Task AddAsync(User user);
    }
}
