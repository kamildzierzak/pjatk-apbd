using System.Net;

namespace Exercise10.API.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // Log the error details for better debugging
            _logger.LogError(ex, "An unhandled exception occurred.");

            // Handle the error and respond accordingly
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Set appropriate status code based on exception type
        context.Response.ContentType = "application/json";

        if (exception is UnauthorizedAccessException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
        else if (exception is ArgumentException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        var response = new
        {
            error = new
            {
                message = exception.Message,
                detail = exception.StackTrace
            }
        };

        var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);

        return context.Response.WriteAsync(jsonResponse);
    }
}
