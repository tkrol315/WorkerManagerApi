using WorkerManager.Domain.Exceptions;

namespace WorkerManager.Domain.ValueObjects
{
    public record UserName
    {
        public string Value { get;}

        public UserName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyUserNameException();
            }
            Value = value;
        }
        public static implicit operator UserName(string value)
            =>new(value);
        public static implicit operator string(UserName userName)
            =>userName.Value;
    }
}
