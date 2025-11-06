using System.Globalization;

namespace LightningArc.Utils.Results.Messages;

/// <summary>
/// Provedor de mensagens para mensagens literais (estáticas).
/// </summary>
/// <remarks>
/// Inicializa uma nova instância de <see cref="LiteralMessageProvider"/>.
/// </remarks>
/// <param name="message">A mensagem literal.</param>
/// <param name="formatArgs">Argumentos opcionais para formatar a mensagem.</param>
public class LiteralMessageProvider(string message, object?[]? formatArgs = null) : IMessageProvider
{
    private readonly string _message = message ?? throw new ArgumentNullException(nameof(message));
    private readonly object?[]? _formatArgs = formatArgs;

    /// <inheritdoc/>
    public string GetMessage(CultureInfo culture) =>
        _formatArgs?.Length > 0 ? string.Format(culture, message, _formatArgs) : _message;
}
