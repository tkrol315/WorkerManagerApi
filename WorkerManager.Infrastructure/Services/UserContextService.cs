using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WorkerManager.Application.Services;

namespace WorkerManager.Infrastructure.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? UserId => User is null ? null :
            Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
    }
}
