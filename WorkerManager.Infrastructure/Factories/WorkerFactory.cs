using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Factories;
using WorkerManager.Domain.Entities;
using WorkerManager.Domain.Enums;

namespace WorkerManager.Infrastructure.Factories
{
    public class WorkerFactory : IUserFactory
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly Roles _role;
        public Roles Role{ get; init; }
        public WorkerFactory(IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _role = Roles.Worker;
        }

        public User CreateUser(RegisterUserDto userDto)
        {
            var worker = _mapper.Map<Worker>(userDto);
            worker.PasswordHash = _passwordHasher.HashPassword(worker,userDto.Password);
            return worker;
        }

        public Roles GetRole()
        {
            return _role;
        }
    }
}
