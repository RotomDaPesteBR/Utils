using LightningArc.Utils.Results.Messages;

namespace LightningArc.Utils.Results;

public partial class Error
{
    /// <summary>
    /// Represents the concurrency error module.
    /// </summary>
    /// <remarks>
    /// This module contains errors that occur when multiple processes attempt
    /// to access or modify the same resource simultaneously, resulting in conflicts.
    /// The code prefix for errors in this module is 10.
    /// </remarks>
    public partial class Concurrency : ErrorModule
    {
        /// <summary>
        /// Gets the error category code prefix.
        /// </summary>
        /// <remarks>
        /// This value is used to categorize the error and is combined with a suffix to form the complete error code.
        /// </remarks>
        public new const int CodePrefix = (int)ModuleCodes.Concurrency;

        /// <summary>
        /// Defines the numeric suffixes for Concurrency module errors (prefix 10).
        /// These values are used to compose the complete error code (e.g., 10001, 10002, etc.).
        /// </summary>
        public enum Codes
        {
            /// <summary>
            /// Code '1'. A generic concurrency conflict detected (e.g., attempt to save data that was modified by another process).
            /// </summary>
            Conflict = 1,

            /// <summary>
            /// Code '2'. The resource is explicitly locked by a transaction or lock from another process.
            /// </summary>
            Locked = 2,

            /// <summary>
            /// Code '3'. The data being used for an operation is already stale, and the operation failed to prevent inconsistency.
            /// </summary>
            StaleData = 3,

            /// <summary>
            /// Code '4'. The resource is temporarily unavailable because it is actively being processed by another thread or operation.
            /// </summary>
            ResourceInUse = 4,
        }

        // --- Classes Internas de Erro ---

        /// <summary>
        /// Represents a concurrency conflict error (Suffix: 01).
        /// </summary>
        internal class ConflictError : Error
        {
            internal ConflictError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Concurrency.CodePrefix, (int)Codes.Conflict, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a locked resource error (Suffix: 02).
        /// </summary>
        internal class LockedError : Error
        {
            internal LockedError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Concurrency.CodePrefix, (int)Codes.Locked, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a stale data error (Suffix: 03).
        /// </summary>
        internal class StaleDataError : Error
        {
            internal StaleDataError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Concurrency.CodePrefix, (int)Codes.StaleData, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a resource in use error (Suffix: 04).
        /// </summary>
        internal class ResourceInUseError : Error
        {
            internal ResourceInUseError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Concurrency.CodePrefix, (int)Codes.ResourceInUse, messageProvider, details)
            { }
        }

        // --- Construtores Estáticos ---

        /// <summary>
        /// Creates a new instance of a conflict error (code 01).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a conflict.</returns>
        public static Error Conflict(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new ConflictError(
                ErrorMessageFactory.CreateProvider(message, "Concurrency_Conflict"),
                details
            );

        /// <summary>
        /// Creates a new instance of a locked resource error (code 02).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a locked resource.</returns>
        public static Error Locked(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new LockedError(
                ErrorMessageFactory.CreateProvider(message, "Concurrency_Locked"),
                details
            );

        /// <summary>
        /// Creates a new instance of a stale data error (code 03).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing stale data.</returns>
        public static Error StaleData(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new StaleDataError(
                ErrorMessageFactory.CreateProvider(message, "Concurrency_StaleData"),
                details
            );

        /// <summary>
        /// Creates a new instance of a resource in use error (code 04).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a resource in use.</returns>
        public static Error ResourceInUse(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new ResourceInUseError(
                ErrorMessageFactory.CreateProvider(message, "Concurrency_ResourceInUse"),
                details
            );
    }
}
