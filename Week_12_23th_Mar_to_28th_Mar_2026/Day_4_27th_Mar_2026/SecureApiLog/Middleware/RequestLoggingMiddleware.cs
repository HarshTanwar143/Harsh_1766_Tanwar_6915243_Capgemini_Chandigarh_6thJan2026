using log4net;

namespace SecureApiLog.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ILog log = LogManager.GetLogger("RequestMiddleware");

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            log.Info($"{context.Request.Method} {context.Request.Path} called at {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}");

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                log.Error($"Unhandled exception on {context.Request.Path}", ex);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Internal Server Error");
            }
        }
    }
}
