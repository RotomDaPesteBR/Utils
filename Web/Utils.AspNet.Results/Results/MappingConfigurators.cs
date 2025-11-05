using System.Net;

namespace LightningArc.Utils.Results.AspNet;

/// <summary>
/// Proporciona uma API fluente para configurar mapeamentos de sucesso personalizados.
/// </summary>
/// <param name="options">O objeto de opções onde os mapeamentos serão registrados.</param>
public sealed class SuccessMappingConfigurator(EndpointResultOptions options)
{
    /// <summary>
    /// Mapeia um tipo de sucesso para uma resposta HTTP.
    /// </summary>
    /// <typeparam name="TSuccess">O tipo do sucesso a ser mapeado.</typeparam>
    /// <param name="statusCode">O código de status HTTP.</param>
    /// <param name="title">O título do sucesso.</param>
    public void Map<TSuccess>(HttpStatusCode statusCode, string title)
        where TSuccess : Success => options.SuccessMappings.Add(new CustomSuccessMapping
        {
            SuccessType = typeof(TSuccess),
            StatusCode = statusCode,
            Title = title
        });
}

/// <summary>
/// Proporciona uma API fluente para configurar mapeamentos de erro personalizados.
/// </summary>
/// <param name="options">O objeto de opções onde os mapeamentos serão registrados.</param>
public sealed class ErrorMappingConfigurator(EndpointResultOptions options)
{
    /// <summary>
    /// Mapeia um tipo de erro para uma resposta HTTP.
    /// </summary>
    /// <typeparam name="TError">O tipo do erro a ser mapeado.</typeparam>
    /// <param name="statusCode">O código de status HTTP.</param>
    /// <param name="title">O título do problema.</param>
    /// <param name="type">A URL do tipo do problema.</param>
    public void Map<TError>(HttpStatusCode statusCode, string title, string type)
        where TError : Error => options.ErrorMappings.Add(new CustomErrorMapping
        {
            ErrorType = typeof(TError),
            StatusCode = statusCode,
            Title = title,
            Type = type
        });
}
