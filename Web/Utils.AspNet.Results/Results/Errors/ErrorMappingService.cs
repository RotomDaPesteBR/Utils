using System.Collections.ObjectModel;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LightningArc.Utils.Results.AspNet;

/// <summary>
/// Serviço para mapear tipos de erros específicos (<see cref="Error"/>) para
/// respostas HTTP apropriadas (código de status e detalhes do problema).
/// </summary>
/// <remarks>
/// Este serviço é responsável por manter um dicionário de mapeamentos,
/// permitindo que a aplicação converta instâncias de <see cref="Error"/> em
/// respostas HTTP padronizadas com base no seu tipo.
/// </remarks>
public class ErrorMappingService
{
    /// <summary>
    /// Armazena os mapeamentos de tipos de erro para detalhes de resposta HTTP.
    /// A chave é o <see cref="Type"/> do erro e o valor é o <see cref="ErrorMapping"/> correspondente.
    /// </summary>
    private readonly Dictionary<Type, ErrorMapping> _mappings = [];

    /// <summary>
    /// Obtém os mapeamentos de tipos de erro para detalhes de resposta HTTP.
    /// A chave é o <see cref="Type"/> do erro e o valor é o <see cref="ErrorMapping"/> correspondente.
    /// </summary>
    public ReadOnlyDictionary<Type, ErrorMapping> Mappings =>
#if NET7_0_OR_GREATER
        _mappings.AsReadOnly();
#else
        new(_mappings);
#endif

    private readonly ILogger<ErrorMappingService> _logger;

    /// <summary>
    /// O construtor é o melhor lugar para definir os mapeamentos padrão de forma explícita.
    /// Mapeia todos os erros da biblioteca para códigos HTTP lógicos e títulos de problema.
    /// </summary>
    /// <param name="logger">O serviço de logging para registrar informações de mapeamento.</param>
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
    /// Adiciona ou sobrescreve um mapeamento para um tipo de erro específico.
    /// </summary>
    /// <typeparam name="TError">O tipo do erro a ser mapeado.</typeparam>
    /// <param name="statusCode">O código de status HTTP a ser retornado para este erro.</param>
    /// <param name="title">O título do problema para este erro.</param>
    /// <param name="type">A URL do tipo do problema para este erro.</param>
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
    /// Obtém o mapeamento HTTP para um erro.
    /// </summary>
    /// <param name="error">A instância do erro para a qual se deseja obter o mapeamento.</param>
    /// <returns>
    /// Um <see cref="ErrorMapping"/> se um mapeamento for encontrado para o tipo de erro,
    /// caso contrário, retorna <c>null</c>.
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
