namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Provides extension methods for the <see cref="Result{TValue}"/> class,
    /// enabling the composition of functional operations.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Maps (transforms) the value of a successful <see cref="Result{TIn}"/> to a new value type, <typeparamref name="TOut"/>.
        /// If the current result is a failure, the error is propagated to the new result without executing the mapper.
        /// This is the standard **Map** or **Select** operation.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="result">The input result.</param>
        /// <param name="mapper">The synchronous mapping function to apply to the successful value.</param>
        /// <returns>A new <see cref="Result{TOut}"/> containing the mapped value on success,
        /// or the original error on failure.</returns>
        public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> mapper)
        {
            if (result.IsSuccess)
            {
                // result.Value is non-null when IsSuccess is true.
                return Result.Success(mapper(result.Value!), result.SuccessDetails!);
            }
            else
            {
                // The implicit conversion from Error to Result<TOut> applies here.
                return result.Error;
            }
        }
    }
}