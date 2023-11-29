using WorkerManager.Domain.Entities;

namespace WorkerManager.Application.Services
{
    public interface IUserReadService
    {
        Task<User> GetManagerWithTaskList(Guid id);
        Task<User> GetWorkerWithAssignedTask(Guid id);
        Task<User> getUserByUserName(string userName);
        Task<bool> ExistsByUserName(string userName);
        Task<IEnumerable<User>> GetUsers();
    }
}
