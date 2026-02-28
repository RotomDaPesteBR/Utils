using LightningArc.Utils.Results.Messages;

namespace LightningArc.Utils.Results;

public partial class Error
{
    /// <summary>
    /// Represents the data input and business rule validation error module.
    /// </summary>
    /// <remarks>
    /// This module contains errors related to data integrity, format, and compliance with schema or rules.
    /// The code prefix for errors in this module is 4.
    /// </remarks>
    public partial class Validation : ErrorModule
    {
        /// <summary>
        /// Gets the code prefix for the error category.
        /// </summary>
        /// <remarks>
        /// This value is used to categorize the error and is combined with a suffix to form the complete error code.
        /// </remarks>
        public new const int CodePrefix = (int)ModuleCodes.Validation;

        /// <summary>
        /// Defines the numeric suffixes for errors in the Validation module (prefix 4).
        /// These values are used to compose the complete error code (e.g., 4001, 4002, etc.).
        /// </summary>
        public enum Codes
        {
            /// <summary>
            /// Code '1'. The data format (JSON, XML, etc.) is incorrect or malformed.
            /// </summary>
            InvalidFormat = 1,

            /// <summary>
            /// Code '2'. The data structure or schema does not match the expected one.
            /// </summary>
            InvalidSchema = 2,

            /// <summary>
            /// Code '3'. Failed to convert a string or bytes to an object (deserialization).
            /// </summary>
            DeserializationFailed = 3,

            /// <summary>
            /// Code '4'. A required data field is missing from the input.
            /// </summary>
            MissingField = 4,

            /// <summary>
            /// Code '5'. A field value is below the minimum or above the maximum allowed.
            /// </summary>
            ValueOutOfRange = 5,
        }

        // --- Internal Error Classes ---

        /// <summary>
        /// Represents an invalid data format error (Suffix: 01).
        /// </summary>
        public sealed class InvalidFormatError : Error
        {
            internal InvalidFormatError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Validation.CodePrefix, (int)Codes.InvalidFormat, messageProvider, details)
            { }
        }

        /// <summary>
        /// Represents an invalid data schema error (Suffix: 02).
        /// </summary>
        public sealed class InvalidSchemaError : Error
        {
            internal InvalidSchemaError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Validation.CodePrefix, (int)Codes.InvalidSchema, messageProvider, details)
            { }
        }

        /// <summary>
        /// Represents a deserialization failed error (Suffix: 03).
        /// </summary>
        public sealed class DeserializationFailedError : Error
        {
            internal DeserializationFailedError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(
                    Validation.CodePrefix,
                    (int)Codes.DeserializationFailed,
                    messageProvider,
                    details
                ) { }
        }

        /// <summary>
        /// Represents a missing required field error (Suffix: 04).
        /// </summary>
        public sealed class MissingFieldError : Error
        {
            internal MissingFieldError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Validation.CodePrefix, (int)Codes.MissingField, messageProvider, details) { }
        }

        /// <summary>
        /// Represents a value out of range error (Suffix: 05).
        /// </summary>
        public sealed class ValueOutOfRangeError : Error
        {
            internal ValueOutOfRangeError(
                IMessageProvider messageProvider,
                IEnumerable<ErrorDetail>? details = null
            )
                : base(Validation.CodePrefix, (int)Codes.ValueOutOfRange, messageProvider, details)
            { }
        }

        // --- Static Factory Methods ---

        /// <summary>
        /// Creates a new invalid data format error instance (code 01).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing an invalid format.</returns>
        public static Error InvalidFormat(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new InvalidFormatError(
                ErrorMessageFactory.CreateProvider(message, "Validation_InvalidFormat"),
                details
            );

        /// <summary>
        /// Creates a new invalid data schema error instance (code 02).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing an invalid data schema.</returns>
        public static Error InvalidSchema(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new InvalidSchemaError(
                ErrorMessageFactory.CreateProvider(message, "Validation_InvalidSchema"),
                details
            );

        /// <summary>
        /// Creates a new deserialization failed error instance (code 03).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a deserialization failure.</returns>
        public static Error DeserializationFailed(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new DeserializationFailedError(
                ErrorMessageFactory.CreateProvider(message, "Validation_DeserializationFailed"),
                details
            );

        /// <summary>
        /// Creates a new missing required field error instance (code 04).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a missing required field.</returns>
        public static Error MissingField(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new MissingFieldError(
                ErrorMessageFactory.CreateProvider(message, "Validation_MissingField"),
                details
            );

        /// <summary>
        /// Creates a new value out of range error instance (code 05).
        /// </summary>
        /// <param name="message">A custom descriptive message. If not provided, the default localized message will be used.</param>
        /// <param name="details">A list of additional error details.</param>
        /// <returns>A new <see cref="Error"/> instance representing a value out of range.</returns>
        public static Error ValueOutOfRange(
            string? message = null,
            params IEnumerable<ErrorDetail>? details
        ) =>
            new ValueOutOfRangeError(
                ErrorMessageFactory.CreateProvider(message, "Validation_ValueOutOfRange"),
                details
            );
    }
}
