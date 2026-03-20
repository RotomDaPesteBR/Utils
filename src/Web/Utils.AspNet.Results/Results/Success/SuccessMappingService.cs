using System.Net;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LightningArc.Utils.Results.AspNet;

/// <summary>
/// Service to map specific success types (<see cref="Success"/>) to
/// appropriate HTTP responses (status code and problem details).
/// </summary>
/// <remarks>
/// This service is responsible for maintaining a dictionary of mappings,
/// allowing the application to convert instances of <see cref="Success"/> into
/// standardized HTTP responses based on their type.
/// </remarks>
public class SuccessMappingService
{
    /// <summary>
    /// Stores the mappings from success types to HTTP response details.
    /// The key is the success <see cref="Type"/> and the value is the corresponding <see cref="SuccessMapping"/>.
    /// </summary>
    private readonly Dictionary<Type, SuccessMapping> _mappings = [];

    private readonly ILogger<SuccessMappingService> _logger;

    /// <summary>
    /// The constructor is the best place to define default mappings explicitly.
    /// Maps all library successes to logical HTTP codes and success titles.
    /// </summary>
    /// <param name="logger">The logging service to record mapping information.</param>
    /// <param name="options"></param>
    public SuccessMappingService(
        ILogger<SuccessMappingService> logger,
        IOptions<EndpointResultOptions> options
    )
    {
        _logger = logger;
        _logger.LogInformation("Iniciando o mapeamento de sucessos HTTP para a API...");

        // Mapeamentos de Sucesso da Biblioteca
        // ------------------------------------
        Map<Success.OkSuccess>(HttpStatusCode.OK, "OK");
        Map<Success.CreatedSuccess>(HttpStatusCode.Created, "Criado");
        Map<Success.AcceptedSuccess>(HttpStatusCode.Accepted, "Aceito");
        Map<Success.NoContentSuccess>(HttpStatusCode.NoContent, "Sem Conteúdo");

        Map(typeof(Success<>.OkSuccess), HttpStatusCode.OK, "OK");
        Map(typeof(Success<>.CreatedSuccess), HttpStatusCode.Created, "Criado");
        Map(typeof(Success<>.AcceptedSuccess), HttpStatusCode.Accepted, "Aceito");
        Map(typeof(Success<>.NoContentSuccess), HttpStatusCode.NoContent, "Sem Conteúdo");

        if (options.Value.SuccessMappings.Count > 0)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation(
                    "Adicionando {Count} mapeamentos de sucesso personalizados.",
                    options.Value.SuccessMappings.Count
                );
            }
            foreach (CustomSuccessMapping mapping in options.Value.SuccessMappings)
            {
                _mappings[mapping.SuccessType] = new SuccessMapping(
                    mapping.StatusCode,
                    mapping.Title
                );
            }
        }

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation(
                "Mapeamento de sucessos HTTP concluído. {Count} sucessos registrados.",
                _mappings.Count
            );
        }
    }

    /// <summary>
    /// Adds or overrides a mapping for a specific success type.
    /// </summary>
    /// <typeparam name="TSuccess">The type of success to map.</typeparam>
    /// <param name="statusCode">The HTTP status code to be returned for this success.</param>
    /// <param name="title">The problem title for this success.</param>
    public void Map<TSuccess>(HttpStatusCode statusCode, string title)
        where TSuccess : Success
    {
        _mappings[typeof(TSuccess)] = new SuccessMapping(statusCode, title);
        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug(
                "Mapeado sucesso {SuccessType} para Status {StatusCode} e Título '{Title}'.",
                typeof(TSuccess).Name,
                (int)statusCode,
                title
            );
        }
    }

    /// <summary>
    /// Adds or overrides a mapping for a specific success type.
    /// </summary>
    /// <param name="successTypeDefinition">The type of success to map.</param>
    /// <param name="statusCode">The HTTP status code to be returned for this success.</param>
    /// <param name="title">The problem title for this success.</param>
    private void Map(Type successTypeDefinition, HttpStatusCode statusCode, string title)
    {
        _mappings[successTypeDefinition] = new SuccessMapping(statusCode, title);
        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug(
                "Mapeado sucesso {SuccessType} para Status {StatusCode} e Título '{Title}'.",
                successTypeDefinition.Name,
                (int)statusCode,
                title
            );
        }
    }

    /// <summary>
    /// Gets the HTTP mapping for a success.
    /// </summary>
    /// <param name="success">The success instance for which the mapping is desired.</param>
    /// <returns>
    /// A <see cref="SuccessMapping"/> if a mapping is found for the success type,
    /// otherwise returns <c>null</c>.
    /// </returns>
    public SuccessMapping? GetMapping(Success success)
    {
        Type successType = success.GetType();

        Type? typeToLookup = successType;

        // 2. Se for um tipo genérico aninhado (ex: Success<User>.OkSuccess)
        //    precisamos extrair sua definição genérica.
        if (successType.IsGenericType && successType.IsNested)
        {
            // Obtém o tipo pai (ex: Success<User>)
            Type? declaringType = successType.DeclaringType;

            // Se o tipo pai for genérico, precisamos construir o tipo de busca:
            if (declaringType?.IsGenericType == true)
            {
                try
                {
                    // 2a. Obtém o tipo de definição genérica do pai (ex: Success<>)
                    Type genericParentDef = declaringType.GetGenericTypeDefinition();

                    // 2b. Encontra o tipo aninhado correspondente na definição genérica do pai (ex: Success<>.{Operation}Success)
                    Type genericTypeDefinition = genericParentDef.GetNestedType(
                        successType.Name,
                        BindingFlags.Public | BindingFlags.NonPublic
                    )!;
                    typeToLookup = genericTypeDefinition;
                }
                catch (Exception ex)
                {
                    // Log de erro se a reflexão falhar de forma inesperada.
                    if (_logger.IsEnabled(LogLevel.Error))
                    {
                        _logger.LogError(
                            ex,
                            "Erro ao tentar obter a definição de tipo genérico para o sucesso aninhado '{SuccessType}'.",
                            successType.Name
                        );
                    }
                }

                // Fallback
                typeToLookup ??= successType;
            }
        }

        // 3. Tenta obter o mapeamento com o tipo de lookup (que será o tipo exato, ou a definição genérica).
        if (_mappings.TryGetValue(typeToLookup, out SuccessMapping? mapping))
        {
            return mapping;
        }

        // 4. Tenta obter o mapeamento pelo tipo exato (caso o mapeamento genérico falhe ou seja um mapeamento personalizado exato)
        if (typeToLookup != successType && _mappings.TryGetValue(successType, out SuccessMapping? exactMapping))
        {
            return exactMapping;
        }

        // Ação de Log para sucessos não mapeados
        if (_logger.IsEnabled(LogLevel.Warning))
        {
            _logger.LogWarning(
                "Nenhum mapeamento HTTP encontrado para o tipo de sucesso '{SuccessType}'. Retornando padrão.",
                success.GetType().Name
            );
        }

        return null;
    }
}
