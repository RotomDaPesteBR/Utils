using LightningArc.Utils.Results.Messages;

namespace LightningArc.Utils.Results;

public partial class Error
{
    /// <summary>
    /// Represents the system and environment error module.
    /// </summary>
    /// <remarks>
    /// This module contains errors that occur due to infrastructure issues,
    /// such as incorrect configurations or missing dependencies.
    /// The code prefix for errors in this module is 10.
    /// </remarks>
    public partial class System : ErrorModule
    {
        /// <summary>
        /// Gets the code prefix for the error category.
        /// </summary>
        /// <remarks>
        /// This value is used to categorize the error and is combined with a suffix to form the complete error code.
        /// </remarks>
        public new const int CodePrefix = (int)ModuleCodes.System;

        /// <summary>
        /// Defines the numeric suffixes for errors in the System module (prefix 10).
        /// These values are used to compose the complete error code (e.g., 10001, 10002, etc.).
        /// </summary>
        public enum Codes
        {
            /// <summary>
            /// Code '1'. Failure to load or process critical settings.
            /// </summary>
            Configuration = 1,

            /// <summary>
            /// Code '2'. An essential dependency (e.g., injected service) was not registered or initialized.
            /// </summary>
            DependencyNotRegistered = 2,

            /// <summary>
            /// Code '3'. Failure due to memory exhaustion on the host.
            /// </summary>
            OutOfMemory = 3,

            /// <summary>
            /// Code '4'. An execution thread was unexpectedly aborted.
            /// </summary>
            ThreadAborted = 4,

            /// <summary>
            /// Code '5'. The system is under scheduled maintenance or temporarily unavailable.
            /// </summary>
            SystemMaintenance = 5,
        }

        // --- Internal Error Classes ---

        /// <summary>
        /// Represents a configuration error (001).
        /// </summary>
        internal class ConfigurationError : Error
        {
            internal ConfigurationError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(System.CodePrefix, (int)Codes.Configuration, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a dependency not registered error (002).
        /// </summary>
        internal class DependencyNotRegisteredError : Error
        {
            internal DependencyNotRegisteredError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(
                    System.CodePrefix,
                    (int)Codes.DependencyNotRegistered,
                    messageProvider,
                    details
                ) { }
        }

        /// <summary>
        /// Represents an out of memory error (003).
        /// </summary>
        internal class OutOfMemoryError : Error
        {
            internal OutOfMemoryError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(System.CodePrefix, (int)Codes.OutOfMemory, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a thread aborted error (004).
        /// </summary>
        internal class ThreadAbortedError : Error
        {
            internal ThreadAbortedError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(System.CodePrefix, (int)Codes.ThreadAborted, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a system maintenance error (005).
        /// </summary>
        internal class SystemMaintenanceError : Error
        {
            internal SystemMaintenanceError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(System.CodePrefix, (int)Codes.SystemMaintenance, messageProvider, details)
            { }
        }

        // --- Static Factory Methods ---

        /// <summary>
        /// Creates a new configuration error instance (code 01).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a configuration error.</returns>
        public static Error Configuration(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new ConfigurationError(
                ErrorMessageFactory.CreateProvider(message, "System_Configuration"),
                details
            );

        /// <summary>
        /// Creates a new dependency not registered error instance (code 02).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a missing dependency.</returns>
        public static Error DependencyNotRegistered(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new DependencyNotRegisteredError(
                ErrorMessageFactory.CreateProvider(message, "System_DependencyNotRegistered"),
                details
            );

        /// <summary>
        /// Creates a new out of memory error instance (code 03).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing an out of memory error.</returns>
        public static Error OutOfMemory(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new OutOfMemoryError(
                ErrorMessageFactory.CreateProvider(message, "System_OutOfMemory"),
                details
            );

        /// <summary>
        /// Creates a new thread aborted error instance (code 04).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a thread aborted error.</returns>
        public static Error ThreadAborted(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new ThreadAbortedError(
                ErrorMessageFactory.CreateProvider(message, "System_ThreadAborted"),
                details
            );

        /// <summary>
        /// Creates a new system maintenance error instance (code 05).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a system maintenance error.</returns>
        public static Error SystemMaintenance(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new SystemMaintenanceError(
                ErrorMessageFactory.CreateProvider(message, "System_SystemMaintenance"),
                details
            );
    }
}
