namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Provides asynchronous extension methods for the <see cref="Result{TValue}"/> class,
    /// focused on chaining (Bind) operations that might fail.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Chains a synchronous result with a new asynchronous operation that might fail.
        /// If the input result is a failure, the error is propagated without executing the function.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="result">The input result (<see cref="Result{TIn}"/>).</param>
        /// <param name="func">The asynchronous function to apply to the successful value, which returns a new <see cref="Task{TResult}">Task&lt;Result&lt;TOut&gt;&gt;</see>.</param>
        /// <returns>The <see cref="Task{TResult}">Task&lt;Result&lt;TOut&gt;&gt;</see> produced by <paramref name="func"/>, or the original error.</returns>
        public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Task<Result<TOut>>> func
        )
        {
            if (result.IsSuccess)
            {
                // result.Value is non-null when IsSuccess is true.
                return await func(result.Value!).ConfigureAwait(false);
            }
            // The implicit conversion from Error to Task<Result<TOut>> is handled.
            return result.Error;
        }

        /// <summary>
        /// Awaits an asynchronous input result and then chains it with a new asynchronous operation that might fail.
        /// If the awaited result is a failure, the error is propagated.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="resultTask">The asynchronous input result.</param>
        /// <param name="func">The asynchronous function to apply to the successful value, which returns a new <see cref="Task{TResult}">Task&lt;Result&lt;TOut&gt;&gt;</see>.</param>
        /// <returns>The <see cref="Task{TResult}">Task&lt;Result&lt;TOut&gt;&gt;</see> produced by <paramref name="func"/>, or the original error.</returns>
        public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, Task<Result<TOut>>> func
        )
        {
            var result = await resultTask.ConfigureAwait(false);
            if (result.IsSuccess)
            {
                // result.Value is non-null when IsSuccess is true.
                return await func(result.Value!).ConfigureAwait(false);
            }
            return result.Error;
        }

        /// <summary>
        /// Awaits an asynchronous input result and then chains it with a new **synchronous** operation that might fail.
        /// If the awaited result is a failure, the error is propagated.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="resultTask">The asynchronous input result.</param>
        /// <param name="func">The synchronous function to apply to the successful value, which returns a new <see cref="Result{TOut}"/>.</param>
        /// <returns>The <see cref="Task{TResult}">Task&lt;Result&lt;TOut&gt;&gt;</see> containing the result produced by <paramref name="func"/>, or the original error.</returns>
        public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, Result<TOut>> func
        )
        {
            var result = await resultTask.ConfigureAwait(false);
            if (result.IsSuccess)
            {
                // result.Value is non-null when IsSuccess is true.
                return func(result.Value!);
            }
            return result.Error;
        }
    }
}