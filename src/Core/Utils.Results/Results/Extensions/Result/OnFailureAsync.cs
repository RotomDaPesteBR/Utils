namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Provides asynchronous extension methods for the <see cref="Result"/> and <see cref="Result{TValue}"/> classes,
    /// focused on handling failures within an asynchronous flow.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        ///     Asynchronously executes the given action if the <see cref="Result" /> is a failure.
        /// </summary>
        /// <param name="result">The input <see cref="Result" />.</param>
        /// <param name="action">The asynchronous action to execute.</param>
        /// <returns>The input <see cref="Result" />.</returns>
        public static async Task<Result> OnFailureAsync(this Result result, Func<Task> action)
        {
            if (result.IsFailure)
            {
                await action().ConfigureAwait(false);
            }
            return result;
        }

        /// <summary>
        ///     Asynchronously executes the given action if the <see cref="Result" /> is a failure.
        /// </summary>
        /// <param name="result">The input <see cref="Result" />.</param>
        /// <param name="action">The asynchronous action to execute.</param>
        /// <returns>The input <see cref="Result" />.</returns>
        public static async Task<Result> OnFailureAsync(
            this Result result,
            Func<Error, Task> action
        )
        {
            if (result.IsFailure)
            {
                await action(result.Error).ConfigureAwait(false);
            }
            return result;
        }

        /// <summary>
        ///     Asynchronously executes the given action if the <see cref="Result{T}" /> is a failure.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="result">The input <see cref="Result{T}" />.</param>
        /// <param name="action">The asynchronous action to execute.</param>
        /// <returns>The input <see cref="Result{T}" />.</returns>
        public static async Task<Result<T>> OnFailureAsync<T>(
            this Result<T> result,
            Func<Task> action
        )
        {
            if (result.IsFailure)
            {
                await action().ConfigureAwait(false);
            }
            return result;
        }

        /// <summary>
        ///     Asynchronously executes the given action if the <see cref="Result{T}" /> is a failure.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="result">The input <see cref="Result{T}" />.</param>
        /// <param name="action">The asynchronous action to execute.</param>
        /// <returns>The input <see cref="Result{T}" />.</returns>
        public static async Task<Result<T>> OnFailureAsync<T>(
            this Result<T> result,
            Func<Error, Task> action
        )
        {
            if (result.IsFailure)
            {
                await action(result.Error).ConfigureAwait(false);
            }
            return result;
        }

        /// <summary>
        ///     Asynchronously executes the given action if the <see cref="Task{Result}" /> is a failure.
        /// </summary>
        /// <param name="resultTask">The input <see cref="Task{Result}" />.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns>The input <see cref="Task{Result}" />.</returns>
        public static async Task<Result> OnFailureAsync(
            this Task<Result> resultTask,
            Action action
        ) => (await resultTask.ConfigureAwait(false)).OnFailure(action);

        /// <summary>
        ///     Asynchronously executes the given action if the <see cref="Task{Result}" /> is a failure.
        /// </summary>
        /// <param name="resultTask">The input <see cref="Task{Result}" />.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns>The input <see cref="Task{Result}" />.</returns>
        public static async Task<Result> OnFailureAsync(
            this Task<Result> resultTask,
            Action<Error> action
        ) => (await resultTask.ConfigureAwait(false)).OnFailure(action);

        /// <summary>
        ///     Asynchronously executes the given action if the <see cref="Result{T}" /> is a failure.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="resultTask">The input <see cref="Result{T}" />.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns>The input <see cref="Result{T}" />.</returns>
        public static async Task<Result<T>> OnFailureAsync<T>(
            this Task<Result<T>> resultTask,
            Action action
        ) => (await resultTask.ConfigureAwait(false)).OnFailure(action);

        /// <summary>
        ///     Asynchronously executes the given action if the <see cref="Result{T}" /> is a failure.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="resultTask">The input <see cref="Result{T}" />.</param>
        /// <param name="action">The action to execute.</param>
        /// <returns>The input <see cref="Result{T}" />.</returns>
        public static async Task<Result<T>> OnFailureAsync<T>(
            this Task<Result<T>> resultTask,
            Action<Error> action
        ) => (await resultTask.ConfigureAwait(false)).OnFailure(action);

        /// <summary>
        ///     Asynchronously executes the given action if the <see cref="Task{Result}" /> is a failure.
        /// </summary>
        /// <param name="resultTask">The input <see cref="Task{Result}" />.</param>
        /// <param name="action">The asynchronous action to execute.</param>
        /// <returns>The input <see cref="Task{Result}" />.</returns>
        public static async Task<Result> OnFailureAsync(
            this Task<Result> resultTask,
            Func<Task> action
        ) =>
            await (await resultTask.ConfigureAwait(false))
                .OnFailureAsync(action)
                .ConfigureAwait(false);

        /// <summary>
        ///     Asynchronously executes the given action if the <see cref="Task{Result}" /> is a failure.
        /// </summary>
        /// <param name="resultTask">The input <see cref="Task{Result}" />.</param>
        /// <param name="action">The asynchronous action to execute.</param>
        /// <returns>The input <see cref="Task{Result}" />.</returns>
        public static async Task<Result> OnFailureAsync(
            this Task<Result> resultTask,
            Func<Error, Task> action
        ) =>
            await (await resultTask.ConfigureAwait(false))
                .OnFailureAsync(action)
                .ConfigureAwait(false);

        /// <summary>
        ///     Asynchronously executes the given action if the <see cref="Result{T}" /> is a failure.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="resultTask">The input <see cref="Result{T}" />.</param>
        /// <param name="action">The asynchronous action to execute.</param>
        /// <returns>The input <see cref="Result{T}" />.</returns>
        public static async Task<Result<T>> OnFailureAsync<T>(
            this Task<Result<T>> resultTask,
            Func<Task> action
        ) =>
            await (await resultTask.ConfigureAwait(false))
                .OnFailureAsync(action)
                .ConfigureAwait(false);

        /// <summary>
        ///     Asynchronously executes the given action if the <see cref="Result{T}" /> is a failure.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="resultTask">The input <see cref="Result{T}" />.</param>
        /// <param name="action">The asynchronous action to execute.</param>
        /// <returns>The input <see cref="Result{T}" />.</returns>
        public static async Task<Result<T>> OnFailureAsync<T>(
            this Task<Result<T>> resultTask,
            Func<Error, Task> action
        ) =>
            await (await resultTask.ConfigureAwait(false))
                .OnFailureAsync(action)
                .ConfigureAwait(false);
    }
}
