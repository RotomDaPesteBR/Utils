using LightningArc.Utils.Results.Messages;

namespace LightningArc.Utils.Results;

public partial class Error
{
    /// <summary>
    /// Represents the external systems error module.
    /// </summary>
    /// <remarks>
    /// This module contains errors that occur during interaction with third-party APIs or
    /// services, such as usage limits or unexpected responses.
    /// The code prefix for errors in this module is 8.
    /// </remarks>
    public partial class External : ErrorModule
    {
        /// <summary>
        /// Gets the error category code prefix.
        /// </summary>
        /// <remarks>
        /// This value is used to categorize the error and is combined with a suffix to form the complete error code.
        /// </remarks>
        public new const int CodePrefix = (int)ModuleCodes.External;

        /// <summary>
        /// Defines the numeric suffixes for External module errors (prefix 8).
        /// These values are used to compose the complete error code (e.g., 8001, 8002, etc.).
        /// </summary>
        public enum Codes
        {
            /// <summary>
            /// Code '1'. The external service's request limit per time period has been exceeded.
            /// </summary>
            RateLimitExceeded = 1,

            /// <summary>
            /// Code '2'. The total usage quota for the external API (monthly/daily) has been reached.
            /// </summary>
            ApiQuotaExceeded = 2,

            /// <summary>
            /// Code '3'. The external API response was not in the expected format or contained inconsistent data.
            /// </summary>
            InvalidApiResponse = 3,

            /// <summary>
            /// Code '4'. The external service is inoperative or temporarily unavailable.
            /// </summary>
            ServiceUnavailable = 4,

            /// <summary>
            /// Code '5'. The request to the external service timed out.
            /// </summary>
            Timeout = 5,

            /// <summary>
            /// Code '6'. Low-level communication failure (e.g., SSL/TLS error, handshake failure).
            /// </summary>
            Communication = 6,
        }

        // --- Classes Internas de Erro ---

        /// <summary>
        /// Represents a rate limit exceeded error (Suffix: 01).
        /// </summary>
        internal class RateLimitExceededError : Error
        {
            internal RateLimitExceededError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(External.CodePrefix, (int)Codes.RateLimitExceeded, messageProvider, details) { }
        }

        /// <summary>
        /// Represents an API quota exceeded error (Suffix: 02).
        /// </summary>
        internal class ApiQuotaExceededError : Error
        {
            internal ApiQuotaExceededError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(External.CodePrefix, (int)Codes.ApiQuotaExceeded, messageProvider, details) { }
        }

        /// <summary>
        /// Represents an invalid API response error (Suffix: 03).
        /// </summary>
        internal class InvalidApiResponseError : Error
        {
            internal InvalidApiResponseError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(External.CodePrefix, (int)Codes.InvalidApiResponse, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a service unavailable error (Suffix: 04).
        /// </summary>
        internal class ServiceUnavailableError : Error
        {
            internal ServiceUnavailableError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(External.CodePrefix, (int)Codes.ServiceUnavailable, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a timeout error (Suffix: 05).
        /// </summary>
        internal class TimeoutError : Error
        {
            internal TimeoutError(IMessageProvider messageProvider, IEnumerable<ErrorDetail>? details = null)
                : base(External.CodePrefix, (int)Codes.Timeout, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a communication error (Suffix: 06).
        /// </summary>
        internal class CommunicationError : Error
        {
            internal CommunicationError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(External.CodePrefix, (int)Codes.Communication, messageProvider, details) { }
        }

        // --- Construtores Estáticos ---

        /// <summary>
        /// Creates a new instance of a rate limit exceeded error (code 01).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a rate limit exceeded.</returns>
        public static Error RateLimitExceeded(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) => new RateLimitExceededError(ErrorMessageFactory.CreateProvider(message, "External_RateLimitExceeded"), details);

        /// <summary>
        /// Creates a new instance of an API quota exceeded error (code 02).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing an API quota exceeded.</returns>
        public static Error ApiQuotaExceeded(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) => new ApiQuotaExceededError(ErrorMessageFactory.CreateProvider(message, "External_ApiQuotaExceeded"), details);

        /// <summary>
        /// Creates a new instance of an invalid API response error (code 03).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing an invalid API response.</returns>
        public static Error InvalidApiResponse(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) => new InvalidApiResponseError(ErrorMessageFactory.CreateProvider(message, "External_InvalidApiResponse"), details);

        /// <summary>
        /// Creates a new instance of a service unavailable error (code 04).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a service unavailable.</returns>
        public static Error ServiceUnavailable(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) => new ServiceUnavailableError(ErrorMessageFactory.CreateProvider(message, "External_ServiceUnavailable"), details);

        /// <summary>
        /// Creates a new instance of a timeout error (code 05).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a timeout.</returns>
        public static Error Timeout(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) => new TimeoutError(ErrorMessageFactory.CreateProvider(message, "External_Timeout"), details);

        /// <summary>
        /// Creates a new instance of a communication error (code 06).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a communication failure.</returns>
        public static Error Communication(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) => new CommunicationError(ErrorMessageFactory.CreateProvider(message, "External_Communication"), details);
    }
}
