using WorkerManager.Domain.Exceptions;
namespace WorkerManager.Domain.ValueObjects
{
    public record UserListId
    {
        public Guid Value { get; }

        public UserListId(Guid value)
        {
            if(value == Guid.Empty)
            {
                throw new EmptyUserListIdException();
            }
            Value = value;
        }
        public static implicit operator UserListId(Guid value)
            => new(value);
        public static implicit operator Guid(UserListId userListId)
            =>userListId.Value;
    }
}
