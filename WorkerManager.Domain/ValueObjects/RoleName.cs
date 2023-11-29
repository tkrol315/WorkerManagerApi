using WorkerManager.Domain.Exceptions;

namespace WorkerManager.Domain.ValueObjects
{
    public class RoleName
    {
        public string Value { get; set; }

        public RoleName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyRoleNameException();
            }
            Value = value;
        }
        public static implicit operator RoleName(string value)
            =>new(value);
        public static implicit operator string(RoleName value)
            =>value.Value;
    }
}
