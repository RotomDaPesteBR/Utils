using System.Globalization;

namespace LightningArc.Results.Messages;

/// <summary>
/// Message provider for literal (static) messages.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="LiteralMessageProvider"/>.
/// </remarks>
/// <param name="message">The literal message.</param>
/// <param name="formatArgs">Optional arguments to format the message.</param>
public class LiteralMessageProvider(string message, object?[]? formatArgs = null) : IMessageProvider
{
    private readonly string _message = message ?? throw new ArgumentNullException(nameof(message));
    private readonly object?[]? _formatArgs = formatArgs;

    /// <inheritdoc/>
    public string GetMessage(CultureInfo culture) =>
        _formatArgs?.Length > 0 ? string.Format(culture, message, _formatArgs) : _message;
}

