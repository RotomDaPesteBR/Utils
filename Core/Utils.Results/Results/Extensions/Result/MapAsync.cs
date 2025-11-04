namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Provides asynchronous extension methods for the <see cref="Result{TValue}"/> class,
    /// focused on the transformation (Map) of the internal success value.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        ///     Asynchronously maps a <see cref="Result{TIn}" /> to <see cref="Result{TOut}" /> by applying an asynchronous function to the contained value.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="result">The input <see cref="Result{TIn}" />.</param>
        /// <param name="mapper">The asynchronous function to apply to the value.</param>
        /// <returns>A new <see cref="Result{TOut}" /> with the mapped value, or the original error.</returns>
        public static async Task<Result<TOut>> MapAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Task<TOut>> mapper
        ) =>
            result.IsSuccess
                ? await result.SuccessDetails.MapAsync<TIn, TOut>(mapper).ConfigureAwait(false)
                : result.Error;

        /// <summary>
        ///     Asynchronously maps a <see cref="Result" /> to <see cref="Result{TOut}" /> by applying an asynchronous function.
        /// </summary>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="result">The input <see cref="Result" />.</param>
        /// <param name="mapper">The asynchronous function to apply.</param>
        /// <returns>A new <see cref="Result{TOut}" /> with the mapped value, or the original error.</returns>
        public static async Task<Result<TOut>> MapAsync<TOut>(
            this Result result,
            Func<Task<TOut>> mapper
        ) =>
            result.IsSuccess
                ? await result.SuccessDetails.MapAsync<TOut>(mapper).ConfigureAwait(false)
                : result.Error;

        /// <summary>
        ///     Asynchronously maps a <see cref="Result{TIn}" /> to <see cref="Result{TOut}" /> by applying a function to the contained value.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="resultTask">The input <see cref="Result{TIn}" />.</param>
        /// <param name="mapper">The function to apply to the value.</param>
        /// <returns>A new <see cref="Result{TOut}" /> with the mapped value, or the original error.</returns>
        public static async Task<Result<TOut>> MapAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, TOut> mapper
        ) => (await resultTask.ConfigureAwait(false)).Map(mapper);

        /// <summary>
        ///     Asynchronously maps a <see cref="Task{Result}" /> to <see cref="Result{TOut}" /> by applying a function.
        /// </summary>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="resultTask">The input <see cref="Task{Result}" />.</param>
        /// <param name="mapper">The function to apply.</param>
        /// <returns>A new <see cref="Result{TOut}" /> with the mapped value, or the original error.</returns>
        public static async Task<Result<TOut>> MapAsync<TOut>(
            this Task<Result> resultTask,
            Func<TOut> mapper
        ) => (await resultTask.ConfigureAwait(false)).Map(mapper);

        /// <summary>
        ///     Asynchronously maps a <see cref="Result{TIn}" /> to <see cref="Result{TOut}" /> by applying an asynchronous function to the contained value.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="resultTask">The input <see cref="Result{TIn}" />.</param>
        /// <param name="mapper">The asynchronous function to apply to the value.</param>
        /// <returns>A new <see cref="Result{TOut}" /> with the mapped value, or the original error.</returns>
        public static async Task<Result<TOut>> MapAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, Task<TOut>> mapper
        ) =>
            await (await resultTask.ConfigureAwait(false))
                .MapAsync(mapper)
                .ConfigureAwait(false);

        /// <summary>
        ///     Asynchronously maps a <see cref="Task{Result}" /> to <see cref="Result{TOut}" /> by applying an asynchronous function.
        /// </summary>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="resultTask">The input <see cref="Task{Result}" />.</param>
        /// <param name="mapper">The asynchronous function to apply.</param>
        /// <returns>A new <see cref="Result{TOut}" /> with the mapped value, or the original error.</returns>
        public static async Task<Result<TOut>> MapAsync<TOut>(
            this Task<Result> resultTask,
            Func<Task<TOut>> mapper
        ) =>
            await (await resultTask.ConfigureAwait(false))
                .MapAsync(mapper)
                .ConfigureAwait(false);
    }
}
