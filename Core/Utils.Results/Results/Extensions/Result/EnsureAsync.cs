namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Provides asynchronous extension methods for the <see cref="Result{TValue}"/> class,
    /// focused on condition validation (Ensure) within an asynchronous flow.
    /// </summary>
    public static partial class ResultExtensions
    {
        // 1. EnsureAsync: Result<TValue> -> Task<Result<TValue>> (Predicate é Task<bool>)
        /// <summary>
        /// Checks an **asynchronous** condition on a **synchronous** result. If the condition is false, 
        /// the successful result is transformed into a failed result using the provided error. 
        /// Propagates existing failures immediately.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="result">The synchronous input result.</param>
        /// <param name="predicate">The asynchronous condition (function) to be checked against the success value.</param>
        /// <param name="error">The error to be returned if the condition is false.</param>
        /// <returns>A <see cref="Task{TResult}">Task&lt;Result&lt;TValue&gt;&gt;</see> containing the original result (if successful and true), or a failed result.</returns>
        public static async Task<Result<TValue>> EnsureAsync<TValue>(
            this Result<TValue> result,
            Func<TValue, Task<bool>> predicate,
            Error error
        )
        {
            if (result.IsFailure)
            {
                return result;
            }

            // result.Value is non-null here.
            return (await predicate(result.Value!).ConfigureAwait(false)) ? result : error;
        }

        // 2. EnsureAsync: Task<Result<TValue>> -> Task<Result<TValue>> (Predicate é Task<bool>)
        /// <summary>
        /// Awaits an asynchronous result and then checks an **asynchronous** condition on the success value.
        /// Propagates existing failures immediately after waiting for the task.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="taskResult">The asynchronous input result.</param>
        /// <param name="predicate">The asynchronous condition (function) to be checked against the success value.</param>
        /// <param name="error">The error to be returned if the condition is false.</param>
        /// <returns>A <see cref="Task{TResult}">Task&lt;Result&lt;TValue&gt;&gt;</see> containing the original result (if successful and true), or a failed result.</returns>
        public static async Task<Result<TValue>> EnsureAsync<TValue>(
            this Task<Result<TValue>> taskResult,
            Func<TValue, Task<bool>> predicate,
            Error error
        )
        {
            var result = await taskResult.ConfigureAwait(false);
            if (result.IsFailure)
            {
                return result;
            }

            // result.Value is non-null here.
            return (await predicate(result.Value!).ConfigureAwait(false)) ? result : error;
        }

        // 3. EnsureAsync: Task<Result<TValue>> -> Task<Result<TValue>> (Predicate é bool síncrono)
        /// <summary>
        /// Awaits an asynchronous result and then checks a **synchronous** condition on the success value.
        /// Propagates existing failures immediately after waiting for the task.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="taskResult">The asynchronous input result.</param>
        /// <param name="predicate">The synchronous condition (function) to be checked against the success value.</param>
        /// <param name="error">The error to be returned if the condition is false.</param>
        /// <returns>A <see cref="Task{TResult}">Task&lt;Result&lt;TValue&gt;&gt;</see> containing the original result (if successful and true), or a failed result.</returns>
        public static async Task<Result<TValue>> EnsureAsync<TValue>(
            this Task<Result<TValue>> taskResult,
            Func<TValue, bool> predicate,
            Error error
        )
        {
            var result = await taskResult.ConfigureAwait(false);
            if (result.IsFailure)
            {
                return result;
            }

            // result.Value is non-null here.
            return predicate(result.Value!) ? result : error;
        }
    }
}