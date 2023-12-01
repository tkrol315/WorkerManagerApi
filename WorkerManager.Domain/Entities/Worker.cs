using WorkerManager.Domain.Enums;
using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Entities
{
    public class Worker : User
    {
        public Task? AssignedTask { get; set; }
        
    }
}
