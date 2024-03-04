using Microsoft.AspNetCore.Http;
using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Application.Exceptions
{
    public class PasswordsDontMatchException : WorkerManagerException
    {
        public PasswordsDontMatchException() : base("'Password' don't match 'ConfirmPassword'.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
