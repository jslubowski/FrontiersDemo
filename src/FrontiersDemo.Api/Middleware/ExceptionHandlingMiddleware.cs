using FrontiersDemo.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using ValidationException = FrontiersDemo.Application.Common.Exceptions.ValidationException;

namespace FrontiersDemo.Api.Middleware;

public sealed class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            logger.LogInformation("Validation failed: {Errors}", ex.Errors);
            var problem = new ValidationProblemDetails(ex.Errors.ToDictionary(kv => kv.Key, kv => kv.Value))
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation failed."
            };
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (NotFoundException ex)
        {
            logger.LogInformation("Not found: {Message}", ex.Message);
            var problem = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Resource not found.",
                Detail = ex.Message
            };
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (BadHttpRequestException ex)
        {
            logger.LogInformation("Bad request: {Message}", ex.Message);
            var problem = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid request body.",
                Detail = "The request body could not be parsed. Please check that all fields have the correct types and values."
            };
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
