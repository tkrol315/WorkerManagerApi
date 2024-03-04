namespace WorkerManager.Shared.Abstractions.Exceptions
{
    public abstract class WorkerManagerException : Exception
    {
        public int StatusCode { get; set; }
        protected WorkerManagerException(string message,int statusCode) : base(message) { StatusCode = statusCode; }
    }
}
