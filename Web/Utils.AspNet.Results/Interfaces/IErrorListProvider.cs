namespace LightningArc.Utils.Results.AspNet.Interfaces;

/// <summary>
/// Defines a contract for a service that provides a list of all registered error metadata.
/// </summary>
public interface IErrorListProvider
{
    /// <summary>
    /// Gathers and returns metadata for all registered errors.
    /// </summary>
    /// <returns>An enumerable collection of <see cref="ErrorMetadata"/>.</returns>
    IEnumerable<ErrorMetadata> GetErrorMetadata();
}
