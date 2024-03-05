using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Reflection;
using WorkerManager.Application.Exceptions;
using WorkerManager.Application.Factories;
using WorkerManager.Application.Services;
using WorkerManager.Domain.Entities;
using WorkerManager.Domain.Enums;

namespace WorkerManager.Infrastructure.Services
{
    public class UserFactoryProviderService : IUserFactoryProviderService
    {
        private readonly Dictionary<Roles, IUserFactory> _factories = new();
        public UserFactoryProviderService(IMapper mapper,IPasswordHasher<User> passwordHasher)
        {
            var factoryTypes = Assembly.GetExecutingAssembly()
                .GetTypes().Where(t => 
                typeof(IUserFactory).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);


            foreach (var factoryType in factoryTypes)
            {
                var factory = Activator.CreateInstance(factoryType,mapper,passwordHasher) as IUserFactory;
                _factories.Add(factory.GetRole(), factory);
            }
        }
        public IUserFactory GetFactory(Roles role)
        {
            if (_factories.TryGetValue(role, out var factory))
            {
                return factory;
            }

            throw new RoleIdOutOfRangeException();
        }
    }
}
