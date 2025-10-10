namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Provides extension methods for the <see cref="Result{TValue}"/> class,
    /// enabling the composition of functional operations.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Checks a condition and, if it is false, transforms the successful result into a failed result
        /// using the provided error. Does nothing if an existing failure is present.
        /// This is the synchronous **Ensure** operation for validation checks.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="result">The input result.</param>
        /// <param name="predicate">The condition (function) to be checked against the success value.</param>
        /// <param name="error">The error to be returned if the condition is false.</param>
        /// <returns>The original result (if successful and the condition is true), or a failed result.</returns>
        public static Result<TValue> Ensure<TValue>(
            this Result<TValue> result,
            Func<TValue, bool> predicate,
            Error error
        )
        {
            // If already failed, propagate the failure immediately.
            if (result.IsFailure)
            {
                return result;
            }

            // If successful, check the predicate.
            // result.Value is non-null here.
            return predicate(result.Value!) ? result : error;
        }
    }
}