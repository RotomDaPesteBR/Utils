using System.Net;

namespace LightningArc.Utils.Results.AspNet.Models;

/// <summary>
/// Represents the structured metadata of a registered error.
/// </summary>
public sealed class ErrorMetadata
{
    /// <summary>
    /// The name of the module the error belongs to (e.g., "Authentication").
    /// </summary>
    public required string Module { get; init; }

    /// <summary>
    /// The full error code.
    /// </summary>
    public required int Code { get; init; }

    /// <summary>
    /// The name of the error (e.g., "InvalidCredentials").
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The descriptive message for the error.
    /// </summary>
    public required string Message { get; init; }

    /// <summary>
    /// The mapped HTTP status code for this error. Can be null if no specific mapping exists.
    /// </summary>
    public HttpStatusCode? HttpStatusCode { get; init; }
}
