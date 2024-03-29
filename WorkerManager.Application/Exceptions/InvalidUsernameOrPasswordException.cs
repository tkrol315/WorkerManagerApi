﻿using Microsoft.AspNetCore.Http;
using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Application.Exceptions
{
    public class InvalidUsernameOrPasswordException : WorkerManagerException
    {
        public InvalidUsernameOrPasswordException() : base("Invalid username or password.", StatusCodes.Status400BadRequest)
        {
        }
    }
}
