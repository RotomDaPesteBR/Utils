using System.Net;

namespace LightningArc.Utils.Results.AspNet;

/// <summary>
/// Represents mapping information for a success type.
/// </summary>
/// <param name="statusCode">The HTTP status code to be returned.</param>
/// <param name="title">The descriptive success title, used in the response.</param>
public sealed class SuccessMapping(HttpStatusCode statusCode, string title)
{
    /// <summary>
    /// Gets the HTTP status code of the mapping.
    /// </summary>
    public HttpStatusCode StatusCode { get; } = statusCode;

    /// <summary>
    /// Gets the mapping title.
    /// </summary>
    public string Title { get; } = title;
}