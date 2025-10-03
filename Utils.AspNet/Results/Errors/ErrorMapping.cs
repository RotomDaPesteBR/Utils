using System.Net;

namespace LightningArc.Utils.Results.AspNet;

/// <summary>
/// Classe para armazenar os detalhes do mapeamento de um erro para uma resposta HTTP.
/// </summary>
/// <remarks>
/// Inicializa uma nova instância de <see cref="ErrorMapping"/>.
/// </remarks>
/// <param name="statusCode">O código de status HTTP.</param>
/// <param name="title">O título da resposta de problema.</param>
/// <param name="type">O identificador do tipo de problema.</param>
public class ErrorMapping(HttpStatusCode statusCode, string title, string type)
{
    /// <summary>
    /// Obtém o código de status HTTP associado ao erro.
    /// </summary>
    public HttpStatusCode StatusCode { get; } = statusCode;

    /// <summary>
    /// Obtém o título da resposta de problema HTTP.
    /// </summary>
    public string Title { get; } = title;

    /// <summary>
    /// Obtém o identificador do tipo de problema.
    /// </summary>
    public string Type { get; } = type;
}
