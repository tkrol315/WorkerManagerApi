using System.Diagnostics;
using WorkerManager.Domain.Exceptions;

namespace WorkerManager.Domain.ValueObjects
{
    public class TaskDescription
    {
        public string Value { get; }

        public TaskDescription(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyTaskDescriptionException();
            }
            Value = value;
        }
        public static implicit operator TaskDescription(string value)
            =>new(value);
        public static implicit operator string(TaskDescription taskDescription)
            =>taskDescription.Value;
    }
}
