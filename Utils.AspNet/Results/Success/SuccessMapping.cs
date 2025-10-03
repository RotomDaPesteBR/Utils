using System.Net;

namespace LightningArc.Utils.Results.AspNet;

/// <summary>
/// Representa as informações de mapeamento para um tipo de sucesso.
/// </summary>
/// <param name="statusCode">O código de status HTTP a ser retornado.</param>
/// <param name="title">O título descritivo do sucesso, usado na resposta.</param>
public sealed class SuccessMapping(HttpStatusCode statusCode, string title)
{
    /// <summary>
    /// Obtém o código de status HTTP do mapeamento.
    /// </summary>
    public HttpStatusCode StatusCode { get; } = statusCode;

    /// <summary>
    /// Obtém o título do mapeamento.
    /// </summary>
    public string Title { get; } = title;
}