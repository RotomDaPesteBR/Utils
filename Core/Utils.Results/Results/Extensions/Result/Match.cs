namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Provides extension methods for the <see cref="Result"/> and <see cref="Result{TValue}"/> classes,
    /// enabling the composition of functional operations (e.g., mapping, unwrapping).
    /// </summary>
    public static partial class ResultExtensions
    {
        #region Result
        /// <summary>
        /// Maps a non-generic result (without value) to a new <see cref="Result{TOut}"/>.
        /// </summary>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="result">The input result (without value).</param>
        /// <param name="success">The function to apply on success, returning <see cref="Result{TOut}"/>.</param>
        /// <param name="failure">The function to apply on failure, returning <see cref="Result{TOut}"/>.</param>
        /// <returns>The new mapped <see cref="Result{TOut}"/>.</returns>
        public static Result<TOut> Match<TOut>(
            this Result result,
            Func<Result<TOut>> success,
            Func<Error, Result<TOut>> failure
        )
        {
            return result.IsSuccess ? success() : failure(result.Error);
        }

        /// <summary>
        /// Maps a non-generic result to a new <see cref="Result{TOut}"/> where the success function returns the raw <typeparamref name="TOut"/> value,
        /// which is then wrapped in a <see cref="Result{TOut}"/>.
        /// </summary>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="result">The input result (without value).</param>
        /// <param name="success">The function to apply on success, returning the raw <typeparamref name="TOut"/> value (which is automatically wrapped).</param>
        /// <param name="failure">The function to apply on failure, returning <see cref="Result{TOut}"/>.</param>
        /// <returns>The new mapped <see cref="Result{TOut}"/>.</returns>
        public static Result<TOut> Match<TOut>(
            this Result result,
            Func<TOut> success,
            Func<Error, Result<TOut>> failure
        )
        {
            // Uses the implicit conversion of TOut to Result<TOut> or explicit Result.Success()
            return result.IsSuccess
                ? Result.Success(success(), result.SuccessDetails)
                : failure(result.Error);
        }

        /// <summary>
        /// Maps a non-generic result (without value) to another non-generic <see cref="Result"/>.
        /// </summary>
        /// <param name="result">The input result (without value).</param>
        /// <param name="success">The function to apply on success, returning <see cref="Result"/>.</param>
        /// <param name="failure">The function to apply on failure, returning <see cref="Result"/>.</param>
        /// <returns>The new mapped <see cref="Result"/>.</returns>
        public static Result Match(
            this Result result,
            Func<Result> success,
            Func<Error, Result> failure
        )
        {
            return result.IsSuccess ? success() : failure(result.Error);
        }

        /// <summary>
        /// Unwraps a non-generic result (without value) by applying one of two functions,
        /// returning the final raw value <typeparamref name="TOut"/>. This method ends the Result chain.
        /// </summary>
        /// <typeparam name="TOut">The type of the output (unwrapped) value.</typeparam>
        /// <param name="result">The input result (without value).</param>
        /// <param name="success">The function to apply on success, which returns <typeparamref name="TOut"/>.</param>
        /// <param name="failure">The function to apply on failure, which returns <typeparamref name="TOut"/>.</param>
        /// <returns>The final <typeparamref name="TOut"/> value.</returns>
        public static TOut Match<TOut>(
            this Result result,
            Func<TOut> success,
            Func<Error, TOut> failure
        )
        {
            // 'result.Error!' is safe because we check IsSuccess first.
            return result.IsSuccess ? success() : failure(result.Error!);
        }

        /// <summary>
        /// Maps a non-generic result by applying one of two functions, where the success function
        /// receives the original success details (<see cref="Success"/> object).
        /// </summary>
        /// <param name="result">The input result (without value).</param>
        /// <param name="success">The function to apply to the success details, returning <see cref="Result"/>.</param>
        /// <param name="failure">The function to apply on failure, returning <see cref="Result"/>.</param>
        /// <returns>The new mapped <see cref="Result"/>.</returns>
        public static Result Match(
            this Result result,
            Func<Success, Result> success,
            Func<Error, Result> failure
        )
        {
            // 'result.SuccessDetails' is safe because we check IsSuccess first.
            return result.IsSuccess ? success(result.SuccessDetails) : failure(result.Error);
        }
        #endregion

        #region Result<TValue>
        /// <summary>
        /// Maps a generic result to a new <see cref="Result{TOut}"/> where the success function returns the raw <typeparamref name="TOut"/> value,
        /// which is then wrapped in a <see cref="Result{TOut}"/>.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="result">The input result.</param>
        /// <param name="success">The function to apply to the success details, returning the raw <typeparamref name="TOut"/> value (which is automatically wrapped).</param>
        /// <param name="failure">The function to apply on failure, returning <see cref="Result{TOut}"/>.</param>
        /// <returns>The new mapped <see cref="Result{TOut}"/>.</returns>
        public static Result<TOut> Match<TIn, TOut>(
            this Result<TIn> result,
            Func<Success<TIn>, TOut> success,
            Func<Error, Result<TOut>> failure
        )
        {
            return result.IsSuccess
                ? Result.Success(success(result.SuccessDetails), result.SuccessDetails)
                : failure(result.Error);
        }

        /// <summary>
        /// Maps a generic result by applying one of two functions: one for success and one for failure.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="result">The input result.</param>
        /// <param name="success">The function to apply to the success details, returning <see cref="Result{TOut}"/>.</param>
        /// <param name="failure">The function to apply on failure, returning <see cref="Result{TOut}"/>.</param>
        /// <returns>The new mapped <see cref="Result{TOut}"/>.</returns>
        public static Result<TOut> Match<TIn, TOut>(
            this Result<TIn> result,
            Func<Success<TIn>, Result<TOut>> success,
            Func<Error, Result<TOut>> failure
        )
        {
            return result.IsSuccess ? success(result.SuccessDetails) : failure(result.Error);
        }

        /// <summary>
        /// Unwraps a <see cref="Result{TIn}"/> by applying one of two functions,
        /// returning the final raw value <typeparamref name="TOut"/>. This method ends the <see cref="Result"/> chain.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output (unwrapped) value.</typeparam>
        /// <param name="result">The input result.</param>
        /// <param name="success">The function to apply to the success details, which returns <typeparamref name="TOut"/>.</param>
        /// <param name="failure">The function to apply on failure, which returns <typeparamref name="TOut"/>.</param>
        /// <returns>The final <typeparamref name="TOut"/> value.</returns>
        public static TOut Match<TIn, TOut>(
            this Result<TIn> result,
            Func<Success<TIn>, TOut> success,
            Func<Error, TOut> failure
        )
        {
            return result.IsSuccess ? success(result.SuccessDetails!) : failure(result.Error!);
        }
        #endregion
    }
}