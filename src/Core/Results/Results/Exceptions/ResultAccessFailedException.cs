namespace LightningArc.Results
{
    /// <summary>
    /// Exception thrown when an attempt to access the value or error of a <see cref="Result"/>
    /// is made in an invalid way (e.g., accessing the value of a failure <see cref="Result"/>).
    /// </summary>
    public class ResultAccessFailedException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultAccessFailedException"/> class.
        /// </summary>
        public ResultAccessFailedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultAccessFailedException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the exception.</param>
        public ResultAccessFailedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultAccessFailedException"/> class with an error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that describes the current exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the inner exception is not specified,
        /// use <c>null</c>.</param>
        public ResultAccessFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
