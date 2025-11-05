using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LightningArc.Utils.Results.AspNet;

/// <summary>
/// Represents an HTTP result that maps an <see cref="Error"/> instance to a standard "Problem Details" response (RFC 7807).
/// </summary>
/// <param name="error">The error instance to be serialized in the response.</param>
public sealed class ErrorResult(Error error) : IResult
{
    /// <summary>
    /// Executes the response asynchronously, formatting the error and writing it to the HTTP context.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    public Task ExecuteAsync(HttpContext httpContext)
    {
        var mappingService = httpContext.RequestServices.GetRequiredService<ErrorMappingService>();
        var options = httpContext.RequestServices.GetRequiredService<IOptions<EndpointResultOptions>>().Value;

        if (options.ErrorResponseBuilder != null)
        {
            var response = options.ErrorResponseBuilder(error, httpContext);
            return httpContext.Response.WriteAsJsonAsync(response);
        }

        // Default behavior: create a ProblemDetails object.
        var mapping = mappingService.GetMapping(error);
        var statusCode = (int)(mapping?.StatusCode ?? HttpStatusCode.InternalServerError);
        var title = mapping?.Title ?? "An unexpected error has occurred.";
        var problemType = mapping?.Type ?? "about:blank";

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/problem+json";

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = problemType,
            Detail = error.Message,
            Instance = httpContext.Request.Path,
        };

        if (error.Details is { Count: > 0 })
        {
            problemDetails.Extensions["errors"] = error.Details;
        }

        return httpContext.Response.WriteAsJsonAsync(problemDetails);
    }
}