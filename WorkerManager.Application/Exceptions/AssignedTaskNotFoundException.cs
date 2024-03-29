﻿using Microsoft.AspNetCore.Http;
using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Application.Exceptions
{
    public class AssignedTaskNotFoundException : WorkerManagerException
    {
        public AssignedTaskNotFoundException() : base("Assigned task not found.",StatusCodes.Status404NotFound)
        {
        }
    }
}
