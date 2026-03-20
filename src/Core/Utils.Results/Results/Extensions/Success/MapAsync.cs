namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Provides extension methods for the <see cref="Success{TValue}"/> class,
    /// enabling fluent value transformation (mapping).
    /// </summary>
    public static partial class SuccessExtensions
    {
        /// <summary>
        ///     Asynchronously maps a <see cref="Success{TIn}" /> to <see cref="Success{TOut}" /> by applying an asynchronous function to the contained value.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="success">The input <see cref="Success{TIn}" />.</param>
        /// <param name="mapper">The asynchronous function to apply to the value.</param>
        /// <returns>A new <see cref="Success{TOut}" /> with the mapped value.</returns>
        public static async Task<Success<TOut>> MapAsync<TIn, TOut>(
            this Success<TIn> success,
            Func<TIn, Task<TOut>> mapper
        ) => success.WithValue(await mapper(success.Value!).ConfigureAwait(false));

        /// <summary>
        ///     Asynchronously maps a <see cref="Success" /> to <see cref="Success{TOut}" /> by applying an asynchronous function.
        /// </summary>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="success">The input <see cref="Success" />.</param>
        /// <param name="mapper">The asynchronous function to apply.</param>
        /// <returns>A new <see cref="Success{TOut}" /> with the mapped value.</returns>
        public static async Task<Success<TOut>> MapAsync<TOut>(
            this Success success,
            Func<Task<TOut>> mapper
        ) => success.WithValue(await mapper().ConfigureAwait(false));

        /// <summary>
        ///     Asynchronously maps a <see cref="Success{TIn}" /> to <see cref="Success{TOut}" /> by applying a function to the contained value.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="successTask">The input <see cref="Success{TIn}" />.</param>
        /// <param name="mapper">The function to apply to the value.</param>
        /// <returns>A new <see cref="Success{TOut}" /> with the mapped value.</returns>
        public static async Task<Success<TOut>> MapAsync<TIn, TOut>(
            this Task<Success<TIn>> successTask,
            Func<TIn, TOut> mapper
        ) => (await successTask.ConfigureAwait(false)).Map(mapper);

        /// <summary>
        ///     Asynchronously maps a <see cref="Task{Success}" /> to <see cref="Success{TOut}" /> by applying a function.
        /// </summary>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="successTask">The input <see cref="Task{Success}" />.</param>
        /// <param name="mapper">The function to apply.</param>
        /// <returns>A new <see cref="Success{TOut}" /> with the mapped value.</returns>
        public static async Task<Success<TOut>> MapAsync<TOut>(
            this Task<Success> successTask,
            Func<TOut> mapper
        ) => (await successTask.ConfigureAwait(false)).Map(mapper);

        /// <summary>
        ///     Asynchronously maps a <see cref="Success{TIn}" /> to <see cref="Success{TOut}" /> by applying an asynchronous function to the contained value.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="successTask">The input <see cref="Success{TIn}" />.</param>
        /// <param name="mapper">The asynchronous function to apply to the value.</param>
        /// <returns>A new <see cref="Success{TOut}" /> with the mapped value.</returns>
        public static async Task<Success<TOut>> MapAsync<TIn, TOut>(
            this Task<Success<TIn>> successTask,
            Func<TIn, Task<TOut>> mapper
        ) => await (await successTask.ConfigureAwait(false))
                .MapAsync(mapper)
                .ConfigureAwait(false);

        /// <summary>
        ///     Asynchronously maps a <see cref="Task{Success}" /> to <see cref="Success{TOut}" /> by applying an asynchronous function.
        /// </summary>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="successTask">The input <see cref="Task{Success}" />.</param>
        /// <param name="mapper">The asynchronous function to apply.</param>
        /// <returns>A new <see cref="Success{TOut}" /> with the mapped value.</returns>
        public static async Task<Success<TOut>> MapAsync<TOut>(
            this Task<Success> successTask,
            Func<Task<TOut>> mapper
        ) => await (await successTask.ConfigureAwait(false))
                .MapAsync(mapper)
                .ConfigureAwait(false);
    }
}
