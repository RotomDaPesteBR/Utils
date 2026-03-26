
using LightningArc.Results;
using System.Collections.ObjectModel;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LightningArc.Results.AspNetCore;

/// <summary>
/// Service to map specific error types (<see cref="Error"/>) to
/// appropriate HTTP responses (status code and problem details).
/// </summary>
/// <remarks>
/// This service is responsible for maintaining a dictionary of mappings,
/// allowing the application to convert instances of <see cref="Error"/> into
/// standardized HTTP responses based on their type.
/// </remarks>
public class ErrorMappingService
{
    /// <summary>
    /// Stores the mappings from error types to HTTP response details.
    /// The key is the error <see cref="Type"/> and the value is the corresponding <see cref="ErrorMapping"/>.
    /// </summary>
    private readonly Dictionary<Type, ErrorMapping> _mappings = [];

    /// <summary>
    /// Gets the mappings from error types to HTTP response details.
    /// The key is the error <see cref="Type"/> and the value is the corresponding <see cref="ErrorMapping"/>.
    /// </summary>
    public ReadOnlyDictionary<Type, ErrorMapping> Mappings =>
#if NET7_0_OR_GREATER
        _mappings.AsReadOnly();
#else
        new(_mappings);
#endif

    private readonly ILogger<ErrorMappingService> _logger;

    /// <summary>
    /// The constructor is the best place to define default mappings explicitly.
    /// Maps all library errors to logical HTTP codes and problem titles.
    /// </summary>
    /// <param name="logger">The logging service to record mapping information.</param>
    /// <param name="options"></param>
    public ErrorMappingService(ILogger<ErrorMappingService> logger, IOptions<EndpointResultOptions> options)
    {
        _logger = logger;

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Iniciando o mapeamento de erros HTTP para a API...");
        }

        // Mapeamentos de Erros da biblioteca
        this.MapDefaultErrors();

        // Mapeamentos de Erros personalizados
        int count = options.Value.ErrorMappings.Count;

        if (count > 0)
        {
            string plural = count > 1 ? "s" : "";

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation(
                    "Adicionando {Count} mapeamento{s} de erro personalizado{s}.",
                    count,
                    plural,
                    plural
                );
            }

            foreach (CustomErrorMapping mapping in options.Value.ErrorMappings)
            {
                _mappings[mapping.ErrorType] = new ErrorMapping(
                    mapping.StatusCode,
                    mapping.Title,
                    mapping.Type
                );

                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug(
                        "Mapeado erro {ErrorType} para Status {StatusCode} e Tipo '{ProblemType}'.",
                        mapping.ErrorType.Name,
                        (int)mapping.StatusCode,
                        mapping.Type
                    );
                }
            }
        }

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation(
                "Mapeamento de erros HTTP concluído. {Count} erros registrados.",
                _mappings.Count
            );
        }
    }

    /// <summary>
    /// Adds or overrides a mapping for a specific error type.
    /// </summary>
    /// <typeparam name="TError">The type of error to map.</typeparam>
    /// <param name="statusCode">The HTTP status code to be returned for this error.</param>
    /// <param name="title">The problem title for this error.</param>
    /// <param name="type">The problem type URL for this error.</param>
    public void Map<TError>(HttpStatusCode statusCode, string title, string type)
        where TError : Error
    {
        _mappings[typeof(TError)] = new ErrorMapping(statusCode, title, type);

        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug(
                "Mapeado erro {ErrorType} para Status {StatusCode} e Tipo '{ProblemType}'.",
                typeof(TError).Name,
                (int)statusCode,
                type
            );
        }
    }

    /// <summary>
    /// Gets the HTTP mapping for an error.
    /// </summary>
    /// <param name="error">The error instance for which the mapping is desired.</param>
    /// <returns>
    /// An <see cref="ErrorMapping"/> if a mapping is found for the error type,
    /// otherwise returns <c>null</c>.
    /// </returns>
    public ErrorMapping? GetMapping(Error error)
    {
        if (_mappings.TryGetValue(error.GetType(), out ErrorMapping? mapping))
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation(
                    "Mapeamento encontrado para o erro {ErrorCode}. Mapeado para o status HTTP {HttpStatusCode}.",
                    error.Code,
                    (int)mapping.StatusCode
                );
            }

            return mapping;
        }

        // Ação de Log para erros não mapeados

        if (_logger.IsEnabled(LogLevel.Warning))
        {
            _logger.LogWarning(
                "Nenhum mapeamento HTTP encontrado para o tipo de erro '{ErrorType}'. Retornando padrão.",
                error.GetType().Name
            );
        }

        return null;
    }
}




