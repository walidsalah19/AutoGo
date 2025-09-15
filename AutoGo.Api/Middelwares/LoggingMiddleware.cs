using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Log Request
        try
        {
            // Log Request
            await LogRequest(context);

            // Measure execution time
            var stopwatch = Stopwatch.StartNew();
            await _next(context); // Call the next middleware
            stopwatch.Stop();

            // Log Response
            await LogResponse(context, stopwatch.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task LogRequest(HttpContext context)
    {
        context.Request.EnableBuffering(); // Allows multiple reads of the body

        var builder = new StringBuilder();
        builder.AppendLine("=== Incoming Request ===");
        builder.AppendLine($"Method: {context.Request.Method}");
        builder.AppendLine($"Path: {context.Request.Path}");
        builder.AppendLine($"QueryString: {context.Request.QueryString}");

        // Read and log request body
        if (context.Request.ContentLength > 0 && context.Request.Body.CanRead)
        {
            context.Request.Body.Position = 0;
            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
            builder.AppendLine($"Body: {body}");
        }
       
        _logger.LogInformation(builder.ToString());
    }

    private async Task LogResponse(HttpContext context, long elapsedMilliseconds)
    {
        var builder = new StringBuilder();
        builder.AppendLine("=== Outgoing Response ===");
        builder.AppendLine($"Status Code: {context.Response.StatusCode}");
        builder.AppendLine($"Execution Time: {elapsedMilliseconds}ms");

        _logger.LogInformation(builder.ToString());
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        _logger.LogError(ex, $"An unhandled exception occurred while processing the request. Path: {context.Request.Path}");

        // يمكن ترجع JSON بشكل مرتب للمستخدم
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var result = System.Text.Json.JsonSerializer.Serialize(new
        {
            error = "An unexpected error occurred. Please try again later.",
            details = ex.Message // لو عاوز تظهر التفاصيل للمطور بس مش للمستخدم النهائي
        });

        await context.Response.WriteAsync(result);
    }
}

