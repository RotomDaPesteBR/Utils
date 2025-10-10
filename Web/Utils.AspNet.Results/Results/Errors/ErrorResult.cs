using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace LightningArc.Utils.Results.AspNet;

/// <summary>
/// Representa um resultado HTTP que mapeia uma instância de <see cref="Error"/> para uma
/// resposta padrão de "Problem Details" (RFC 7807).
/// </summary>
/// <param name="error">A instância do erro a ser serializada na resposta.</param>
public sealed class ErrorResult(Error error) : IResult
{
    /// <summary>
    /// Executa a resposta assincronamente, formatando o erro como "Problem Details" e escrevendo no contexto HTTP.
    /// </summary>
    /// <param name="httpContext">O contexto HTTP atual.</param>
    /// <returns>Uma <see cref="Task"/> que representa a operação assíncrona.</returns>
    public Task ExecuteAsync(HttpContext httpContext)
    {
        // Obtém o serviço de mapeamento do provedor de serviços
        var mappingService = httpContext.RequestServices.GetService<ErrorMappingService>();

        var mapping = mappingService?.GetMapping(error);
        var statusCode = (int)(mapping?.StatusCode ?? HttpStatusCode.InternalServerError);
        var title = mapping?.Title ?? "Um erro inesperado ocorreu.";
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

        if (error.Details is not null && error.Details.Count != 0)
        {
            problemDetails.Extensions["errors"] = error.Details;
        }

        return httpContext.Response.WriteAsJsonAsync(problemDetails);
    }
}
