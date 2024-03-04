using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using WorkerManager.Shared.Abstractions.Exceptions;

namespace WorkerManager.Shared.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
               _logger.LogInformation($"Starting request {context.Request.Path} | {DateTime.Now}");
                await next.Invoke(context);
            }
            catch (WorkerManagerException ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                context.Response.Headers.Add("content-type", "application/json");
                var errorCode = ToUnderscoreCase(ex.GetType().Name.Replace("Exception", string.Empty));
                var json = JsonSerializer.Serialize(new {ErrorCode = errorCode, ex.Message});
                _logger.LogError($"Error {context.Request.Path} | {ex.Message} | {DateTime.Now}");
                await context.Response.WriteAsync(json);
            }
            catch(Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.Headers.Add("content-type", "application/json");
                var json = JsonSerializer.Serialize("Something went wrong");
                _logger.LogError(ex.ToString(), ex.Message);
                await context.Response.WriteAsync(json);
            }
        }

        public static string ToUnderscoreCase(string value)
            => string.Concat((value ?? string.Empty)
                .Select((x, i) => i > 0 && char.IsUpper(x) &&
                !char.IsUpper(value[i - 1]) ? $"_{x}" : x.ToString())).ToLower();
    }
}
