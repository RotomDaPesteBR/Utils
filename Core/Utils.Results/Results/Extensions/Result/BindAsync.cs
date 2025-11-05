namespace LightningArc.Utils.Results;

/// <summary>
/// Provides asynchronous extension methods for the <see cref="Result{TValue}"/> class,
/// focused on the chaining (Bind) of asynchronous operations.
/// </summary>
public static partial class ResultExtensions
{
    #region Result

    #region Result => Task<Result<TValue>>

    #region () => Result<TOut>

    /// <summary>
    /// Asynchronously binds a non-generic <see cref="Result"/> to a new <see cref="Result{TOut}"/> by applying an asynchronous function.
    /// This method is used to chain asynchronous operations where the next step (defined by the <paramref name="mapper"/>)
    /// also returns a <see cref="Result{TOut}"/>, allowing for propagation of success or failure.
    /// </summary>
    /// <typeparam name="TOut">The type of the value contained in the output <see cref="Result{TOut}"/>.</typeparam>
    /// <param name="result">The input non-generic <see cref="Result"/>.</param>
    /// <param name="mapper">
    /// The asynchronous function to apply if the input <paramref name="result"/> is successful.
    /// This function takes no arguments and returns a <see cref="Result{TOut}"/>.
    /// </param>
    /// <returns>
    /// A <see cref="Result{TOut}"/> representing the asynchronous operation:
    /// If the input <paramref name="result"/> is successful, the result of applying the <paramref name="mapper"/> function.
    /// If the input <paramref name="result"/> is a failure, a <see cref="Result{TOut}"/> containing the original error is returned.
    /// </returns>
    public static async Task<Result<TOut>> BindAsync<TOut>(
        this Result result,
        Func<Task<Result<TOut>>> mapper
    ) => result.IsSuccess ? await mapper().ConfigureAwait(false) : result.Error;

    #endregion

    #endregion

    #region Task<Result> => Result<TValue>

    #region () => Result<TOut>
    /// <summary>
    /// Asynchronously binds a <see cref="Task{Result}"/> to a new <see cref="Result{TOut}"/> by applying a synchronous function.
    /// This method awaits the input <see cref="Task{Result}"/> and then applies a synchronous binding function
    /// if the awaited result is successful, allowing for propagation of success or failure.
    /// </summary>
    /// <typeparam name="TOut">The type of the value contained in the output <see cref="Result{TOut}"/>.</typeparam>
    /// <param name="resultTask">The input <see cref="Task{Result}"/> representing an asynchronous operation that yields a non-generic result.</param>
    /// <param name="mapper">
    /// The synchronous function to apply if the awaited <paramref name="resultTask"/> is successful.
    /// This function takes no arguments and returns a <see cref="Result{TOut}"/>.
    /// </param>
    /// <returns>
    /// A <see cref="Result{TOut}"/> representing the asynchronous operation:
    /// If the awaited <paramref name="resultTask"/> is successful, the result of applying the <paramref name="mapper"/> function.
    /// If the awaited <paramref name="resultTask"/> is a failure, the original error is propagated.
    /// </returns>
    public static async Task<Result<TOut>> BindAsync<TOut>(
        this Task<Result> resultTask,
        Func<Result<TOut>> mapper
    ) => (await resultTask.ConfigureAwait(false)).Bind(mapper);
    #endregion

    #endregion

    #region Task<Result> => Task<Result<TValue>>

    #region () => Result<TOut>
    /// <summary>
    /// Asynchronously binds a <see cref="Task{Result}"/> to a new <see cref="Result{TOut}"/> by applying an asynchronous function.
    /// This method awaits the input <see cref="Task{Result}"/> and then applies an asynchronous binding function
    /// if the awaited result is successful, allowing for propagation of success or failure.
    /// </summary>
    /// <typeparam name="TOut">The type of the value contained in the output <see cref="Result{TOut}"/>.</typeparam>
    /// <param name="resultTask">The input <see cref="Task{Result}"/> representing an asynchronous operation that yields a non-generic result.</param>
    /// <param name="mapper">
    /// The asynchronous function to apply if the awaited <paramref name="resultTask"/> is successful.
    /// This function takes no arguments and returns a <see cref="Result{TOut}"/>.
    /// </param>
    /// <returns>
    /// A <see cref="Result{TOut}"/> representing the asynchronous operation:
    /// If the awaited <paramref name="resultTask"/> is successful, the result of applying the <paramref name="mapper"/> function.
    /// If the awaited <paramref name="resultTask"/> is a failure, a <see cref="Result{TOut}"/> containing the original error is returned.
    /// </returns>
    public static async Task<Result<TOut>> BindAsync<TOut>(
        this Task<Result> resultTask,
        Func<Task<Result<TOut>>> mapper
    ) => await (await resultTask.ConfigureAwait(false)).BindAsync(mapper).ConfigureAwait(false);
    #endregion

    #endregion

    #endregion

    #region Result<TValue>

    #region Result<TValue> => Task<Result<TValue>>

    #region TIn => Result<TOut>
    /// <summary>
    /// Asynchronously binds a <see cref="Result{TIn}"/> to a new <see cref="Result{TOut}"/> by applying an asynchronous function to its contained value.
    /// This method is used to chain asynchronous operations where the next step (defined by the <paramref name="mapper"/>)
    /// takes the successful value of the current result and returns a new <see cref="Result{TOut}"/>,
    /// allowing for propagation of success or failure.
    /// </summary>
    /// <typeparam name="TIn">The type of the value contained in the input <see cref="Result{TIn}"/>.</typeparam>
    /// <typeparam name="TOut">The type of the value contained in the output <see cref="Result{TOut}"/>.</typeparam>
    /// <param name="result">The input <see cref="Result{TIn}"/>.</param>
    /// <param name="mapper">
    /// The asynchronous function to apply if the input <paramref name="result"/> is successful.
    /// This function takes the successful value of type <typeparamref name="TIn"/> and returns a <see cref="Result{TOut}"/>.
    /// </param>
    /// <returns>
    /// A <see cref="Result{TOut}"/> representing the asynchronous operation:
    /// If the input <paramref name="result"/> is successful, the result of applying the <paramref name="mapper"/> function to its value.
    /// If the input <paramref name="result"/> is a failure, a <see cref="Result{TOut}"/> containing the original error is returned.
    /// </returns>
    public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, Task<Result<TOut>>> mapper
    ) =>
        result.IsSuccess
            ? await mapper(result.Value).ConfigureAwait(false)
            : result.Error;
    #endregion

    #endregion

    #region Task<Result<TValue>> => Result<TValue>

    #region TIn => Result<TOut>
    /// <summary>
    /// Asynchronously binds a <see cref="Result{TIn}"/> to a new <see cref="Result{TOut}"/> by applying a synchronous function to its contained value.
    /// This method awaits the input <see cref="Result{TIn}"/> and then applies a synchronous binding function
    /// if the awaited result is successful, allowing for propagation of success or failure.
    /// </summary>
    /// <typeparam name="TIn">The type of the value contained in the input <see cref="Result{TIn}"/>.</typeparam>
    /// <typeparam name="TOut">The type of the value contained in the output <see cref="Result{TOut}"/>.</typeparam>
    /// <param name="resultTask">The input <see cref="Result{TIn}"/> representing an asynchronous operation that yields a generic result.</param>
    /// <param name="mapper">
    /// The synchronous function to apply if the awaited <paramref name="resultTask"/> is successful.
    /// This function takes the successful value of type <typeparamref name="TIn"/> and returns a <see cref="Result{TOut}"/>.
    /// </param>
    /// <returns>
    /// A <see cref="Result{TOut}"/> representing the asynchronous operation:
    /// If the awaited <paramref name="resultTask"/> is successful, the result of applying the <paramref name="mapper"/> function to its value.
    /// If the awaited <paramref name="resultTask"/> is a failure, the original error is propagated.
    /// </returns>
    public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
        this Task<Result<TIn>> resultTask,
        Func<TIn, Result<TOut>> mapper
    ) => (await resultTask.ConfigureAwait(false)).Bind(mapper);
    #endregion

    #endregion

    #region Task<Result<TValue>> => Task<Result<TValue>>

    #region TIn => Result<TOut>
    /// <summary>
    /// Asynchronously binds a <see cref="Result{TIn}"/> to a new <see cref="Result{TOut}"/> by applying an asynchronous function to its contained value.
    /// This method awaits the input <see cref="Result{TIn}"/> and then applies an asynchronous binding function
    /// if the awaited result is successful, allowing for propagation of success or failure.
    /// </summary>
    /// <typeparam name="TIn">The type of the value contained in the input <see cref="Result{TIn}"/>.</typeparam>
    /// <typeparam name="TOut">The type of the value contained in the output <see cref="Result{TOut}"/>.</typeparam>
    /// <param name="resultTask">The input <see cref="Result{TIn}"/> representing an asynchronous operation that yields a generic result.</param>
    /// <param name="mapper">
    /// The asynchronous function to apply if the awaited <paramref name="resultTask"/> is successful.
    /// This function takes the successful value of type <typeparamref name="TIn"/> and returns a <see cref="Result{TOut}"/>.
    /// </param>
    /// <returns>
    /// A <see cref="Result{TOut}"/> representing the asynchronous operation:
    /// If the awaited <paramref name="resultTask"/> is successful, the result of applying the <paramref name="mapper"/> function to its value.
    /// If the awaited <paramref name="resultTask"/> is a failure, a <see cref="Result{TOut}"/> containing the original error is returned.
    /// </returns>
    public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
        this Task<Result<TIn>> resultTask,
        Func<TIn, Task<Result<TOut>>> mapper
    )
    {
        var result = await resultTask.ConfigureAwait(false);

        return result.IsSuccess ? await mapper(result.Value).ConfigureAwait(false) : result.Error;
    }
    #endregion

    #endregion

    #endregion
}