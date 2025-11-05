using System.Net;

namespace Utils.AspNet.Results.Results.Success
{
    /// <summary>
    /// Represents detailed information about a successful operation, used for custom success response formatting.
    /// </summary>
    public class SuccessDetail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessDetail"/> class.
        /// </summary>
        public SuccessDetail() { }

        /// <summary>
        /// Gets the HTTP status code associated with the success.
        /// </summary>
        public HttpStatusCode Status { get; init; }

        /// <summary>
        /// Gets the descriptive message for the success.
        /// </summary>
        public string Message { get; init; } = "";

        /// <summary>
        /// Gets the optional data payload associated with the success.
        /// </summary>
        public object? Data { get; init; }
    }
}