using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Factories;
using WorkerManager.Domain.Entities;
using WorkerManager.Domain.Enums;

namespace WorkerManager.Infrastructure.Factories
{
    public class ManagerFactory : IUserFactory
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly Roles _role;

        public ManagerFactory(IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _role =  Roles.Manager;
        }


        public User CreateUser(RegisterUserDto userDto)
        {
            var manager = _mapper.Map<Manager>(userDto);
            manager.PasswordHash = _passwordHasher.HashPassword(manager,userDto.Password);
            return manager;
        }

        public Roles GetRole()
        {
            return _role;
        }
    }
}
