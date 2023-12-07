using System.Security.Claims;

namespace WorkerManager.Application.Services
{
    public interface IUserContextService
    {
        Guid? UserId { get; }
        ClaimsPrincipal? User { get; }
    }
}
