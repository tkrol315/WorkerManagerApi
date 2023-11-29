using MediatR;

namespace WorkerManager.Application.Queries
{
    public record Login(string Username, string Password) : IRequest<string>
    {
    }
}
