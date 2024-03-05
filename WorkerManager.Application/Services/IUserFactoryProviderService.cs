using WorkerManager.Application.Factories;
using WorkerManager.Domain.Enums;

namespace WorkerManager.Application.Services
{
    public interface IUserFactoryProviderService
    {
        IUserFactory GetFactory(Roles role);
    }
}
