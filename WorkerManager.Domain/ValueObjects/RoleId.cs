using System.Net.Http.Headers;
using WorkerManager.Domain.Exceptions;

namespace WorkerManager.Domain.ValueObjects
{
    public class RoleId
    {
        public uint Value { get; }
        public RoleId(uint value)
        {
            if(value is < 0 or > 1)
            {
                throw new OutOfRangeRoleIdException();
            }
            Value = value;
        }
        public static implicit operator RoleId(uint value)
            => new(value);
        public static implicit operator uint(RoleId roleId) 
            => roleId.Value;
    }
}
