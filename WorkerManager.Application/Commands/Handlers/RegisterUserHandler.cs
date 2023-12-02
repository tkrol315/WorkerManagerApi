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
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public RegisterUserHandler(IPasswordHasher<User> passwordHasher, IUserRepository repository, IMapper mapper)
        {
            _passwordHasher = passwordHasher;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(RegisterUser command, CancellationToken cancellationToken)
        {
            if (await _repository.AlreadyExistsByUserNameAsync(command.Dto.Username))
            {
                throw new UserWithUserNameAlreadyExistException(command.Dto.Username);
            }
            if(!command.Dto.Password.Equals(command.Dto.ConfirmPassword))
            {
                throw new PasswordsDontMatchException();
            }
            var createdUser = _mapper.Map<User>(command.Dto);
            createdUser.PasswordHash = _passwordHasher.HashPassword(createdUser,command.Dto.Password);
            await _repository.AddAsync(createdUser);
            return Unit.Value;
        }
    }
}
