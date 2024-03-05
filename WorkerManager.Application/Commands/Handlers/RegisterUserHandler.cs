using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Repositories;
using WorkerManager.Application.Services;
using WorkerManager.Domain.Entities;
using WorkerManager.Domain.Enums;

namespace WorkerManager.Application.Commands.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUser, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFactoryProviderService _userFactoryProviderService;

        public RegisterUserHandler(IUserRepository userRepository, IUserFactoryProviderService userFactoryProviderService)
        {
            _userRepository = userRepository;
            _userFactoryProviderService = userFactoryProviderService;
        }

        public async Task<Unit> Handle(RegisterUser command, CancellationToken cancellationToken)
        {
            if (await _userRepository.AlreadyExistsByUserIdAsync(command.Dto.Id))
                throw new UserWithUserIdAlreadyExistsException(command.Dto.Id);

            if (await _userRepository.AlreadyExistsByUserNameAsync(command.Dto.Username))
                throw new UserWithUsernameAlreadyExistException(command.Dto.Username);

            if (!command.Dto.Password.Equals(command.Dto.ConfirmPassword))
                throw new PasswordsDontMatchException();


            var userFactory = _userFactoryProviderService.GetFactory((Roles)command.Dto.RoleId);
            var createdUser = userFactory.CreateUser(command.Dto);

            await _userRepository.AddAsync(createdUser);
            return Unit.Value;
        }
    }
}
