namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Provides asynchronous extension methods for the <see cref="Result"/> and <see cref="Result{TValue}"/> classes,
    /// focused on executing side effects (Tap) within an asynchronous flow.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        ///     Asynchronously executes the given action if the <see cref="Result{T}" /> is a success.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="result">The input <see cref="Result{T}" />.</param>
        /// <param name="action">The asynchronous action to execute.</param>
        /// <returns>The input <see cref="Result{T}" />.</returns>
        public static async Task<Result<T>> TapAsync<T>(this Result<T> result, Func<T, Task> action)
        {
            if (result.IsSuccess)
            {
                await action(result.Value!).ConfigureAwait(false);
            }

            return result;
        }

        /// <summary>
        ///     Asynchronously executes the given action if the <see cref="Result" /> is a success.
        /// </summary>
        /// <param name="result">The input <see cref="Result" />.</param>
        /// <param name="action">The asynchronous action to execute.</param>
        /// <returns>The input <see cref="Result" />.</returns>
        public static async Task<Result> TapAsync(this Result result, Func<Task> action)
        {
            if (result.IsSuccess)
            {
                await action().ConfigureAwait(false);
            }
            return result;
        }

        /// <summary>
        ///     Asynchronously executes the given action if the <see cref="Result{T}" /> is a success.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="result">The input <see cref="Result{T}" />.</param>
        /// <param name="action">The asynchronous action to execute.</param>
        /// <returns>The input <see cref="Result{T}" />.</returns>
        public static async Task<Result<T>> TapAsync<T>(this Result<T> result, Func<Task> action)
        {
            if (result.IsSuccess)
            {
                await action().ConfigureAwait(false);
            }
            return result;
        }

        /// <summary>
        ///     Asynchronously executes the given action if the <see cref="Result{T}" /> is a success.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="resultTask">The input <see cref="Result{T}" />.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns>The input <see cref="Result{T}" />.</returns>
        public static async Task<Result<T>> TapAsync<T>(
            this Task<Result<T>> resultTask,
            Action<T> action
        ) => (await resultTask.ConfigureAwait(false)).Tap(action);

        /// <summary>
        ///     Asynchronously executes the given action if the <see cref="Task{Result}" /> is a success.
        /// </summary>
        /// <param name="resultTask">The input <see cref="Task{Result}" />.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns>The input <see cref="Task{Result}" />.</returns>
        public static async Task<Result> TapAsync(this Task<Result> resultTask, Action action) =>
            (await resultTask.ConfigureAwait(false)).Tap(action);

        /// <summary>
        ///     Asynchronously executes the given action if the <see cref="Result{T}" /> is a success.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="resultTask">The input <see cref="Result{T}" />.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns>The input <see cref="Result{T}" />.</returns>
        public static async Task<Result<T>> TapAsync<T>(
            this Task<Result<T>> resultTask,
            Action action
        ) => (await resultTask.ConfigureAwait(false)).Tap(action);

        /// <summary>
        ///     Asynchronously executes the given action if the <see cref="Result{T}" /> is a success.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="resultTask">The input <see cref="Result{T}"/>.</param>
        /// <param name="action">The asynchronous action to execute.</param>
        /// <returns>The input <see cref="Result{T}" />.</returns>
        public static async Task<Result<T>> TapAsync<T>(
            this Task<Result<T>> resultTask,
            Func<T, Task> action
        ) => await (await resultTask.ConfigureAwait(false)).TapAsync(action).ConfigureAwait(false);

        /// <summary>
        ///     Asynchronously executes the given action if the <see cref="Task{Result}" /> is a success.
        /// </summary>
        /// <param name="resultTask">The input <see cref="Task{Result}" />.</param>
        /// <param name="action">The asynchronous action to execute.</param>
        /// <returns>The input <see cref="Task{Result}" />.</returns>
        public static async Task<Result> TapAsync(
            this Task<Result> resultTask,
            Func<Task> action
        ) => await (await resultTask.ConfigureAwait(false)).TapAsync(action).ConfigureAwait(false);

        /// <summary>
        ///     Asynchronously executes the given action if the <see cref="Result{T}" /> is a success.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="resultTask">The input <see cref="Result{T}" />.</param>
        /// <param name="action">The asynchronous action to execute.</param>
        /// <returns>The input <see cref="Result{T}" />.</returns>
        public static async Task<Result<T>> TapAsync<T>(
            this Task<Result<T>> resultTask,
            Func<Task> action
        ) => await (await resultTask.ConfigureAwait(false)).TapAsync(action).ConfigureAwait(false);
    }
}
