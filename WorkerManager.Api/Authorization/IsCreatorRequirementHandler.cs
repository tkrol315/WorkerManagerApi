using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace WorkerManager.Api.Authorization
{
    public class IsCreatorRequirementHandler : AuthorizationHandler<IsCreatorRequirement, Domain.Entities.Task>
    {
        protected override System.Threading.Tasks.Task HandleRequirementAsync(AuthorizationHandlerContext context,
            IsCreatorRequirement requirement, Domain.Entities.Task task)
        {
            var userId = Guid.Parse(context.User
                .FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            if(userId == task.ManagerId)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
