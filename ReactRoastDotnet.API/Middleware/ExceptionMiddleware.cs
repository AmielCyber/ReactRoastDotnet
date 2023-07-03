using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace ReactRoastDotnet.API.Middleware;

/// <summary>
/// Our global middleware that will catch errors and logged them.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = 500;

            var response = new ProblemDetails
            {
                Title = _env.IsDevelopment() ? e.Message : "Server Error",
                // Only show stack trace in development.
                Detail = _env.IsDevelopment() ? e.StackTrace : "An error occurred while processing your request.",
                Status = (int)HttpStatusCode.InternalServerError,
            };


            // We are outside of our api controller so we need to specify JsonNamingPolicy
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }
    }
}