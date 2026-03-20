namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Provides extension methods for the <see cref="Result"/> and <see cref="Result{TValue}"/> classes,
    /// enabling the composition of functional operations.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        ///     Executes the given action if the <see cref="Result" /> is a failure.
        /// </summary>
        /// <param name="result">The input <see cref="Result" />.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns>The input <see cref="Result" />.</returns>
        public static Result OnFailure(this Result result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }
            return result;
        }

        /// <summary>
        ///     Executes the given action if the <see cref="Result" /> is a failure.
        /// </summary>
        /// <param name="result">The input <see cref="Result" />.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns>The input <see cref="Result" />.</returns>
        public static Result OnFailure(this Result result, Action<Error> action)
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }
            return result;
        }

        /// <summary>
        ///     Executes the given action if the <see cref="Result{T}" /> is a failure.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="result">The input <see cref="Result{T}" />.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns>The input <see cref="Result{T}" />.</returns>
        public static Result<T> OnFailure<T>(this Result<T> result, Action action)
        {
            if (result.IsFailure)
            {
                action();
            }
            return result;
        }

        /// <summary>
        ///     Executes the given action if the <see cref="Result{T}" /> is a failure.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="result">The input <see cref="Result{T}" />.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns>The input <see cref="Result{T}" />.</returns>
        public static Result<T> OnFailure<T>(this Result<T> result, Action<Error> action)
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }
            return result;
        }
    }
}
