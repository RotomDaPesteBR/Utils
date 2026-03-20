using LightningArc.Utils.Results.Messages;

namespace LightningArc.Utils.Results;

public partial class Error
{
    /// <summary>
    /// Represents the resource error module.
    /// </summary>
    /// <remarks>
    /// This module contains errors that occur when there are problems
    /// managing a resource, such as it not being found or
    /// already existing. The code prefix for errors in this module is 5.
    /// </remarks>
    public partial class Resource : ErrorModule
    {
        /// <summary>
        /// Gets the code prefix for the error category.
        /// </summary>
        /// <remarks>
        /// This value is used to categorize the error and is combined with a suffix to form the complete error code.
        /// </remarks>
        public new const int CodePrefix = (int)ModuleCodes.Resource;

        /// <summary>
        /// Defines the numeric suffixes for errors in the Resource module (prefix 5).
        /// These values are used to compose the complete error code (e.g., 5001, 5002, etc.).
        /// </summary>
        public enum Codes
        {
            /// <summary>
            /// Code '1'. The requested resource does not exist.
            /// </summary>
            NotFound = 1,

            /// <summary>
            /// Code '2'. Attempt to create a resource that already exists.
            /// </summary>
            AlreadyExists = 2,

            /// <summary>
            /// Code '3'. The resource exists but cannot be accessed or used at the moment.
            /// </summary>
            Unavailable = 3,

            /// <summary>
            /// Code '4'. The resource is not in the expected state for the operation (e.g., trying to delete an already deleted resource).
            /// </summary>
            InvalidState = 4,

            /// <summary>
            /// Code '5'. The requested resource is obsolete or discontinued.
            /// </summary>
            Obsolete = 5,
        }

        // --- Internal Error Classes ---

        /// <summary>
        /// Represents a resource not found error (Suffix: 01).
        /// </summary>
        public sealed class NotFoundError : Error
        {
            internal NotFoundError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Resource.CodePrefix, (int)Codes.NotFound, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a resource already exists error (Suffix: 02).
        /// </summary>
        public sealed class AlreadyExistsError : Error
        {
            internal AlreadyExistsError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Resource.CodePrefix, (int)Codes.AlreadyExists, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a resource unavailable error (Suffix: 03).
        /// </summary>
        public sealed class UnavailableError : Error
        {
            internal UnavailableError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Resource.CodePrefix, (int)Codes.Unavailable, messageProvider, details) { }
        }

        /// <summary>
        /// Represents an invalid resource state error (Suffix: 04).
        /// </summary>
        public sealed class InvalidStateError : Error
        {
            internal InvalidStateError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Resource.CodePrefix, (int)Codes.InvalidState, messageProvider, details) { }
        }

        /// <summary>
        /// Represents an obsolete resource error (Suffix: 05).
        /// </summary>
        public sealed class ObsoleteError : Error
        {
            internal ObsoleteError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Resource.CodePrefix, (int)Codes.Obsolete, messageProvider, details) { }
        }

        // --- Static Factory Methods ---

        /// <summary>
        /// Creates a new resource not found error instance (code 01).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a resource not found error.</returns>
        public static Error NotFound(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new NotFoundError(
                ErrorMessageFactory.CreateProvider(message, "Resource_NotFound"),
                details
            );

        /// <summary>
        /// Creates a new resource already exists error instance (code 02).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a resource that already exists.</returns>
        public static Error AlreadyExists(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new AlreadyExistsError(
                ErrorMessageFactory.CreateProvider(message, "Resource_AlreadyExists"),
                details
            );

        /// <summary>
        /// Creates a new resource unavailable error instance (code 03).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing an unavailable resource.</returns>
        public static Error Unavailable(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new UnavailableError(
                ErrorMessageFactory.CreateProvider(message, "Resource_Unavailable"),
                details
            );

        /// <summary>
        /// Creates a new invalid resource state error instance (code 04).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing an invalid resource state.</returns>
        public static Error InvalidState(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new InvalidStateError(
                ErrorMessageFactory.CreateProvider(message, "Resource_InvalidState"),
                details
            );

        /// <summary>
        /// Creates a new obsolete resource error instance (code 05).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing an obsolete resource.</returns>
        public static Error Obsolete(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new ObsoleteError(
                ErrorMessageFactory.CreateProvider(message, "Resource_Obsolete"),
                details
            );
    }
}
