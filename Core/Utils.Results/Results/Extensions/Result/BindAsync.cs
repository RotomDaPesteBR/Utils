namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Provides asynchronous extension methods for the <see cref="Result{TValue}"/> class,
    /// focused on chaining (Bind) operations that might fail.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        ///     Asynchronously binds a <see cref="Result{TIn}" /> to a <see cref="Result{TOut}" /> by applying an asynchronous function to the contained value.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="result">The input <see cref="Result{TIn}" />.</param>
        /// <param name="binder">The asynchronous function to apply to the value.</param>
        /// <returns>The <see cref="Result{TOut}" /> from the binder function.</returns>
        public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Task<Result<TOut>>> binder
        ) =>
            result.IsSuccess
                ? await binder(result.Value!).ConfigureAwait(false)
                : Result<TOut>.Failure(result.Error);

        /// <summary>
        ///     Asynchronously binds a <see cref="Result" /> to a <see cref="Result{T}" /> by applying an asynchronous function.
        /// </summary>
        /// <typeparam name="T">The type of the output value.</typeparam>
        /// <param name="result">The input <see cref="Result" />.</param>
        /// <param name="binder">The asynchronous function to apply.</param>
        /// <returns>The <see cref="Result{T}" /> from the binder function.</returns>
        public static async Task<Result<T>> BindAsync<T>(
            this Result result,
            Func<Task<Result<T>>> binder
        ) =>
            result.IsSuccess
                ? await binder().ConfigureAwait(false)
                : Result<T>.Failure(result.Error);

        /// <summary>
        ///     Asynchronously binds a <see cref="Result" /> to a <see cref="Result" /> by applying an asynchronous function.
        /// </summary>
        /// <param name="result">The input <see cref="Result" />.</param>
        /// <param name="binder">The asynchronous function to apply.</param>
        /// <returns>The <see cref="Result" /> from the binder function.</returns>
        public static async Task<Result> BindAsync(this Result result, Func<Task<Result>> binder) =>
            result.IsSuccess ? await binder().ConfigureAwait(false) : result;

        /// <summary>
        ///     Asynchronously binds a <see cref="Result{TIn}" /> to a <see cref="Result{TOut}" /> by applying a function to the contained value.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="resultTask">The input <see cref="Result{TIn}" />.</param>
        /// <param name="binder">The function to apply to the value.</param>
        /// <returns>The <see cref="Result{TOut}" /> from the binder function.</returns>
        public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, Result<TOut>> binder
        ) => (await resultTask.ConfigureAwait(false)).Bind(binder);

        /// <summary>
        ///     Asynchronously binds a <see cref="Task{Result}" /> to a <see cref="Result{T}" /> by applying a function.
        /// </summary>
        /// <typeparam name="T">The type of the output value.</typeparam>
        /// <param name="resultTask">The input <see cref="Task{Result}" />.</param>
        /// <param name="binder">The function to apply.</param>
        /// <returns>The <see cref="Result{T}" /> from the binder function.</returns>
        public static async Task<Result<T>> BindAsync<T>(
            this Task<Result> resultTask,
            Func<Result<T>> binder
        ) => (await resultTask.ConfigureAwait(false)).Bind(binder);

        /// <summary>
        ///     Asynchronously binds a <see cref="Task{Result}" /> to a <see cref="Result" /> by applying a function.
        /// </summary>
        /// <param name="resultTask">The input <see cref="Task{Result}" />.</param>
        /// <param name="binder">The function to apply.</param>
        /// <returns>The <see cref="Result" /> from the binder function.</returns>
        public static async Task<Result> BindAsync(
            this Task<Result> resultTask,
            Func<Result> binder
        ) => (await resultTask.ConfigureAwait(false)).Bind(binder);

        /// <summary>
        ///     Asynchronously binds a <see cref="Result{TIn}" /> to a <see cref="Result{TOut}" /> by applying an asynchronous function to the contained value.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="resultTask">The input <see cref="Result{TIn}" />.</param>
        /// <param name="binder">The asynchronous function to apply to the value.</param>
        /// <returns>The <see cref="Result{TOut}" /> from the binder function.</returns>
        public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, Task<Result<TOut>>> binder
        ) =>
            await (await resultTask.ConfigureAwait(false))
                .BindAsync(binder)
                .ConfigureAwait(false);

        /// <summary>
        ///     Asynchronously binds a <see cref="Task{Result}" /> to a <see cref="Result{T}" /> by applying an asynchronous function.
        /// </summary>
        /// <typeparam name="T">The type of the output value.</typeparam>
        /// <param name="resultTask">The input <see cref="Task{Result}" />.</param>
        /// <param name="binder">The asynchronous function to apply.</param>
        /// <returns>The <see cref="Result{T}" /> from the binder function.</returns>
        public static async Task<Result<T>> BindAsync<T>(
            this Task<Result> resultTask,
            Func<Task<Result<T>>> binder
        ) =>
            await (await resultTask.ConfigureAwait(false))
                .BindAsync(binder)
                .ConfigureAwait(false);

        /// <summary>
        ///     Asynchronously binds a <see cref="Task{Result}" /> to a <see cref="Result" /> by applying an asynchronous function.
        /// </summary>
        /// <param name="resultTask">The input <see cref="Task{Result}" />.</param>
        /// <param name="binder">The asynchronous function to apply.</param>
        /// <returns>The <see cref="Result" /> from the binder function.</returns>
        public static async Task<Result> BindAsync(
            this Task<Result> resultTask,
            Func<Task<Result>> binder
        ) => await (await resultTask.ConfigureAwait(false)).BindAsync(binder).ConfigureAwait(false);
    }
}
