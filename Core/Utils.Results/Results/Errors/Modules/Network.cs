using LightningArc.Utils.Results.Messages;

namespace LightningArc.Utils.Results;

public partial class Error
{
    /// <summary>
    /// Represents the network error module.
    /// </summary>
    /// <remarks>
    /// This module contains errors that occur during low-level communication (TCP/IP, SSL)
    /// with external services, such as connection failures or timeouts. The code prefix
    /// for errors in this module is 9.
    /// </remarks>
    public partial class Network : ErrorModule
    {
        /// <summary>
        /// Gets the code prefix for the error category.
        /// </summary>
        /// <remarks>
        /// This value is used to categorize the error and is combined with a suffix to form the complete error code.
        /// </remarks>
        public new const int CodePrefix = (int)ModuleCodes.Network;

        /// <summary>
        /// Defines the numeric suffixes for errors in the Network module (prefix 9).
        /// These values are used to compose the complete error code (e.g., 9001, 9002, etc.).
        /// </summary>
        public enum Codes
        {
            /// <summary>
            /// Code '1'. Failed to establish the connection (e.g., unreachable host, closed port).
            /// </summary>
            ConnectionFailed = 1,

            /// <summary>
            /// Code '2'. The time limit for receiving a response or data was exceeded.
            /// </summary>
            RequestTimeout = 2,

            /// <summary>
            /// Code '3'. The network service is temporarily down (usually status 503).
            /// </summary>
            ServiceUnavailable = 3,

            /// <summary>
            /// Code '4'. Domain name resolution (DNS) failure.
            /// </summary>
            DnsFailure = 4,

            /// <summary>
            /// Code '5'. Failure during SSL/TLS handshake or invalid certificate.
            /// </summary>
            SslHandshakeFailed = 5,

            /// <summary>
            /// Code '6'. Failed to connect or authenticate through a proxy server.
            /// </summary>
            ProxyFailure = 6,
        }

        // --- Internal Error Classes ---

        /// <summary>
        /// Represents a network connection failed error (Suffix: 01).
        /// </summary>
        public sealed class ConnectionFailedError : Error
        {
            internal ConnectionFailedError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Network.CodePrefix, (int)Codes.ConnectionFailed, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a request timeout error (Suffix: 02).
        /// </summary>
        public sealed class RequestTimeoutError : Error
        {
            internal RequestTimeoutError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Network.CodePrefix, (int)Codes.RequestTimeout, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a service unavailable error (Suffix: 03).
        /// </summary>
        public sealed class ServiceUnavailableError : Error
        {
            internal ServiceUnavailableError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Network.CodePrefix, (int)Codes.ServiceUnavailable, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a DNS failure error (Suffix: 04).
        /// </summary>
        public sealed class DnsFailureError : Error
        {
            internal DnsFailureError(IMessageProvider messageProvider, IEnumerable<ErrorDetail>? details = null)
                : base(Network.CodePrefix, (int)Codes.DnsFailure, messageProvider, details) { }
        }

        /// <summary>
        /// Represents an SSL/TLS handshake failed error (Suffix: 05).
        /// </summary>
        public sealed class SslHandshakeFailedError : Error
        {
            internal SslHandshakeFailedError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Network.CodePrefix, (int)Codes.SslHandshakeFailed, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a proxy failure error (Suffix: 06).
        /// </summary>
        public sealed class ProxyFailureError : Error
        {
            internal ProxyFailureError(IMessageProvider messageProvider, IEnumerable<ErrorDetail>? details = null)
                : base(Network.CodePrefix, (int)Codes.ProxyFailure, messageProvider, details) { }
        }

        // --- Static Factory Methods ---

        /// <summary>
        /// Creates a new connection failed error instance (code 01).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a connection failure.</returns>
        public static Error ConnectionFailed(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new ConnectionFailedError(
                ErrorMessageFactory.CreateProvider(message, "Network_ConnectionFailed"),
                details
            );

        /// <summary>
        /// Creates a new request timeout error instance (code 02).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a timeout.</returns>
        public static Error RequestTimeout(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new RequestTimeoutError(
                ErrorMessageFactory.CreateProvider(message, "Network_RequestTimeout"),
                details
            );

        /// <summary>
        /// Creates a new service unavailable error instance (code 03).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a service unavailable.</returns>
        public static Error ServiceUnavailable(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new ServiceUnavailableError(
                ErrorMessageFactory.CreateProvider(message, "Network_ServiceUnavailable"),
                details
            );

        /// <summary>
        /// Creates a new DNS failure error instance (code 04).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a DNS failure.</returns>
        public static Error DnsFailure(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new DnsFailureError(
                ErrorMessageFactory.CreateProvider(message, "Network_DnsFailure"),
                details
            );

        /// <summary>
        /// Creates a new SSL/TLS handshake failed error instance (code 05).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing an SSL/TLS failure.</returns>
        public static Error SslHandshakeFailed(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new SslHandshakeFailedError(
                ErrorMessageFactory.CreateProvider(message, "Network_SslHandshakeFailed"),
                details
            );

        /// <summary>
        /// Creates a new proxy failure error instance (code 06).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a proxy failure.</returns>
        public static Error ProxyFailure(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new ProxyFailureError(
                ErrorMessageFactory.CreateProvider(message, "Network_ProxyFailure"),
                details
            );
    }
}
