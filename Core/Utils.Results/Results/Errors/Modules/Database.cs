using LightningArc.Utils.Results.Messages;

namespace LightningArc.Utils.Results;

public partial class Error
{
    /// <summary>
    /// Represents the database error module.
    /// </summary>
    /// <remarks>
    /// This module contains errors that occur during interaction with the database,
    /// such as connection failures or constraint violations. The code prefix
    /// for errors in this module is 3.
    /// </remarks>
    public partial class Database : ErrorModule
    {
        /// <summary>
        /// Gets the error category code prefix.
        /// </summary>
        /// <remarks>
        /// This value is used to categorize the error and is combined with a suffix to form the complete error code.
        /// </remarks>
        public new const int CodePrefix = (int)ModuleCodes.Database;

        /// <summary>
        /// Defines the numeric suffixes for Database module errors (prefix 3).
        /// These values are used to compose the complete error code (e.g., 3001, 3002, etc.).
        /// </summary>
        public enum Codes
        {
            /// <summary>
            /// Code '1'. Failed to establish a connection with the database server.
            /// </summary>
            ConnectionFailed = 1,

            /// <summary>
            /// Code '2'. Error during the execution of an SQL command (query) in the database.
            /// </summary>
            QueryExecutionFailed = 2,

            /// <summary>
            /// Code '3'. Attempt to violate a rule (unique key, foreign key, etc.) of the database schema.
            /// </summary>
            ConstraintViolation = 3,

            /// <summary>
            /// Code '4'. Temporary error that can be resolved with a retry.
            /// </summary>
            Transient = 4,

            /// <summary>
            /// Code '5'. Two or more transactions mutually blocked each other.
            /// </summary>
            Deadlock = 5,
        }

        // --- Classes Internas de Erro ---

        /// <summary>
        /// Represents a database connection failed error (Suffix: 01).
        /// </summary>
        public sealed class ConnectionFailedError : Error
        {
            internal ConnectionFailedError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Database.CodePrefix, (int)Codes.ConnectionFailed, messageProvider, details)
            { }
        }

        /// <summary>
        /// Represents a query execution failed error (Suffix: 02).
        /// </summary>
        public sealed class QueryExecutionFailedError : Error
        {
            internal QueryExecutionFailedError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(
                    Database.CodePrefix,
                    (int)Codes.QueryExecutionFailed,
                    messageProvider,
                    details
                ) { }
        }

        /// <summary>
        /// Represents a constraint violation error (Suffix: 03).
        /// </summary>
        public sealed class ConstraintViolationError : Error
        {
            internal ConstraintViolationError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(
                    Database.CodePrefix,
                    (int)Codes.ConstraintViolation,
                    messageProvider,
                    details
                ) { }
        }

        /// <summary>
        /// Represents a transient database error (Suffix: 04).
        /// </summary>
        public sealed class TransientError : Error
        {
            internal TransientError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Database.CodePrefix, (int)Codes.Transient, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a deadlock error (Suffix: 05).
        /// </summary>
        public sealed class DeadlockError : Error
        {
            internal DeadlockError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Database.CodePrefix, (int)Codes.Deadlock, messageProvider, details) { }
        }

        // --- Construtores Estáticos ---

        /// <summary>
        /// Creates a new instance of a connection failed error (code 01).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a connection failure.</returns>
        public static Error ConnectionFailed(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new ConnectionFailedError(
                ErrorMessageFactory.CreateProvider(message, "Database_ConnectionFailed"),
                details
            );

        /// <summary>
        /// Creates a new instance of a query execution failed error (code 02).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a query execution failure.</returns>
        public static Error QueryExecutionFailed(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new QueryExecutionFailedError(
                ErrorMessageFactory.CreateProvider(message, "Database_QueryExecutionFailed"),
                details
            );

        /// <summary>
        /// Creates a new instance of a constraint violation error (code 03).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a constraint violation.</returns>
        public static Error ConstraintViolation(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new ConstraintViolationError(
                ErrorMessageFactory.CreateProvider(message, "Database_ConstraintViolation"),
                details
            );

        /// <summary>
        /// Creates a new instance of a transient database error (code 04).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a transient error.</returns>
        public static Error Transient(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new TransientError(
                ErrorMessageFactory.CreateProvider(message, "Database_Transient"),
                details
            );

        /// <summary>
        /// Creates a new instance of a deadlock error (code 05).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a deadlock.</returns>
        public static Error Deadlock(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new DeadlockError(
                ErrorMessageFactory.CreateProvider(message, "Database_Deadlock"),
                details
            );
    }
}
