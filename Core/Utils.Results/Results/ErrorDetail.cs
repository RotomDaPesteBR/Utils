namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Represents a specific detail of an error, providing additional context and a descriptive message.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of <see cref="ErrorDetail"/>
    /// with a context and an error message.
    /// </remarks>
    /// <param name="context">The identifier that provides context about the error.</param>
    /// <param name="message">The specific error message for the context.</param>
    public readonly struct ErrorDetail(string context, string message)
    {
        /// <summary>
        /// Gets an identifier that provides additional context about the error.
        /// It can be a field name, a resource ID, a parameter name, etc.
        /// </summary>
        public string Context { get; } = context;

        /// <summary>
        /// Gets the specific error message for the context.
        /// </summary>
        public string Message { get; } = message;

        /// <summary>
        /// Allows implicit creation of an ErrorDetail from a tuple.
        /// </summary>
        public static implicit operator ErrorDetail((string Context, string Message) value)
        {
            return new ErrorDetail(value.Context, value.Message);
        }
    }
}
