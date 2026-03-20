using System.Runtime.CompilerServices;

namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Represents the asynchronous result of an operation that can be a success (with a specific value) or a failure.
    /// It encapsulates a Task containing a <see cref="Result{TValue}"/> and is directly awaitable.
    /// </summary>
    /// <typeparam name="TValue">The type of the success value that this result encapsulates.</typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="TaskResult{TValue}"/> class.
    /// </remarks>
    /// <param name="task">The <see cref="Task{TResult}"/> containing the <see cref="Result{TValue}"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown if the task is null.</exception>
    public class TaskResult<TValue>(Task<Result<TValue>> task)
    {
        private readonly Task<Result<TValue>> _task = task ?? throw new ArgumentNullException(nameof(task));

        // ******** START: AWAITABLE PATTERN IMPLEMENTATION ********

        /// <summary>
        /// Gets the awaiter for this <see cref="TaskResult{TValue}"/>, allowing it to be directly awaited.
        /// </summary>
        /// <returns>A <see cref="TaskResultAwaiter"/> for this instance.</returns>
        public TaskResultAwaiter GetAwaiter() => new(_task);

        /// <summary>
        /// The Awaiter type for <see cref="TaskResult{TValue}"/>.
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of the <see cref="TaskResultAwaiter"/> struct.
        /// </remarks>
        /// <param name="task">The encapsulated <see cref="Task{TResult}"/>.</param>
        public readonly struct TaskResultAwaiter(Task<Result<TValue>> task) : INotifyCompletion, ICriticalNotifyCompletion
        {
            private readonly Task<Result<TValue>> _task = task;

            /// <summary>
            /// Gets a value indicating whether the associated task is completed.
            /// </summary>
            public bool IsCompleted => _task.IsCompleted;

            /// <summary>
            /// Gets the result of the task after completion.
            /// </summary>
            /// <returns>The <see cref="Result{TValue}"/> encapsulated by the task.</returns>
            public Result<TValue> GetResult() =>
                // Isso lançará a exceção original da Task se ela falhar,
                // ou retornará o Result<TValue> se for bem-sucedida.
                _task.Result;

            /// <summary>
            /// Schedules the continuation action when the task is completed.
            /// </summary>
            /// <param name="continuation">The action to be executed.</param>
            public void OnCompleted(Action continuation) =>
                _task.GetAwaiter().OnCompleted(continuation);

            /// <summary>
            /// Schedules the continuation action when the task is critically completed.
            /// </summary>
            /// <param name="continuation">The action to be executed.</param>
            public void UnsafeOnCompleted(Action continuation) =>
                _task.GetAwaiter().UnsafeOnCompleted(continuation);
        }

        // ******** END: AWAITABLE PATTERN IMPLEMENTATION ********

        /// <summary>
        /// Allows implicit conversion from a <see cref="Task{TResult}"/> of <see cref="Result{TValue}"/> to a <see cref="TaskResult{TValue}"/>.
        /// </summary>
        /// <param name="task">The <see cref="Task{TResult}"/> of <see cref="Result{TValue}"/> to be converted.</param>
        /// <returns>A <see cref="TaskResult{TValue}"/> encapsulating the task.</returns>
        public static implicit operator TaskResult<TValue>(Task<Result<TValue>> task) =>
            new(task);
    }

    /// <summary>
    /// Provides static factory methods to create <see cref="TaskResult{TValue}"/>.
    /// </summary>
    public static class TaskResult
    {
        /// <summary>
        /// Creates a successful <see cref="TaskResult{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of the success value.</typeparam>
        /// <param name="value">The value to be encapsulated in the result.</param>
        /// <returns>A new instance of <see cref="TaskResult{TValue}"/> indicating success.</returns>
        public static TaskResult<TValue> Success<TValue>(TValue value) =>
            new(Task.FromResult(Result.Success(value)));

        /// <summary>
        /// Creates a failed <see cref="TaskResult{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of the success value.</typeparam>
        /// <param name="error">The <see cref="Error"/> object describing the failure.</param>
        /// <returns>A new instance of <see cref="TaskResult{TValue}"/> indicating failure.</returns>
        public static TaskResult<TValue> Failure<TValue>(Error error) =>
            new(Task.FromResult(Result<TValue>.Failure(error)));

        /// <summary>
        /// Converts a <see cref="Task{TResult}"/> of <see cref="Result"/> into a TaskResult&lt;object&gt; to allow asynchronous chaining.
        /// This method is for cases where the <see cref="Result"/> does not have a generic value (base <see cref="Result"/>).
        /// </summary>
        /// <param name="task">The <see cref="Task{TResult}"/> to be converted.</param>
        /// <returns>A TaskResult&lt;object&gt; encapsulating the task.</returns>
        public static TaskResult<object> FromTask(Task<Result> task) => new(
                task.ContinueWith(t =>
                {
                    if (t.IsFaulted && t.Exception != null)
                    {
                        // Propaga a exceção original da Task
                        throw t.Exception.InnerException ?? t.Exception;
                    }
                    else if (t.IsCanceled)
                    {
                        // Lida com o cancelamento da Task
                        throw new TaskCanceledException(t);
                    }
                    else if (t.Result.IsSuccess)
                    {
                        // Retorna um Result<object> de sucesso
                        return Result.Success(new object());
                    }
                    else
                    {
                        // Retorna um Result<object> de falha com o erro original
                        return Result<object>.Failure(t.Result.Error);
                    }
                })
            );

        /// <summary>
        /// Creates a successful TaskResult&lt;object&gt; for operations that do not return a specific value.
        /// </summary>
        /// <returns>A new instance of TaskResult&lt;object&gt; indicating success.</returns>
        public static TaskResult<object> Success() =>
            new(Task.FromResult(Result.Success(new object())));

        /// <summary>
        /// Creates a failed TaskResult&lt;object&gt; for operations that do not return a specific value.
        /// </summary>
        /// <param name="error">The <see cref="Error"/> object describing the failure.</param>
        /// <returns>A new instance of TaskResult&lt;object&gt; indicating failure.</returns>
        public static TaskResult<object> Failure(Error error) =>
            new(Task.FromResult(Result<object>.Failure(error)));
    }
}
