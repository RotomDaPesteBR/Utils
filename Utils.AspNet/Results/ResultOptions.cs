using System.Net;

namespace LightningArc.Utils.Results.AspNet;

/// <summary>
/// Representa uma configuração de mapeamento de erro personalizado.
/// </summary>
public sealed class CustomErrorMapping
{
    /// <summary>
    /// O tipo do erro a ser mapeado.
    /// </summary>
    public Type ErrorType { get; set; } = default!;

    /// <summary>
    /// O código de status HTTP a ser retornado para este erro.
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    /// O título do problema para este erro.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// A URL do tipo do problema para este erro.
    /// </summary>
    public string Type { get; set; } = string.Empty;
}

/// <summary>
/// Representa uma configuração de mapeamento de sucesso personalizado.
/// </summary>
public sealed class CustomSuccessMapping
{
    /// <summary>
    /// O tipo do sucesso a ser mapeado.
    /// </summary>
    public Type SuccessType { get; set; } = default!;

    /// <summary>
    /// O código de status HTTP a ser retornado para este sucesso.
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    /// O título do sucesso.
    /// </summary>
    public string Title { get; set; } = string.Empty;
}

/// <summary>
/// Classe de opções para configurar os mapeamentos de sucesso e erro.
/// </summary>
public sealed class ResultOptions
{
    /// <summary>
    /// Uma lista para registrar mapeamentos de erros personalizados.
    /// </summary>
    public List<CustomErrorMapping> ErrorMappings { get; } = [];

    /// <summary>
    /// Uma lista para registrar mapeamentos de sucessos personalizados.
    /// </summary>
    public List<CustomSuccessMapping> SuccessMappings { get; } = [];
}