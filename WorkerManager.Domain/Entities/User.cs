using WorkerManager.Domain.Exceptions;
using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Entities
{
    public class User
    {
        public UserId Id { get; private set; }
        private UserName _name;
        private PasswordHash _passwordHash;
        private TaskList? _taskList;
        protected uint _roleId { get; private set; }
        public Task? _assignedTask { get; private set; }

        internal User(UserId id, UserName name, PasswordHash passwordHash)
        {
            Id = id;
            _name = name;
            _passwordHash = passwordHash;
            _roleId = 0;
        }
        internal User(UserId id, UserName name, PasswordHash passwordHash, TaskListId taskListId)
        {
            Id = id;
            _name = name;
            _passwordHash = passwordHash;
            _roleId = 1;
            _taskList = new(taskListId);
        }

        public void MarkTaskAsCompleted()
        {
           var foundTask = _assignedTask.Creator._taskList.GetTask(_assignedTask.Name);
            foundTask.IsCompleted = true;
            _assignedTask = null;
        }
    
        public void AssignTaskToWorker(string taskName, User user)
        {
            var foundTask = _taskList.GetTask(taskName);
            if (foundTask.AssignedToUserId is not null)
            {
                throw new TaskAlreadyAssignedException(foundTask.Id);
            }
            if (!_assignedTask.IsCompleted && _assignedTask is not null)
            {
                throw new UserTaskAlreadyAssignedException(Id);
            }
            user._assignedTask = foundTask;
            foundTask.IsAssigned = true;
        }
    }
}
