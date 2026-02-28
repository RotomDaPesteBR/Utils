using LightningArc.Utils.Results.Messages;

namespace LightningArc.Utils.Results;

public partial class Error
{
    /// <summary>
    /// Represents the request error module.
    /// </summary>
    /// <remarks>
    /// This module contains errors related to HTTP request processing,
    /// such as format, size, and frequency limits. The code prefix
    /// for errors in this module is 7.
    /// </remarks>
    public partial class Request : ErrorModule
    {
        /// <summary>
        /// Gets the code prefix for the error category.
        /// </summary>
        /// <remarks>
        /// This value is used to categorize the error and is combined with a suffix to form the complete error code.
        /// </remarks>
        public new const int CodePrefix = (int)ModuleCodes.Request;

        /// <summary>
        /// Defines the numeric suffixes for errors in the Request module (prefix 7).
        /// These values are used to compose the complete error code (e.g., 7001, 7002, etc.).
        /// </summary>
        public enum Codes
        {
            /// <summary>
            /// Code '1'. The request structure (URI, headers, body) is invalid.
            /// </summary>
            InvalidRequest = 1,

            /// <summary>
            /// Code '2'. The total payload size exceeds the server's allowed limit.
            /// </summary>
            TooLargeRequest = 2,

            /// <summary>
            /// Code '3'. The client has exceeded the request limit within a given time period (Rate Limiting).
            /// </summary>
            TooManyRequests = 3,

            /// <summary>
            /// Code '4'. The requested response format (via Accept header) is not supported by the server.
            /// </summary>
            NotAcceptable = 4,

            /// <summary>
            /// Code '5'. The media type (via Content-Type header) of the request is not supported for this endpoint.
            /// </summary>
            UnsupportedMediaType = 5,
        }

        // --- Internal Error Classes ---

        /// <summary>
        /// Represents an invalid request error (Suffix: 01).
        /// </summary>
        public sealed class InvalidRequestError : Error
        {
            internal InvalidRequestError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Request.CodePrefix, (int)Codes.InvalidRequest, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a too large request error (Suffix: 02).
        /// </summary>
        public sealed class TooLargeRequestError : Error
        {
            internal TooLargeRequestError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Request.CodePrefix, (int)Codes.TooLargeRequest, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a too many requests error (Suffix: 03).
        /// </summary>
        public sealed class TooManyRequestsError : Error
        {
            internal TooManyRequestsError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Request.CodePrefix, (int)Codes.TooManyRequests, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a "not acceptable" error (Suffix: 04).
        /// </summary>
        public sealed class NotAcceptableError : Error
        {
            internal NotAcceptableError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Request.CodePrefix, (int)Codes.NotAcceptable, messageProvider, details) { }
        }

        /// <summary>
        /// Represents an unsupported media type error (Suffix: 05).
        /// </summary>
        public sealed class UnsupportedMediaTypeError : Error
        {
            internal UnsupportedMediaTypeError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Request.CodePrefix, (int)Codes.UnsupportedMediaType, messageProvider, details)
            { }
        }

        // --- Static Factory Methods ---

        /// <summary>
        /// Creates a new invalid request error instance (code 01).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing an invalid request.</returns>
        public static Error Invalid(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new InvalidRequestError(
                ErrorMessageFactory.CreateProvider(message, "Request_InvalidRequest"),
                details
            );

        /// <summary>
        /// Creates a new too large request error instance (code 02).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a too large request.</returns>
        public static Error TooLarge(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new TooLargeRequestError(
                ErrorMessageFactory.CreateProvider(message, "Request_TooLargeRequest"),
                details
            );

        /// <summary>
        /// Creates a new too many requests error instance (code 03).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing too many requests.</returns>
        public static Error TooManyRequests(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new TooManyRequestsError(
                ErrorMessageFactory.CreateProvider(message, "Request_TooManyRequests"),
                details
            );

        /// <summary>
        /// Creates a new "not acceptable" error instance (code 04).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a "not acceptable" error.</returns>
        public static Error NotAcceptable(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new NotAcceptableError(
                ErrorMessageFactory.CreateProvider(message, "Request_NotAcceptable"),
                details
            );

        /// <summary>
        /// Creates a new unsupported media type error instance (code 05).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing an unsupported media type.</returns>
        public static Error UnsupportedMediaType(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new UnsupportedMediaTypeError(
                ErrorMessageFactory.CreateProvider(message, "Request_UnsupportedMediaType"),
                details
            );
    }
}
