using System.Diagnostics;
using log4net;

namespace PerformanceLoggingAPI.Middleware;

public class PerformanceMiddleware
{
    private static readonly ILog log = LogManager.GetLogger(typeof(PerformanceMiddleware));
    private readonly RequestDelegate _next;
    private const int SlowThresholdMs = 3000;

    public PerformanceMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var method   = context.Request.Method;
        var endpoint = context.Request.Path;
        var sw       = Stopwatch.StartNew();

        log.Info($"Request started — {method} {endpoint} at {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            sw.Stop();
            log.Error($"Unhandled exception in {method} {endpoint} after {sw.Elapsed.TotalSeconds:F2} sec", ex);
            throw;
        }
        finally
        {
            sw.Stop();
            var ms = sw.ElapsedMilliseconds;

            if (ms > SlowThresholdMs)
                log.Warn($"SLOW REQUEST — {method} {endpoint} took {sw.Elapsed.TotalSeconds:F1} sec (threshold: 3 sec)");
            else
                log.Info($"Request finished — {method} {endpoint} in {sw.Elapsed.TotalSeconds:F2} sec");
        }
    }
}
