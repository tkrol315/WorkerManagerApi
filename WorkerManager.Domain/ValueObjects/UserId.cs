using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerManager.Domain.Exceptions;

namespace WorkerManager.Domain.ValueObjects
{
    public record UserId
    {
        public Guid Value { get;}

        public UserId(Guid value)
        {
            if(value == Guid.Empty)
            {
                throw new EmptyUserIdException();
            }
            Value = value;
        }
        public static implicit operator UserId(Guid value)
            =>new(value);
        public static implicit operator Guid(UserId userId)
            => userId.Value;
    }
}
