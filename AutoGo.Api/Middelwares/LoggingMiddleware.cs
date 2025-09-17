using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
            try
            {
                // Log the incoming request
                await LogRequest(context);  

                // Capture the response in a memory stream
                var originalBodyStream = context.Response.Body;
                using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;

                var stopwatch = Stopwatch.StartNew();

                await _next(context); // Continue through the pipeline

                stopwatch.Stop();

                // Log the outgoing response
                await LogResponse(context, stopwatch.ElapsedMilliseconds);

                // Copy the response back to the original stream
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                throw; // rethrow to let exception middleware handle it if needed
            }
        }

        private async Task LogRequest(HttpContext context)
        {
            try
            {
                context.Request.EnableBuffering();

                var builder = new StringBuilder();
                builder.AppendLine("=== Incoming Request ===");
                builder.AppendLine($"Method: {context.Request.Method}");
                builder.AppendLine($"Path: {context.Request.Path}");
                builder.AppendLine($"QueryString: {context.Request.QueryString}");

                if (context.Request.ContentLength > 0)
                {
                    context.Request.Body.Position = 0;
                    using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
                    var body = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                    builder.AppendLine($"Body: {body}");
                }

                _logger.LogInformation(builder.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to log request body.");
            throw;
            }
        }

        private async Task LogResponse(HttpContext context, long elapsedMilliseconds)
        {
            try
            {
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                string text = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                var builder = new StringBuilder();
                builder.AppendLine("=== Outgoing Response ===");
                builder.AppendLine($"Status Code: {context.Response.StatusCode}");
                builder.AppendLine($"Execution Time: {elapsedMilliseconds} ms");
                builder.AppendLine($"Body: {text}");

                _logger.LogInformation(builder.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to log response body.");
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex,
                "An unhandled exception occurred while processing request. Path: {Path}",
                context.Request.Path);

            return Task.CompletedTask;
        }
}

