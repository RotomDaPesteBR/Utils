using LightningArc.Utils.Results.AspNet.Models;

namespace LightningArc.Utils.Results.AspNet.Interfaces;

/// <summary>
/// Defines a contract for a service that formats a list of error metadata into a specific string representation.
/// </summary>
public interface IErrorListFormatter
{
    /// <summary>
    /// Formats a collection of error metadata.
    /// </summary>
    /// <param name="errors">The collection of <see cref="ErrorMetadata"/> to format.</param>
    /// <returns>A string representation of the error list.</returns>
    string Format(IEnumerable<ErrorMetadata> errors);
}
