using AutoGo.Application.Common.Result;
using AutoGo.Application.Common.Result;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace AutoGo.Api.Middelwares
{
    public static class GlobelHandler
    {
        public static void ExceptionHandling(this IApplicationBuilder builder)
        {
            builder.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = exceptionFeature?.Error;
                    var realException = exception is AggregateException agg ? agg.InnerException : exception;

                    object response;
                    int statusCode;

                    if (realException is FluentValidation.ValidationException validationException)
                    {
                        statusCode = (int)HttpStatusCode.BadRequest;
                        var errors = validationException.Errors.Select(error => new
                        {
                            message = error.ErrorMessage,
                            property = error.PropertyName
                        });

                        response = new
                        {
                            isSuccessed = false,
                            errors
                        };
                    }
                    else
                    {
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        response = new
                        {
                            isSuccessed = false,
                            error = realException?.Message
                        };
                    }

                    context.Response.StatusCode = statusCode;

                    var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                    await context.Response.WriteAsync(json);
                });
            });
        }
    }

}
