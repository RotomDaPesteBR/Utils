using LightningArc.Utils.Results.Messages;

namespace LightningArc.Utils.Results;

public partial class Error
{
    /// <summary>
    /// Represents the authentication and authorization error module.
    /// </summary>
    /// <remarks>
    /// This module contains errors that occur during the authentication (identity)
    /// and authorization (permission) process. The code prefix for errors in this module is 6.
    /// </remarks>
    public partial class Authentication : ErrorModule
    {
        /// <summary>
        /// Gets the error category code prefix.
        /// </summary>
        /// <remarks>
        /// This value is used to categorize the error and is combined with a suffix to form the complete error code.
        /// </remarks>
        public new const int CodePrefix = (int)ModuleCodes.Authentication;

        /// <summary>
        /// Defines the numeric suffixes for Authentication module errors (prefix 6).
        /// These values are used to compose the complete error code (e.g., 6001, 6002, etc.).
        /// </summary>
        public enum Codes
        {
            /// <summary>
            /// Code '1'. Generic authentication attempt failure (e.g., invalid or missing token).
            /// </summary>
            Unauthorized = 1,

            /// <summary>
            /// Code '2'. The user was authenticated (identity verified), but does not have permission for the operation (Authorization).
            /// </summary>
            Forbidden = 2,

            /// <summary>
            /// Code '3'. The access token (JWT, OAuth) was rejected because its lifetime expired.
            /// </summary>
            TokenExpired = 3,

            /// <summary>
            /// Code '4'. The provided username/email or password are incorrect.
            /// </summary>
            InvalidCredentials = 4,

            /// <summary>
            /// Code '5'. The user account is disabled, locked, or pending activation.
            /// </summary>
            InactiveAccount = 5,

            /// <summary>
            /// Code '6'. The user session has been terminated due to inactivity or logoff (different from TokenExpired).
            /// </summary>
            ExpiredSession = 6,
        }

        // --- Classes Internas de Erro ---

        /// <summary>
        /// Represents an authentication error (Suffix: 01).
        /// </summary>
        internal class UnauthorizedError : Error
        {
            internal UnauthorizedError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(
                    Authentication.CodePrefix,
                    (int)Codes.Unauthorized,
                    messageProvider,
                    details
                ) { }
        }

        /// <summary>
        /// Represents a forbidden access error (Suffix: 02).
        /// </summary>
        internal class ForbiddenError : Error
        {
            internal ForbiddenError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(
                    Authentication.CodePrefix,
                    (int)Codes.Forbidden,
                    messageProvider,
                    details
                ) { }
        }

        /// <summary>
        /// Represents an expired token error (Suffix: 03).
        /// </summary>
        internal class TokenExpiredError : Error
        {
            internal TokenExpiredError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(
                    Authentication.CodePrefix,
                    (int)Codes.TokenExpired,
                    messageProvider,
                    details
                ) { }
        }

        /// <summary>
        /// Represents an invalid credentials error (Suffix: 04).
        /// </summary>
        internal class InvalidCredentialsError : Error
        {
            internal InvalidCredentialsError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(
                    Authentication.CodePrefix,
                    (int)Codes.InvalidCredentials,
                    messageProvider,
                    details
                ) { }
        }

        /// <summary>
        /// Represents an inactive account error (Suffix: 05).
        /// </summary>
        internal class InactiveAccountError : Error
        {
            internal InactiveAccountError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(
                    Authentication.CodePrefix,
                    (int)Codes.InactiveAccount,
                    messageProvider,
                    details
                ) { }
        }

        /// <summary>
        /// Represents an expired session error (Suffix: 06).
        /// </summary>
        internal class ExpiredSessionError : Error
        {
            internal ExpiredSessionError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(
                    Authentication.CodePrefix,
                    (int)Codes.ExpiredSession,
                    messageProvider,
                    details
                ) { }
        }

        // --- Construtores Estáticos ---

        /// <summary>
        /// Creates a new instance of an authentication failure error (code 01).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing an authentication failure.</returns>
        public static Error Unauthorized(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new UnauthorizedError(
                ErrorMessageFactory.CreateProvider(message, "Authentication_Unauthorized"),
                details
            );

        /// <summary>
        /// Creates a new instance of a forbidden access error (code 02).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing forbidden access.</returns>
        public static Error Forbidden(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new ForbiddenError(
                ErrorMessageFactory.CreateProvider(message, "Authentication_Forbidden"),
                details
            );

        /// <summary>
        /// Creates a new instance of an expired token error (code 03).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing an expired token.</returns>
        public static Error TokenExpired(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new TokenExpiredError(
                ErrorMessageFactory.CreateProvider(message, "Authentication_TokenExpired"),
                details
            );

        /// <summary>
        /// Creates a new instance of an invalid credentials error (code 04).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing invalid credentials.</returns>
        public static Error InvalidCredentials(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new InvalidCredentialsError(
                ErrorMessageFactory.CreateProvider(
                    message,
                    "Authentication_InvalidCredentials"
                ),
                details
            );

        /// <summary>
        /// Creates a new instance of an inactive account error (code 05).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing an inactive account.</returns>
        public static Error InactiveAccount(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new InactiveAccountError(
                ErrorMessageFactory.CreateProvider(message, "Authentication_InactiveAccount"),
                details
            );

        /// <summary>
        /// Creates a new instance of an expired session error (code 06).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing an expired session.</returns>
        public static Error ExpiredSession(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new ExpiredSessionError(
                ErrorMessageFactory.CreateProvider(message, "Authentication_ExpiredSession"),
                details
            );
    }
}
