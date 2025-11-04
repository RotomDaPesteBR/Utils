namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Provides extension methods for the <see cref="Result{TValue}"/> class,
    /// enabling the composition of functional operations.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        ///     Maps a <see cref="Result{TIn}" /> to <see cref="Result{TOut}" /> by applying a function to the contained value.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="result">The input <see cref="Result{TIn}" />.</param>
        /// <param name="mapper">The function to apply to the value.</param>
        /// <returns>A new <see cref="Result{TOut}" /> with the mapped value, or the original error.</returns>
        public static Result<TOut> Map<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, TOut> mapper
        ) =>
            !result.IsSuccess
                ? (Result<TOut>)result.Error
                : (Result<TOut>)result.SuccessDetails.Map(mapper);

        /// <summary>
        ///     Maps a <see cref="Result" /> to <see cref="Result{TOut}" /> by applying a function.
        /// </summary>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="result">The input <see cref="Result" />.</param>
        /// <param name="mapper">The function to apply.</param>
        /// <returns>A new <see cref="Result{TOut}" /> with the mapped value, or the original error.</returns>
        public static Result<TOut> Map<TOut>(this Result result, Func<TOut> mapper) =>
            !result.IsSuccess
                ? (Result<TOut>)result.Error
                : (Result<TOut>)result.SuccessDetails.Map(mapper);
    }
}
