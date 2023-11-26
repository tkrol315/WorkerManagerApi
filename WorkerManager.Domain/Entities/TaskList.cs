using WorkerManager.Domain.Exceptions;
using WorkerManager.Domain.ValueObjects;

namespace WorkerManager.Domain.Entities
{
    public class TaskList
    {
        public TaskListId Id { get; private set; }

        internal TaskList(TaskListId id)
        {
            Id = id;
        }

        private LinkedList<Task> _tasks = new();

        public Task GetTask(string name)
        {
            var task = _tasks.FirstOrDefault(t => t.Name == name);
            if (task is null)
            {
                throw new TaskNotFoundException(name);
            }
            return task;
        }
        public void AddTask(Task task)
        {
            var alreadyExits = _tasks.Any(t => t.Name == task.Name);
            if (alreadyExits)
            {
                throw new TaskAlreadyExistsException(task.Name);
            }

            _tasks.AddLast(task);
        }
        public void RemoveTask(string taskName)
        {
            var foundTask = GetTask(taskName);

            _tasks.Remove(foundTask);
        }

     
       
    }
}
