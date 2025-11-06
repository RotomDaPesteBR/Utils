using LightningArc.Utils.Results.Messages;

namespace LightningArc.Utils.Results;

public partial class Error
{
    /// <summary>
    /// Represents the application error module.
    /// </summary>
    /// <remarks>
    /// This module contains high-level and generic application errors,
    /// such as internal failures and flow issues. The code prefix
    /// for errors in this module is 1.
    /// </remarks>
    public partial class Application : ErrorModule
    {
        /// <summary>
        /// Gets the code prefix for the error category.
        /// </summary>
        public new const int CodePrefix = (int)ModuleCodes.Application;

        /// <summary>
        /// Defines the numeric suffixes for errors in the Application module (prefix 1).
        /// </summary>
        public enum Codes
        {
            /// <summary>
            /// Code '1'. Generic error for any unmapped internal failure.
            /// </summary>
            Internal = 1,

            /// <summary>
            /// Code '2'. One or more parameters in an internal function call are invalid.
            /// </summary>
            InvalidParameter = 2,

            /// <summary>
            /// Code '3'. An operation is logically invalid in the current state of the object or system.
            /// </summary>
            InvalidOperation = 3,

            /// <summary>
            /// Code '4'. An operation was prematurely interrupted or canceled.
            /// </summary>
            TaskCanceled = 4,

            /// <summary>
            /// Code '5'. Resource, method, or functionality not yet implemented.
            /// </summary>
            NotImplemented = 5,
        }

        // --- Internal Error Classes ---

        internal class InternalError : Error
        {
            internal InternalError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Application.CodePrefix, (int)Codes.Internal, messageProvider, details) { }
        }

        internal class InvalidParameterError : Error
        {
            internal InvalidParameterError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(
                    Application.CodePrefix,
                    (int)Codes.InvalidParameter,
                    messageProvider,
                    details
                ) { }
        }

        internal class InvalidOperationError : Error
        {
            internal InvalidOperationError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(
                    Application.CodePrefix,
                    (int)Codes.InvalidOperation,
                    messageProvider,
                    details
                ) { }
        }

        internal class TaskCanceledError : Error
        {
            internal TaskCanceledError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Application.CodePrefix, (int)Codes.TaskCanceled, messageProvider, details)
            { }
        }

        internal class NotImplementedError : Error
        {
            internal NotImplementedError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Application.CodePrefix, (int)Codes.NotImplemented, messageProvider, details)
            { }
        }

        // --- Static Factory Methods ---

        /// <summary>
        /// Creates a new internal error instance (code 001).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing an internal error.</returns>
        public static Error Internal(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new InternalError(
                ErrorMessageFactory.CreateProvider(message, "Application_InternalError"),
                details
            );

        /// <summary>
        /// Creates a new invalid parameter error instance (code 002).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing an invalid parameter.</returns>
        public static Error InvalidParameter(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new InvalidParameterError(
                ErrorMessageFactory.CreateProvider(message, "Application_ValidationError"),
                details
            );

        /// <summary>
        /// Creates a new invalid operation error instance (code 003).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing an invalid operation.</returns>
        public static Error InvalidOperation(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new InvalidOperationError(
                ErrorMessageFactory.CreateProvider(message, "Application_InvalidOperation"),
                details
            );

        /// <summary>
        /// Creates a new task canceled error instance (code 004).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a canceled operation.</returns>
        public static Error TaskCanceled(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new TaskCanceledError(
                ErrorMessageFactory.CreateProvider(message, "Application_TaskCanceled"),
                details
            );

        /// <summary>
        /// Creates a new not implemented error instance (code 005).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a not implemented feature.</returns>
        public static Error NotImplemented(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new NotImplementedError(
                ErrorMessageFactory.CreateProvider(message, "Application_NotImplemented"),
                details
            );
    }
}
