using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerManager.Domain.Exceptions;
using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Entities
{
    public class Worker : User
    {
        public Task _task { get; private set; }
        public Worker(UserId id, UserName name, PasswordHash passwordHash, uint roleId) 
            : base(id, name, passwordHash, roleId)
        {
        }

        public void MarkTaskAsCompleted()
        {
            _task.IsCompleted = true;
        }
        public void SetTask(Task task)
        {
            if(!_task.IsCompleted && _task is not null)
            {
                throw new WorkerTaskAlreadyAssignedException(Id);
            }
            _task = task;
        }
    }
}
