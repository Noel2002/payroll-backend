namespace DotNetTest.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string requestMethod = context.Request.Method;
        string requestPath = context.Request.Path;

        await _next(context);

        int statusCode = context.Response.StatusCode;
        _logger.LogInformation("{Method} {Path} {StatusCode}", requestMethod, requestPath, statusCode);
    }
}