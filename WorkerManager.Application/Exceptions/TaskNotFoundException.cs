﻿using Microsoft.AspNetCore.Http;
using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Application.Exceptions
{
    public class TaskNotFoundException : WorkerManagerException
    {
        public TaskNotFoundException(string taskName) : base($"Task '${taskName}' not found.", StatusCodes.Status404NotFound)
        {
        }
    }
}
