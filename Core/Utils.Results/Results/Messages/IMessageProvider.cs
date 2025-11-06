using System.Globalization;

namespace LightningArc.Utils.Results.Messages;

/// <summary>
/// Define um contrato para provedores de mensagens, permitindo diferentes estratégias
/// para obter a mensagem de um erro ou sucesso.
/// </summary>
public interface IMessageProvider
{
    /// <summary>
    /// Obtém a mensagem formatada.
    /// </summary>
    /// <param name="culture">A cultura a ser usada para localização, se aplicável.</param>
    /// <returns>A mensagem formatada.</returns>
    string GetMessage(CultureInfo culture);
}
