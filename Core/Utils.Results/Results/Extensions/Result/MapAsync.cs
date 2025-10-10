// File: ResultExtensions.MapAsync.cs

namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Provides asynchronous extension methods for the <see cref="Result{TValue}"/> class,
    /// focused on the transformation (Map) of the internal success value.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Awaits an asynchronous input result and then maps the success value using an asynchronous function.
        /// If the awaited result is a failure, the error is propagated.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="resultTask">The asynchronous input result.</param>
        /// <param name="mapper">The asynchronous mapping function to apply to the successful value, which returns <see cref="Task{TResult}">Task&lt;TOut&gt;</see>.</param>
        /// <returns>A <see cref="Task{TResult}">Task&lt;Result&lt;TOut&gt;&gt;</see> containing the mapped value on success, or the original error.</returns>
        public static async Task<Result<TOut>> MapAsync<TIn, TOut>( 
            this Task<Result<TIn>> resultTask,
            Func<TIn, Task<TOut>> mapper
        )
        {
            var result = await resultTask.ConfigureAwait(false);
            if (result.IsSuccess)
            {
                // result.Value é non-null quando IsSuccess é true.
                return Result.Success(
                    await mapper(result.Value!).ConfigureAwait(false),
                    result.SuccessDetails!
                );
            }
            return result.Error;
        }

        /// <summary>
        /// Awaits an asynchronous input result and then maps the success value using a synchronous function.
        /// If the awaited result is a failure, the error is propagated.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="resultTask">The asynchronous input result.</param>
        /// <param name="mapper">The synchronous mapping function to apply to the successful value, which returns <typeparamref name="TOut"/>.</param>
        /// <returns>A <see cref="Task{TResult}">Task&lt;Result&lt;TOut&gt;&gt;</see> containing the mapped value on success, or the original error.</returns>
        public static async Task<Result<TOut>> MapAsync<TIn, TOut>( // Renomeado para MapAsync
            this Task<Result<TIn>> resultTask,
            Func<TIn, TOut> mapper
        )
        {
            var result = await resultTask.ConfigureAwait(false);
            if (result.IsSuccess)
            {
                // result.Value é non-null quando IsSuccess é true.
                return Result.Success(mapper(result.Value!), result.SuccessDetails!);
            }
            return result.Error;
        }
    }
}