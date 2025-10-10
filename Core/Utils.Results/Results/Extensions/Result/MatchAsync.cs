namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Provides extension methods for the <see cref="Result"/> and <see cref="Result{TValue}"/> classes,
    /// enabling the composition of functional and asynchronous operations.
    /// </summary>
    public static partial class ResultExtensions
    {
        #region Result
        /// <summary>
        /// Maps a non-generic result (without value) by applying asynchronous functions,
        /// returning a <see cref="Task{TResult}">Task&lt;Result&gt;</see>.
        /// </summary>
        /// <param name="result">The input result (without value).</param>
        /// <param name="success">The asynchronous function to apply on success, returning a <see cref="Task{TResult}">Task&lt;Result&gt;</see>.</param>
        /// <param name="failure">The asynchronous function to apply on failure, returning a <see cref="Task{TResult}">Task&lt;Result&gt;</see>.</param>
        /// <returns>A Task containing the new <see cref="Result"/>.</returns>
        public static async Task<Result> MatchAsync(
            this Result result,
            Func<Task<Result>> success,
            Func<Error, Task<Result>> failure
        )
        {
            return result.IsSuccess
                ? await success().ConfigureAwait(false)
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Maps a non-generic result (without value) using a synchronous success function,
        /// but with an asynchronous failure function. Returns a <see cref="Task{TResult}">Task&lt;Result&gt;</see>.
        /// </summary>
        /// <param name="result">The input result (without value).</param>
        /// <param name="success">The synchronous function to apply on success, returning a <see cref="Result"/>.</param>
        /// <param name="failure">The asynchronous function to apply on failure, returning a <see cref="Task{TResult}">Task&lt;Result&gt;</see>.</param>
        /// <returns>A Task containing the new <see cref="Result"/>.</returns>
        public static async Task<Result> MatchAsync(
            this Result result,
            Func<Result> success,
            Func<Error, Task<Result>> failure
        )
        {
            return result.IsSuccess ? success() : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Awaits a <see cref="Task{TResult}">Task&lt;Result&gt;</see> and maps the result using asynchronous functions,
        /// returning a <see cref="Task{TResult}">Task&lt;Result&gt;</see>.
        /// </summary>
        /// <param name="resultTask">The Task containing the input result.</param>
        /// <param name="success">The asynchronous function to apply on success, returning a <see cref="Task{TResult}">Task&lt;Result&gt;</see>.</param>
        /// <param name="failure">The asynchronous function to apply on failure, returning a <see cref="Task{TResult}">Task&lt;Result&gt;</see>.</param>
        /// <returns>A Task containing the new <see cref="Result"/>.</returns>
        public static async Task<Result> MatchAsync(
            this Task<Result> resultTask,
            Func<Task<Result>> success,
            Func<Error, Task<Result>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess
                ? await success().ConfigureAwait(false)
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Awaits a <see cref="Task{TResult}">Task&lt;Result&gt;</see> and maps the result using synchronous functions for success,
        /// with an asynchronous failure function. Returns a <see cref="Task{TResult}">Task&lt;Result&gt;</see>.
        /// </summary>
        /// <param name="resultTask">The Task containing the input result.</param>
        /// <param name="success">The synchronous function to apply on success, returning a <see cref="Result"/>.</param>
        /// <param name="failure">The asynchronous function to apply on failure, returning a <see cref="Task{TResult}">Task&lt;Result&gt;</see>.</param>
        /// <returns>A Task containing the new <see cref="Result"/>.</returns>
        public static async Task<Result> MatchAsync(
            this Task<Result> resultTask,
            Func<Result> success,
            Func<Error, Task<Result>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess ? success() : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Awaits a <see cref="Task{TResult}">Task&lt;Result&lt;TIn&gt;&gt;</see> and maps the result. The success function receives the original
        /// <see cref="Result{TIn}"/> (for re-sending or inspection), while failure receives the <see cref="Error"/>.
        /// </summary>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<Result<TIn>, Task<Result<TOut>>> success, // Receives Result<TIn> and returns Task<Result<TOut>>
            Func<Error, Task<Result<TOut>>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            // On success: the function should map TIn to TOut (or re-forward the failure if TIn=TOut).
            return result.IsSuccess
                ? await success(result).ConfigureAwait(false)
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Maps a synchronous <see cref="Result{TIn}"/> using a delegate that receives the original result.
        /// </summary>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<Result<TIn>, Task<Result<TOut>>> success, // Receives Result<TIn>
            Func<Error, Task<Result<TOut>>> failure
        )
        {
            return result.IsSuccess
                ? await success(result).ConfigureAwait(false)
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Awaits a <see cref="Task{TResult}">Task&lt;Result&gt;</see> and maps the result. The success function receives the original <see cref="Result"/>
        /// (for re-sending), and failure receives the <see cref="Error"/>, both returning <see cref="Task{TResult}">Task&lt;Result&gt;</see>.
        /// </summary>
        public static async Task<Result> MatchAsync(
            this Task<Result> resultTask,
            Func<Result, Task<Result>> success, // Receives Result and returns Task<Result>
            Func<Error, Task<Result>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess
                ? await success(result).ConfigureAwait(false) // Passes the original Result for re-sending
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Maps a synchronous <see cref="Result"/> using a delegate that receives the original result.
        /// </summary>
        public static async Task<Result> MatchAsync(
            this Result result,
            Func<Result, Task<Result>> success, // Receives Result
            Func<Error, Task<Result>> failure
        )
        {
            return result.IsSuccess
                ? await success(result).ConfigureAwait(false)
                : await failure(result.Error).ConfigureAwait(false);
        }
        #endregion

        #region Result<TValue>
        /// <summary>
        /// Maps the result by applying one of two asynchronous functions: one for success and one for failure.
        /// The success result is unwrapped and re-wrapped with existing success details.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="result">The input result.</param>
        /// <param name="success">The asynchronous function to apply to the success details (<see cref="Success{TIn}"/>).</param>
        /// <param name="failure">The asynchronous function to apply to the error.</param>
        /// <returns>A <see cref="Task{TResult}">Task&lt;Result&lt;TOut&gt;&gt;</see>.</returns>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<Success<TIn>, Task<TOut>> success,
            Func<Error, Task<Result<TOut>>> failure
        )
        {
            return result.IsSuccess
                ? Result.Success(
                    await success(result.SuccessDetails).ConfigureAwait(false),
                    result.SuccessDetails
                )
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Maps the result by applying one of two asynchronous functions: one for success and one for failure.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="result">The input result.</param>
        /// <param name="success">The asynchronous function to apply to the success details (<see cref="Success{TIn}"/>).</param>
        /// <param name="failure">The asynchronous function to apply to the error.</param>
        /// <returns>A <see cref="Task{TResult}">Task&lt;Result&lt;TOut&gt;&gt;</see></returns>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<Success<TIn>, Task<Result<TOut>>> success,
            Func<Error, Task<Result<TOut>>> failure
        )
        {
            return result.IsSuccess
                ? await success(result.SuccessDetails).ConfigureAwait(false)
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Awaits a <see cref="Task{TResult}">Task&lt;Result&lt;TIn&gt;&gt;</see> and maps the result
        /// using asynchronous functions for success and failure. The success result is unwrapped and re-wrapped.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="resultTask">The Task containing the input result.</param>
        /// <param name="success">The asynchronous function to apply to the success details (<see cref="Success{TIn}"/>), returning <see cref="Task{TResult}">Task&lt;TOut&gt;</see>.</param>
        /// <param name="failure">The asynchronous function to apply to the error, returning the failure Task&lt;<see cref="Result{TOut}"/>&gt;.</param>
        /// <returns>A <see cref="Task{TResult}">Task&lt;Result&lt;TOut&gt;&gt;</see> containing the new <see cref="Result{TOut}"/>.</returns>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<Success<TIn>, Task<TOut>> success,
            Func<Error, Task<Result<TOut>>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess
                ? Result.Success(
                    await success(result.SuccessDetails!).ConfigureAwait(false),
                    result.SuccessDetails
                )
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Awaits a <see cref="Task{TResult}">Task&lt;Result&lt;TIn&gt;&gt;</see> and maps the result
        /// using synchronous functions for success and failure. The success result is unwrapped and re-wrapped.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="resultTask">The Task containing the input result.</param>
        /// <param name="success">The synchronous function to apply to the success details (<see cref="Success{TIn}"/>), which returns <typeparamref name="TOut"/>.</param>
        /// <param name="failure">The synchronous function to apply to the error, which returns the failure <see cref="Result{TOut}"/>.</param>
        /// <returns>A <see cref="Task{TResult}">Task&lt;Result&lt;TOut&gt;&gt;</see> containing the new <see cref="Result{TOut}"/>.</returns>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<Success<TIn>, TOut> success,
            Func<Error, Result<TOut>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess
                ? Result.Success(success(result.SuccessDetails!), result.SuccessDetails)
                : failure(result.Error);
        }

        /// <summary>
        /// Awaits a <see cref="Task{TResult}">Task&lt;Result&lt;TIn&gt;&gt;</see> and maps the result
        /// using asynchronous functions for success and failure.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="resultTask">The Task containing the input result.</param>
        /// <param name="success">The asynchronous function to apply to the success details (<see cref="Success{TIn}"/>), returning <see cref="Task{TResult}">Task&lt;Result&lt;TOut&gt;&gt;</see>.</param>
        /// <param name="failure">The asynchronous function to apply to the error, returning the failure <see cref="Task{TResult}">Task&lt;Result&lt;TOut&gt;&gt;</see>.</param>
        /// <returns>A <see cref="Task{TResult}">Task&lt;Result&lt;TOut&gt;&gt;</see> containing the new <see cref="Result{TOut}"/>.</returns>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<Success<TIn>, Task<Result<TOut>>> success,
            Func<Error, Task<Result<TOut>>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess
                ? await success(result.SuccessDetails!).ConfigureAwait(false)
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Awaits a <see cref="Task{TResult}">Task&lt;Result&lt;TIn&gt;&gt;</see> and maps the result
        /// using synchronous functions for success, returning a <see cref="Result{TOut}"/>.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="resultTask">The Task containing the input result.</param>
        /// <param name="success">The synchronous function to apply to the success details (<see cref="Success{TIn}"/>), returning <see cref="Result{TOut}"/>.</param>
        /// <param name="failure">The synchronous function to apply to the error, returning the failure <see cref="Result{TOut}"/>.</param>
        /// <returns>A <see cref="Task{TResult}">Task&lt;Result&lt;TOut&gt;&gt;</see> containing the new <see cref="Result{TOut}"/>.</returns>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<Success<TIn>, Result<TOut>> success,
            Func<Error, Result<TOut>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess ? success(result.SuccessDetails!) : failure(result.Error);
        }

        /// <summary>
        /// Maps the synchronous result (<see cref="Result{TIn}"/>) using an asynchronous success function
        /// and a synchronous failure function. Preserves the SuccessDetails.
        /// </summary>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<Success<TIn>, Task<TOut>> success,
            Func<Error, Result<TOut>> failure
        )
        {
            return result.IsSuccess
                ? Result.Success(
                    await success(result.SuccessDetails!).ConfigureAwait(false),
                    result.SuccessDetails
                )
                : failure(result.Error);
        }

        /// <summary>
        /// Maps the synchronous result (<see cref="Result{TIn}"/>) using an asynchronous success function
        /// that returns a <see cref="Result{TOut}"/> and a synchronous failure function.
        /// </summary>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<Success<TIn>, Task<Result<TOut>>> success,
            Func<Error, Result<TOut>> failure
        )
        {
            return result.IsSuccess
                ? await success(result.SuccessDetails!).ConfigureAwait(false)
                : failure(result.Error);
        }

        /// <summary>
        /// Awaits the asynchronous input result and maps it using a synchronous success function (returns <see cref="Result{TOut}"/>)
        /// and an asynchronous failure function.
        /// </summary>
        public static async Task<Result<TOut>> MatchAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<Success<TIn>, Result<TOut>> success,
            Func<Error, Task<Result<TOut>>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess
                ? success(result.SuccessDetails!)
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Awaits a <see cref="Task{TResult}">Task&lt;Result&gt;</see> and maps the result using asynchronous functions.
        /// The success function receives the success details (<see cref="Success"/>).
        /// </summary>
        /// <typeparam name="TOut">The type of the output value (if successful).</typeparam>
        /// <param name="resultTask">The Task containing the input result.</param>
        /// <param name="success">The asynchronous function to apply to the success details, returning <see cref="Task{TResult}">Task&lt;TOut&gt;</see>.</param>
        /// <param name="failure">The asynchronous function to apply to the error.</param>
        /// <returns>A <see cref="Task{TResult}">Task&lt;Result&lt;TOut&gt;&gt;</see> containing the new <see cref="Result{TOut}"/>.</returns>
        public static async Task<Result<TOut>> MatchAsync<TOut>(
            this Task<Result> resultTask,
            Func<Success, Task<TOut>> success,
            Func<Error, Task<Result<TOut>>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess
                ? Result.Success(
                    await success(result.SuccessDetails).ConfigureAwait(false),
                    result.SuccessDetails
                )
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Awaits a <see cref="Task{TResult}">Task&lt;Result&gt;</see> and maps the result using synchronous functions for success.
        /// The success function receives the success details (<see cref="Success"/>).
        /// </summary>
        /// <typeparam name="TOut">The type of the output value (if successful).</typeparam>
        /// <param name="resultTask">The Task containing the input result.</param>
        /// <param name="success">The synchronous function to apply to the success details, which returns <typeparamref name="TOut"/>.</param>
        /// <param name="failure">The asynchronous function to apply to the error.</param>
        /// <returns>A <see cref="Task{TResult}">Task&lt;Result&lt;TOut&gt;&gt;</see> containing the new <see cref="Result{TOut}"/>.</returns>
        public static async Task<Result<TOut>> MatchAsync<TOut>(
            this Task<Result> resultTask,
            Func<Success, TOut> success,
            Func<Error, Task<Result<TOut>>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            // Uses the implicit conversion of TOut to Result<TOut>
            return result.IsSuccess
                ? Result.Success(success(result.SuccessDetails), result.SuccessDetails)
                : await failure(result.Error).ConfigureAwait(false);
        }

        /// <summary>
        /// Awaits a <see cref="Task{TResult}">Task&lt;Result&gt;</see> and maps the result using asynchronous functions, where success returns a <see cref="Result{TOut}"/>.
        /// The success function receives the success details (<see cref="Success"/>).
        /// </summary>
        /// <typeparam name="TOut">The type of the output value (if successful).</typeparam>
        /// <param name="resultTask">The Task containing the input result.</param>
        /// <param name="success">The asynchronous function to apply to the success details, which returns a <see cref="Task{TResult}">Task&lt;Result&lt;TOut&gt;&gt;</see>.</param>
        /// <param name="failure">The asynchronous function to apply to the error.</param>
        /// <returns>A <see cref="Task{TResult}">Task&lt;Result&lt;TOut&gt;&gt;</see> containing the new <see cref="Result{TOut}"/>.</returns>
        public static async Task<Result<TOut>> MatchAsync<TOut>(
            this Task<Result> resultTask,
            Func<Success, Task<Result<TOut>>> success,
            Func<Error, Task<Result<TOut>>> failure
        )
        {
            var result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess
                ? await success(result.SuccessDetails).ConfigureAwait(false)
                : await failure(result.Error).ConfigureAwait(false);
        }
        #endregion
    }
}
