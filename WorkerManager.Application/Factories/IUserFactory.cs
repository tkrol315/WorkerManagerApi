using WorkerManager.Application.Dto;
using WorkerManager.Domain.Entities;
using WorkerManager.Domain.Enums;

namespace WorkerManager.Application.Factories
{
    public interface IUserFactory
    {
        User CreateUser(RegisterUserDto userDto);
        Roles GetRole();
    }
}
