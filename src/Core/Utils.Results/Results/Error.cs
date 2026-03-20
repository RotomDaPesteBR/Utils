using LightningArc.Utils.Results.Messages;
using System.Globalization;

namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Represents a standardized error object for the application.
    /// </summary>
    /// <remarks>
    /// This base class is used in conjunction with the Result pattern to provide
    /// clear information about operation failures. Errors can be mapped to HTTP
    /// status codes in the application's presentation layer.
    /// <para>
    /// The error code is composed of a prefix (category) and a suffix (specific error).
    /// For example: 1001 (prefix 10 for "Application", suffix 01 for "Internal").
    /// </para>
    /// </remarks>
    public partial class Error
    {
        private readonly IMessageProvider _messageProvider;

        /// <summary>
        /// Gets the full error code, composed of a category prefix and a specific suffix.
        /// </summary>
        public int Code => CodePrefix * 1000 + CodeSuffix;

        /// <summary>
        /// Gets the error code prefix, representing the error category (e.g., 10 for Application).
        /// </summary>
        private protected int CodePrefix { get; }

        /// <summary>
        /// Gets the error code suffix, representing the specific error within a category (e.g., 01 for Internal Error).
        /// </summary>
        private protected int CodeSuffix { get; }

        /// <summary>
        /// Gets the descriptive message for the error.
        /// This message is either a literal string or a localized string retrieved using a resource key.
        /// </summary>
        public string Message => _messageProvider.GetMessage(CultureInfo.CurrentCulture);

        /// <summary>
        /// Gets a list of additional error details, useful for validations with multiple issues.
        /// </summary>
        public IReadOnlyList<ErrorDetail> Details { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class with a literal message.
        /// </summary>
        /// <param name="codePrefix">The error code prefix, representing the category.</param>
        /// <param name="codeSuffix">The error code suffix, representing the specific error.</param>
        /// <param name="messageProvider">The message provider for the error.</param>
        /// <param name="details">Additional error details.</param>
        protected Error(
            int codePrefix,
            int codeSuffix,
            IMessageProvider messageProvider,
            IEnumerable<ErrorDetail>? details = null
        )
        {
            if (codePrefix <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(codePrefix),
                    "Code prefix must be a positive integer."
                );
            }

            if (codeSuffix <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(codeSuffix),
                    "Code suffix must be a positive integer."
                );
            }

            CodePrefix = codePrefix;
            CodeSuffix = codeSuffix;
            _messageProvider = messageProvider;
            Details = details?.ToList() ?? [];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class with a literal message.
        /// </summary>
        /// <param name="codePrefix">The error code prefix, representing the category.</param>
        /// <param name="codeSuffix">The error code suffix, representing the specific error.</param>
        /// <param name="message">The message provider for the error.</param>
        /// <param name="details">Additional error details.</param>
        protected Error(
            int codePrefix,
            int codeSuffix,
            string message,
            IEnumerable<ErrorDetail>? details = null
        )
        {
            if (codePrefix <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(codePrefix),
                    "Code prefix must be a positive integer."
                );
            }

            if (codeSuffix <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(codeSuffix),
                    "Code suffix must be a positive integer."
                );
            }

            CodePrefix = codePrefix;
            CodeSuffix = codeSuffix;
            _messageProvider = new LiteralMessageProvider(message);
            Details = details?.ToList() ?? [];
        }
    }
}
