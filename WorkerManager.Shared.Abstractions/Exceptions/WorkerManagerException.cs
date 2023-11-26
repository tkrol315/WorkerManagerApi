using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerManager.Shared.Abstractions.Exceptions
{
    public abstract class WorkerManagerException : Exception
    {
        protected WorkerManagerException(string message) : base(message) { }
    }
}
