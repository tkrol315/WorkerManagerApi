using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Repositories;
using WorkerManager.Application.Services;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Application.Queries.Handlers
{
    public class LoginHandler : IRequestHandler<Login, JwtTokenDto>
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;

        public LoginHandler(IUserRepository repository, IPasswordHasher<User> passwordHasher, IJwtService jwtService)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<JwtTokenDto> Handle(Login query, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByNameAsync(query.Username)
                ?? throw new InvalidUsernameOrPasswordException();

            var passwordValidationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, query.Password);
            if (passwordValidationResult is PasswordVerificationResult.Failed)
            {
                throw new InvalidUsernameOrPasswordException();
            }
            var token = _jwtService.GetJwtToken(user);
            return new JwtTokenDto()
            {
                Token = token
            };
        }
    }
}
