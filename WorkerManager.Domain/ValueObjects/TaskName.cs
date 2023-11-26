using WorkerManager.Domain.Exceptions;

namespace WorkerManager.Domain.ValueObjects
{
    public record TaskName
    {
        public string Value { get;}

        public TaskName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyTaskNameException();
            }
            Value = value;
        }
        public static implicit operator TaskName(string value)
            => new(value);
        public static implicit operator string(TaskName taskName)
            => taskName.Value;
    }
}
