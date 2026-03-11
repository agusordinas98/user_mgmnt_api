using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace UserMgmntAPI.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Guardamos datos de la request
            var method = context.Request.Method;
            var path = context.Request.Path;
            var traceId = context.TraceIdentifier;

            await _next(context); // Pasamos al siguiente middleware

            // Después de ejecutar el resto del pipeline, logueamos la respuesta
            var statusCode = context.Response.StatusCode;

            _logger.LogInformation("TraceId: {TraceId} | Request: {Method} {Path} => {StatusCode}",
                traceId, method, path, statusCode);
        }
    }

}
