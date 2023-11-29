using WorkerManager.Domain.Exceptions;

namespace WorkerManager.Domain.ValueObjects
{
    public record Username
    {
        public string Value { get;}

        public Username(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new EmptyUsernameException();
            }
            Value = value;
        }
        public static implicit operator Username(string value)
            =>new(value);
        public static implicit operator string(Username userName)
            =>userName.Value;
    }
}
