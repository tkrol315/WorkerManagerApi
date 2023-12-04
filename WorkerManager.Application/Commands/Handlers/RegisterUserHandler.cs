using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Repositories;
using WorkerManager.Domain.Entities;

namespace WorkerManager.Application.Commands.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUser, Unit>
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public RegisterUserHandler(IPasswordHasher<User> passwordHasher,
            IMapper mapper, IUserRepository userRepository)
        {
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(RegisterUser command, CancellationToken cancellationToken)
        {
            if (await _userRepository.AlreadyExistsByUserNameAsync(command.Dto.Username))
            {
                throw new UserWithUserNameAlreadyExistException(command.Dto.Username);
            }
            if(!command.Dto.Password.Equals(command.Dto.ConfirmPassword))
            {
                throw new PasswordsDontMatchException();
            }

            User createdUser;
            if(command.Dto.RoleId == 1)
            {
                createdUser = _mapper.Map<Worker>(command.Dto);
            }
            else if(command.Dto.RoleId == 2)
            {
                createdUser = _mapper.Map<Manager>(command.Dto);
            }
            else
            {
                throw new RoleIdOutOfRangeException();
            }
            createdUser.PasswordHash = _passwordHasher.HashPassword(createdUser, command.Dto.Password);
            await _userRepository.AddAsync(createdUser);
            return Unit.Value;
        }
    }
}
