using MediatR;

namespace WorkerManager.Application.Commands
{
    public record RegisterUser(Guid Id, string UserName, string Password, string ConfirmPassword, uint RoleId) : IRequest<Unit>;
}
