using System.Globalization;

namespace LightningArc.Utils.Results.Messages;

/// <summary>
/// Message provider for localized messages via resource key.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="ResourceMessageProvider"/>.
/// </remarks>
/// <param name="resourceKey">The resource key for the message.</param>
/// <param name="localizationFunction">The function to obtain the localized string (e.g., LocalizationManager.GetErrorString).</param>
/// <param name="formatArgs">Optional arguments to format the message.</param>
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
