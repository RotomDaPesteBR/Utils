using System.Net;
using Microsoft.Extensions.DependencyInjection;

namespace LightningArc.Utils.Results.AspNet;

/// <summary>
/// Representa um resultado HTTP que mapeia uma instância de <see cref="Success"/> para uma
/// resposta de sucesso padrão.
/// </summary>
/// <param name="success">A instância de sucesso a ser mapeada.</param>
public sealed class SuccessResult(Success success) : IResult
{

    /// <summary>
    /// Executa a resposta assincronamente, formatando o resultado de sucesso e escrevendo
    /// no contexto HTTP.
    /// </summary>
    /// <param name="httpContext">O contexto HTTP atual.</param>
    /// <returns>Uma <see cref="Task"/> que representa a operação assíncrona.</returns>
    public Task ExecuteAsync(HttpContext httpContext)
    {
        var mappingService =
            httpContext.RequestServices.GetRequiredService<SuccessMappingService>();
        var mapping = mappingService.GetMapping(success);
        var statusCode = (int)(mapping?.StatusCode ?? HttpStatusCode.OK);

        httpContext.Response.StatusCode = statusCode;

        if (!string.IsNullOrEmpty(success.Message) && statusCode != (int)HttpStatusCode.NoContent)
        {
            var successDetails = new
            {
                Status = mapping?.StatusCode ?? HttpStatusCode.OK,
                Message = success.Message ?? "",
            };

            return httpContext.Response.WriteAsJsonAsync(successDetails);
        }

        return Task.CompletedTask;
    }
}

/// <summary>
/// Representa um resultado HTTP que mapeia uma instância de <see cref="Success"/> e um valor
/// para uma resposta de sucesso padrão.
/// </summary>
/// <typeparam name="TValue">O tipo do valor de sucesso a ser retornado na resposta.</typeparam>
public sealed class SuccessResult<TValue>(Success success, TValue value, string? contentType = null)
    : IResult
{

    /// <summary>
    /// Executa a resposta assincronamente, formatando o resultado de sucesso com o valor
    /// e escrevendo no contexto HTTP.
    /// </summary>
    /// <param name="httpContext">O contexto HTTP atual.</param>
    /// <returns>Uma <see cref="Task"/> que representa a operação assíncrona.</returns>
    public Task ExecuteAsync(HttpContext httpContext)
    {
        var mappingService = httpContext.RequestServices.GetService<SuccessMappingService>();
        var mapping = mappingService?.GetMapping(success);
        var statusCode = (int)(mapping?.StatusCode ?? HttpStatusCode.OK);

        httpContext.Response.StatusCode = statusCode;

        if (statusCode != (int)HttpStatusCode.NoContent)
        {
            if (contentType is not null)
            {
                if (contentType.Contains("text/plain") && !contentType.Contains("charset"))
                {
                    httpContext.Response.ContentType = $"{contentType}; charset=utf-8";
                    return httpContext.Response.WriteAsync(value?.ToString() ?? string.Empty);
                }

                httpContext.Response.ContentType = contentType;

                return httpContext.Response.WriteAsync(value?.ToString() ?? string.Empty);
            }

            var successDetails = new
            {
                Status = mapping?.StatusCode ?? HttpStatusCode.OK,
                Message = success.Message ?? "",
                Data = value,
            };

            return httpContext.Response.WriteAsJsonAsync(successDetails);
        }

        return Task.CompletedTask;
    }
}
