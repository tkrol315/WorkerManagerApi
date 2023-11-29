using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Services;
using WorkerManager.Domain.Entities;
using WorkerManager.Domain.Factories;
using WorkerManager.Domain.Repositories;

namespace WorkerManager.Application.Commands.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUser, Unit>
    {
        private readonly IUserFactory _factory;
        private readonly IUserReadService _readService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserRepository _repository;

        public RegisterUserHandler(IUserFactory factory, IUserReadService readService, IPasswordHasher<User> passwordHasher, IUserRepository repository)
        {
            _factory = factory;
            _readService = readService;
            _passwordHasher = passwordHasher;
            _repository = repository;
        }

        public async Task<Unit> Handle(RegisterUser command, CancellationToken cancellationToken)
        {
            if (await _readService.ExistsByUserName(command.UserName))
            {
                throw new UserWithUserNameAlreadyExistException(command.UserName);
            }
            if(!command.Password.Equals(command.ConfirmPassword))
            {
                throw new PasswordsDontMatchException();
            }
            var createdUser = _factory.Create(command.Id, command.UserName, "NotHashedYet", command.RoleId);
            createdUser.PasswordHash = _passwordHasher.HashPassword(createdUser,command.Password);
            await _repository.AddAsync(createdUser);
            return Unit.Value;
        }
    }
}
