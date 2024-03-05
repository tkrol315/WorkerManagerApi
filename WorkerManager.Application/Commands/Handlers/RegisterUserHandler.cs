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
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserFactoryProviderService _userFactoryProviderService;

        public RegisterUserHandler(IPasswordHasher<User> passwordHasher,
            IMapper mapper, IUserRepository userRepository, IUserFactoryProviderService userFactoryProviderService)
        {
            _passwordHasher = passwordHasher;
            _mapper = mapper;
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

            createdUser.PasswordHash = _passwordHasher.HashPassword(createdUser, command.Dto.Password);
            await _userRepository.AddAsync(createdUser);
            return Unit.Value;
        }
    }
}
