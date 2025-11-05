namespace LightningArc.Utils.Results.Errors;

/// <summary>
/// Classe para encapsular os detalhes de um erro específico.
/// </summary>
public class ErrorInformation
{
    /// <summary>
    /// O código numérico único do erro.
    /// </summary>
    public int Code { get; init; }

    /// <summary>
    /// O nome do método de fábrica que cria o erro.
    /// </summary>
    public string Name { get; init; } = "Erro";

    /// <summary>
    /// A mensagem padrão associada a este erro.
    /// </summary>
    public string Message { get; init; } = "";
}
