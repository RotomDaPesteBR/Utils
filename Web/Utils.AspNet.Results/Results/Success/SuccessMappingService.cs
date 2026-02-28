using System.Net;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LightningArc.Utils.Results.AspNet;

/// <summary>
/// Serviço para mapear tipos de sucesso específicos (<see cref="Success"/>) para
/// respostas HTTP apropriadas (código de status e detalhes do problema).
/// </summary>
/// <remarks>
/// Este serviço é responsável por manter um dicionário de mapeamentos,
/// permitindo que a aplicação converta instâncias de <see cref="Success"/> em
/// respostas HTTP padronizadas com base no seu tipo.
/// </remarks>
public class SuccessMappingService
{
    /// <summary>
    /// Armazena os mapeamentos de tipos de sucesso para detalhes de resposta HTTP.
    /// A chave é o <see cref="Type"/> do sucesso e o valor é o <see cref="SuccessMapping"/> correspondente.
    /// </summary>
    private readonly Dictionary<Type, SuccessMapping> _mappings = [];

    private readonly ILogger<SuccessMappingService> _logger;

    /// <summary>
    /// O construtor é o melhor lugar para definir os mapeamentos padrão de forma explícita.
    /// Mapeia todos os sucessos da biblioteca para códigos HTTP lógicos e títulos de sucesso.
    /// </summary>
    /// <param name="logger">O serviço de logging para registrar informações de mapeamento.</param>
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
    /// Adiciona ou sobrescreve um mapeamento para um tipo de sucesso específico.
    /// </summary>
    /// <typeparam name="TSuccess">O tipo do sucesso a ser mapeado.</typeparam>
    /// <param name="statusCode">O código de status HTTP a ser retornado para este sucesso.</param>
    /// <param name="title">O título do problema para este sucesso.</param>
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
    /// Adiciona ou sobrescreve um mapeamento para um tipo de sucesso específico.
    /// </summary>
    /// <param name="successTypeDefinition">O tipo do sucesso a ser mapeado.</param>
    /// <param name="statusCode">O código de status HTTP a ser retornado para este sucesso.</param>
    /// <param name="title">O título do problema para este sucesso.</param>
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
    /// Obtém o mapeamento HTTP para um sucesso.
    /// </summary>
    /// <param name="success">A instância do sucesso para a qual se deseja obter o mapeamento.</param>
    /// <returns>
    /// Um <see cref="SuccessMapping"/> se um mapeamento for encontrado para o tipo de sucesso,
    /// caso contrário, retorna <c>null</c>.
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
