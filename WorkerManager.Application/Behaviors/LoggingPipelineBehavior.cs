using MediatR;
using Microsoft.Extensions.Logging;
using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Application.Behaviors
{
    public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> 
    {
        private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

        public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Starting request {typeof(TRequest).Name} | {DateTime.Now}");

            try
            {
                var result = await next();
                _logger.LogInformation($"Completed request {typeof(TRequest).Name} | {DateTime.Now}");
                return result;
            }
            catch (WorkerManagerException ex)
            {
                _logger.LogError($"Error {typeof(TRequest).Name} | {ex.Message} | {DateTime.Now}");
                return default;
            }

        }
    }
}
