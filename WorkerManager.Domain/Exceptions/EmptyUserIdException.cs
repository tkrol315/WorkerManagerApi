using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Domain.Exceptions
{
    public class EmptyUserIdException : WorkerManagerException
    {
        public EmptyUserIdException() : base("User Id cannot be empty.")
        {
        }
    }
}
