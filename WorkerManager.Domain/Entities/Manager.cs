using WorkerManager.Domain.Exceptions;
using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Entities
{
    public class Manager : User
    {
        private TaskList _taskList = new();
        public Manager(UserId id, UserName name, PasswordHash passwordHash, uint roleId) : base(id, name, passwordHash, roleId)
        {
        }
        public void AssignTaskToWorker(string taskName, Worker worker)
        {
            var foundTask = _taskList.GetTask(taskName);
            if (foundTask.WorkerId is not null)
            {
                throw new TaskAlreadyAssignedException(foundTask.Id);
            }
            worker.SetTask(foundTask);
            foundTask.IsAssigned = true;
           
        }
    }
}
