using WorkerManager.Domain.Entities;

namespace WorkerManager.Application.Services
{
    public interface IUserReadService
    {
        Task<User> getUserByUserName(string userName);
        Task<bool> ExistsByUserName(string userName);
       
    }
}
