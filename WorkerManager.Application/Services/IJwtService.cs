using WorkerManager.Domain.Entities;

namespace WorkerManager.Application.Services
{
    public interface IJwtService
    {
        string GetJwtToken(User user);
    }
}
