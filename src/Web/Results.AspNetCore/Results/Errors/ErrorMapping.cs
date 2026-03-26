
using LightningArc.Results;
using System.Net;

namespace LightningArc.Results.AspNetCore;

/// <summary>
/// Class to store the details of mapping an error to an HTTP response.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="ErrorMapping"/>.
/// </remarks>
/// <param name="statusCode">The HTTP status code.</param>
/// <param name="title">The problem response title.</param>
/// <param name="type">The problem type identifier.</param>
public class ErrorMapping(HttpStatusCode statusCode, string title, string type)
{
    /// <summary>
    /// Gets the HTTP status code associated with the error.
    /// </summary>
    public HttpStatusCode StatusCode { get; } = statusCode;

    /// <summary>
    /// Gets the HTTP problem response title.
    /// </summary>
    public string Title { get; } = title;

    /// <summary>
    /// Gets the problem type identifier.
    /// </summary>
    public string Type { get; } = type;
}




