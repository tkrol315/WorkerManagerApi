using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerManager.Domain.Exceptions;

namespace WorkerManager.Domain.ValueObjects
{
    public record TaskId
    {
        public Guid Value { get; }

        public TaskId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyTaskIdException();
            }
            Value = value;
        }
        public static implicit operator Guid(TaskId taskId)
            =>taskId.Value;
        public static implicit operator TaskId(Guid value)
            => new(value);
    }
}
