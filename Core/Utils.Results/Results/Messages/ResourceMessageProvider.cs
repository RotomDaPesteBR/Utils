using System.Globalization;

namespace LightningArc.Utils.Results.Messages;

/// <summary>
/// Provedor de mensagens para mensagens localizadas via chave de recurso.
/// </summary>
/// <remarks>
/// Inicializa uma nova instância de <see cref="ResourceMessageProvider"/>.
/// </remarks>
/// <param name="resourceKey">A chave do recurso para a mensagem.</param>
/// <param name="localizationFunction">A função para obter a string localizada (e.g., LocalizationManager.GetErrorString).</param>
/// <param name="formatArgs">Argumentos opcionais para formatar a mensagem.</param>
public class ResourceMessageProvider(
    string resourceKey,
    Func<string, string> localizationFunction,
    object?[]? formatArgs = null
) : IMessageProvider
{
    private readonly string _resourceKey =
        resourceKey ?? throw new ArgumentNullException(nameof(resourceKey));
    private readonly object?[]? _formatArgs = formatArgs;
    private readonly Func<string, string> _localizationFunction =
        localizationFunction ?? throw new ArgumentNullException(nameof(localizationFunction));

    /// <inheritdoc/>
    public string GetMessage(CultureInfo culture)
    {
        string? localizedString = _localizationFunction(_resourceKey);
        return (_formatArgs?.Length > 0)
            ? string.Format(culture, localizedString, _formatArgs)
            : localizedString;
    }
}
