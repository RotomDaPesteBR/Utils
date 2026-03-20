namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Provides asynchronous extension methods for the <see cref="Result{TValue}"/> class,
    /// focused on condition validation (Ensure) within an asynchronous flow.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        ///     Asynchronously ensures that the given predicate is true, otherwise returns a new failure <see cref="Result" />.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="result">The input <see cref="Result{T}" />.</param>
        /// <param name="predicate">The asynchronous predicate to check.</param>
        /// <param name="error">The error to return if the predicate is false.</param>
        /// <returns>The input <see cref="Result{T}" /> if the predicate is true, otherwise a new failure <see cref="Result{T}" />.</returns>
        public static async Task<Result<T>> EnsureAsync<T>(
            this Result<T> result,
            Func<T, Task<bool>> predicate,
            Error error
        ) =>
            result.IsFailure
                ? result
                : (
                    await predicate(result.Value!).ConfigureAwait(false)
                        ? result
                        : Result<T>.Failure(error)
                );

        /// <summary>
        ///     Asynchronously ensures that the given condition is true, otherwise returns a new failure <see cref="Result" />.
        /// </summary>
        /// <param name="result">The input <see cref="Result" />.</param>
        /// <param name="condition">The asynchronous condition to check.</param>
        /// <param name="error">The error to return if the condition is false.</param>
        /// <returns>The input <see cref="Result" /> if the condition is true, otherwise a new failure <see cref="Result" />.</returns>
        public static async Task<Result> EnsureAsync(
            this Result result,
            Func<Task<bool>> condition,
            Error error
        ) =>
            result.IsFailure
                ? result
                : (await condition().ConfigureAwait(false) ? result : Result.Failure(error));

        /// <summary>
        ///     Asynchronously ensures that the given predicate is true, otherwise returns a new failure <see cref="Result" />.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="resultTask">The input <see cref="Result{T}" />.</param>
        /// <param name="predicate">The predicate to check.</param>
        /// <param name="error">The error to return if the predicate is false.</param>
        /// <returns>The input <see cref="Result{T}" /> if the predicate is true, otherwise a new failure <see cref="Result{T}" />.</returns>
        public static async Task<Result<T>> EnsureAsync<T>(
            this Task<Result<T>> resultTask,
            Func<T, bool> predicate,
            Error error
        ) => (await resultTask.ConfigureAwait(false)).Ensure(predicate, error);

        /// <summary>
        ///     Asynchronously ensures that the given condition is true, otherwise returns a new failure <see cref="Result" />.
        /// </summary>
        /// <param name="resultTask">The input <see cref="Task{Result}" />.</param>
        /// <param name="condition">The condition to check.</param>
        /// <param name="error">The error to return if the condition is false.</param>
        /// <returns>The input <see cref="Result" /> if the condition is true, otherwise a new failure <see cref="Result" />.</returns>
        public static async Task<Result> EnsureAsync(
            this Task<Result> resultTask,
            bool condition,
            Error error
        ) => (await resultTask.ConfigureAwait(false)).Ensure(condition, error);

        /// <summary>
        ///     Asynchronously ensures that the given predicate is true, otherwise returns a new failure <see cref="Result" />.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="resultTask">The input <see cref="Result{T}" />.</param>
        /// <param name="predicate">The asynchronous predicate to check.</param>
        /// <param name="error">The error to return if the predicate is false.</param>
        /// <returns>The input <see cref="Result{T}" /> if the predicate is true, otherwise a new failure <see cref="Result{T}" />.</returns>
        public static async Task<Result<T>> EnsureAsync<T>(
            this Task<Result<T>> resultTask,
            Func<T, Task<bool>> predicate,
            Error error
        ) =>
            await (await resultTask.ConfigureAwait(false))
                .EnsureAsync(predicate, error)
                .ConfigureAwait(false);

        /// <summary>
        ///     Asynchronously ensures that the given condition is true, otherwise returns a new failure <see cref="Result" />.
        /// </summary>
        /// <param name="resultTask">The input <see cref="Task{Result}" />.</param>
        /// <param name="condition">The asynchronous condition to check.</param>
        /// <param name="error">The error to return if the condition is false.</param>
        /// <returns>The input <see cref="Result" /> if the condition is true, otherwise a new failure <see cref="Result" />.</returns>
        public static async Task<Result> EnsureAsync(
            this Task<Result> resultTask,
            Func<Task<bool>> condition,
            Error error
        ) =>
            await (await resultTask.ConfigureAwait(false))
                .EnsureAsync(condition, error)
                .ConfigureAwait(false);
    }
}
