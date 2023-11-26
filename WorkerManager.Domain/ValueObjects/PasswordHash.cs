using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerManager.Domain.Exceptions;

namespace WorkerManager.Domain.ValueObjects
{
    public record PasswordHash
    {
        public string Value { get;}

        public PasswordHash(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyUserPasswordHashException();
            }
            Value = value;
        }

        public static implicit operator PasswordHash(string value)
            => new(value);

        public static implicit operator string(PasswordHash passwordHash)
            =>passwordHash.Value;
    }
}
