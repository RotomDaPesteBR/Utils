using LightningArc.Utils.Results.Messages;

namespace LightningArc.Utils.Results;

public partial class Error
{
    /// <summary>
    /// Represents the input/output (I/O) error module.
    /// </summary>
    /// <remarks>
    /// This module contains errors that occur during operations with files,
    /// directories, or streams, such as file not found, permission denied, or disk issues.
    /// The code prefix for errors in this module is 11.
    /// </remarks>
    public partial class IO : ErrorModule
    {
        /// <summary>
        /// Gets the error category code prefix.
        /// </summary>
        /// <remarks>
        /// This value is used to categorize the error and is combined with a suffix to form the complete error code.
        /// </remarks>
        public new const int CodePrefix = (int)ModuleCodes.IO;

        /// <summary>
        /// Defines the numeric suffixes for IO module errors (prefix 11).
        /// These values are used to compose the complete error code (e.g., 11001, 11002, etc.).
        /// </summary>
        public enum Codes
        {
            /// <summary>
            /// Code '1'. The specified file does not exist at the provided path.
            /// </summary>
            FileNotFound = 1,

            /// <summary>
            /// Code '2'. The specified directory path does not exist.
            /// </summary>
            DirectoryNotFound = 2,

            /// <summary>
            /// Code '3'. The user or process does not have the necessary permissions to read/write/access the resource.
            /// </summary>
            PermissionDenied = 3,

            /// <summary>
            /// Code '4'. The write operation failed because there is no available disk space.
            /// </summary>
            DiskFull = 4,

            /// <summary>
            /// Code '5'. The file was read, but its content is in an invalid or unreadable format.
            /// </summary>
            CorruptedFile = 5,
        }

        // --- Internal Error Classes ---

        /// <summary>
        /// Represents a file not found error (Suffix: 01).
        /// </summary>
        public sealed class FileNotFoundError : Error
        {
            internal FileNotFoundError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(IO.CodePrefix, (int)Codes.FileNotFound, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a directory not found error (Suffix: 05).
        /// </summary>
        public sealed class DirectoryNotFoundError : Error
        {
            internal DirectoryNotFoundError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(IO.CodePrefix, (int)Codes.DirectoryNotFound, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a permission denied error (Suffix: 02).
        /// </summary>
        public sealed class PermissionDeniedError : Error
        {
            internal PermissionDeniedError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(IO.CodePrefix, (int)Codes.PermissionDenied, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a disk full error (Suffix: 04).
        /// </summary>
        public sealed class DiskFullError : Error
        {
            internal DiskFullError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(IO.CodePrefix, (int)Codes.DiskFull, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a corrupted file error (Suffix: 03).
        /// </summary>
        public sealed class CorruptedFileError : Error
        {
            internal CorruptedFileError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(IO.CodePrefix, (int)Codes.CorruptedFile, messageProvider, details) { }
        }

        // --- Static Factory Methods ---

        /// <summary>
        /// Creates a new file not found error instance (code 01).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a file not found error.</returns>
        public static Error FileNotFound(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new FileNotFoundError(
                ErrorMessageFactory.CreateProvider(message, "IO_FileNotFound"),
                details
            );

        /// <summary>
        /// Creates a new directory not found error instance (code 02).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a directory not found error.</returns>
        public static Error DirectoryNotFound(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new DirectoryNotFoundError(
                ErrorMessageFactory.CreateProvider(message, "IO_DirectoryNotFound"),
                details
            );

        /// <summary>
        /// Creates a new permission denied error instance (code 03).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a permission denied error.</returns>
        public static Error PermissionDenied(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new PermissionDeniedError(
                ErrorMessageFactory.CreateProvider(message, "IO_PermissionDenied"),
                details
            );

        /// <summary>
        /// Creates a new disk full error instance (code 04).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a disk full error.</returns>
        public static Error DiskFull(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) => new DiskFullError(ErrorMessageFactory.CreateProvider(message, "IO_DiskFull"), details);

        /// <summary>
        /// Creates a new corrupted file error instance (code 05).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a corrupted file error.</returns>
        public static Error CorruptedFile(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new CorruptedFileError(
                ErrorMessageFactory.CreateProvider(message, "IO_CorruptedFile"),
                details
            );
    }
}
