using Microsoft.AspNetCore.Authorization;

namespace WorkerManager.Api.Authorization
{
    public class IsCreatorRequirement : IAuthorizationRequirement
    {
        public bool IsCreator { get; set; }

        public IsCreatorRequirement(bool isCreator)
            => IsCreator = isCreator;
    }
}
