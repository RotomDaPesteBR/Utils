using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net;
using Utils.AspNet.Results.Results.Success;

namespace LightningArc.Utils.Results.AspNet;

/// <summary>
/// Represents an HTTP result that maps a <see cref="Success"/> instance to a standard success response.
/// </summary>
/// <param name="success">The success instance to be mapped.</param>
public sealed class SuccessResult(Success success) : IResult
{
    /// <summary>
    /// Executes the response asynchronously, formatting the success result and writing it to the HTTP context.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    public Task ExecuteAsync(HttpContext httpContext)
    {
        var mappingService = httpContext.RequestServices.GetRequiredService<SuccessMappingService>();
        var options = httpContext.RequestServices.GetRequiredService<IOptions<EndpointResultOptions>>().Value;

        var mapping = mappingService.GetMapping(success);
        var statusCode = (int)(mapping?.StatusCode ?? HttpStatusCode.OK);

        httpContext.Response.StatusCode = statusCode;

        if (statusCode == (int)HttpStatusCode.NoContent)
        {
            return Task.CompletedTask;
        }

        if (options.WrapSuccessResponses && options.SuccessResponseBuilder != null)
        {
            var successDetails = new SuccessDetail
            {
                Status = mapping?.StatusCode ?? HttpStatusCode.OK,
                Message = success.Message ?? "",
                Data = null
            };

            var response = options.SuccessResponseBuilder(successDetails);
            return httpContext.Response.WriteAsJsonAsync(response);
        }

        return Task.CompletedTask;
    }
}

/// <summary>
/// Represents an HTTP result that maps a <see cref="Success{TValue}"/> instance and a value to a standard success response.
/// </summary>
/// <typeparam name="TValue">The type of the success value to be returned in the response.</typeparam>
public sealed class SuccessResult<TValue>(Success<TValue> success, string? contentType = null)
    : IResult
{
    /// <summary>
    /// Executes the response asynchronously, formatting the success result with the value and writing it to the HTTP context.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    public Task ExecuteAsync(HttpContext httpContext)
    {
        var mappingService = httpContext.RequestServices.GetRequiredService<SuccessMappingService>();
        var options = httpContext.RequestServices.GetRequiredService<IOptions<EndpointResultOptions>>().Value;

        var mapping = mappingService.GetMapping(success);
        var statusCode = (int)(mapping?.StatusCode ?? HttpStatusCode.OK);

        httpContext.Response.StatusCode = statusCode;

        if (statusCode == (int)HttpStatusCode.NoContent)
        {
            return Task.CompletedTask;
        }

        if (contentType is not null)
        {
            if (contentType.Contains("text/") && !contentType.Contains("charset"))
            {
                httpContext.Response.ContentType = $"{contentType}; charset={options.DefaultCharset}";
            }
            else
            {
                httpContext.Response.ContentType = contentType;
            }
            return httpContext.Response.WriteAsync(success.Value?.ToString() ?? string.Empty);
        }

        if (options.WrapSuccessResponses && options.SuccessResponseBuilder != null)
        {
            var successDetails = new SuccessDetail
            {
                Status = mapping?.StatusCode ?? HttpStatusCode.OK,
                Message = success.Message ?? "",
                Data = success.Value,
            };

            var response = options.SuccessResponseBuilder(successDetails);
            return httpContext.Response.WriteAsJsonAsync(response);
        }
        else
        {
            return httpContext.Response.WriteAsJsonAsync(success.Value);
        }
    }
}