using WorkerManager.Domain.Exceptions;

namespace WorkerManager.Domain.ValueObjects
{
    public record TaskListId
    {
        public Guid Value { get; }

        public TaskListId(Guid value)
        {
            if(value == Guid.Empty)
            {
                throw new EmptyTaskListIdException();
            }
            Value = value;
        }
        public static implicit operator Guid(TaskListId taskListId) 
            => taskListId.Value;
        public static implicit operator TaskListId(Guid value)
            => new(value);
    }
}
