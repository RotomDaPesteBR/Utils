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

        /// <summary>
        /// Ensures that the given condition is true, otherwise returns a new failure <see cref="Result" />.
        /// </summary>
        /// <param name="result">The input <see cref="Result" />.</param>
        /// <param name="condition">The condition to check.</param>
        /// <param name="error">The error to return if the condition is false.</param>
        /// <returns>The input <see cref="Result" /> if the condition is true, otherwise a new failure <see cref="Result" />.</returns>
        public static Result Ensure(this Result result, bool condition, Error error) =>
            result.IsFailure || condition ? result : Result.Failure(error);

        /// <summary>
        /// Ensures that the given predicate is true, otherwise returns a new failure <see cref="Result" />.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="result">The input <see cref="Result{T}" />.</param>
        /// <param name="predicate">The predicate to check.</param>
        /// <param name="errorFactory">The function to create an error if the predicate is false.</param>
        /// <returns>The input <see cref="Result{T}" /> if the predicate is true, otherwise a new failure <see cref="Result{T}" />.</returns>
        public static Result<T> Ensure<T>(
            this Result<T> result,
            Func<T, bool> predicate,
            Func<Error> errorFactory
        ) =>
            result.IsFailure || predicate(result.Value!)
                ? result
                : Result<T>.Failure(errorFactory());

        /// <summary>
        /// Ensures that the given condition is true, otherwise returns a new failure <see cref="Result" />.
        /// </summary>
        /// <param name="result">The input <see cref="Result" />.</param>
        /// <param name="condition">The condition to check.</param>
        /// <param name="errorFactory">The function to create an error if the condition is false.</param>
        /// <returns>The input <see cref="Result" /> if the condition is true, otherwise a new failure <see cref="Result" />.</returns>
        public static Result Ensure(this Result result, bool condition, Func<Error> errorFactory) =>
            result.IsFailure || condition ? result : Result.Failure(errorFactory());

        /// <summary>
        /// Ensures that the given predicate is true, otherwise returns a new failure <see cref="Result" />.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="result">The input <see cref="Result{T}" />.</param>
        /// <param name="predicate">The predicate to check.</param>
        /// <param name="errorFactory">The function to create an error if the predicate is false.</param>
        /// <returns>The input <see cref="Result{T}" /> if the predicate is true, otherwise a new failure <see cref="Result{T}" />.</returns>
        public static Result<T> Ensure<T>(
            this Result<T> result,
            Func<T, bool> predicate,
            Func<T, Error> errorFactory
        ) =>
            result.IsFailure || predicate(result.Value!)
                ? result
                : Result<T>.Failure(errorFactory(result.Value!));
    }
}
