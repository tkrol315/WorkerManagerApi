using WorkerManager.Domain.Entities;

namespace WorkerManager.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUserNameAsync(string username);
        Task<bool> AlreadyExistsByUserNameAsync(string username);
        System.Threading.Tasks.Task AddAsync(User user);
    }
}
