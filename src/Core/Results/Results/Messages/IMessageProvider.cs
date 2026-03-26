using System.Globalization;

namespace LightningArc.Results.Messages;

/// <summary>
/// Defines a contract for message providers, allowing for different strategies
/// to obtain the message of an error or success.
/// </summary>
public interface IMessageProvider
{
    /// <summary>
    /// Gets the formatted message.
    /// </summary>
    /// <param name="culture">The culture to be used for localization, if applicable.</param>
    /// <returns>The formatted message.</returns>
    string GetMessage(CultureInfo culture);
}

