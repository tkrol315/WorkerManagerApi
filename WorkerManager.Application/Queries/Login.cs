using MediatR;
using WorkerManager.Application.Dto;

namespace WorkerManager.Application.Queries
{
    public record Login(string Username, string Password) : IRequest<JwtTokenDto>;
}
