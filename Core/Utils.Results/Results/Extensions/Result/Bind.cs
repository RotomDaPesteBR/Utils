namespace LightningArc.Utils.Results;

/// <summary>
/// Provides extension methods for the <see cref="Result{TValue}"/> class,
/// enabling the composition of functional operations.
/// </summary>
public static partial class ResultExtensions
{
    #region Result => Result<TValue>

    #region Result => Result<TOut>
    /// <summary>
    /// Binds a non-generic <see cref="Result"/> to a new <see cref="Result{TOut}"/> by applying a function.
    /// This method is used to chain operations where the next step (defined by the <paramref name="mapper"/>)
    /// also returns a <see cref="Result{TOut}"/>, allowing for propagation of success or failure.
    /// </summary>
    /// <typeparam name="TOut">The type of the value contained in the output <see cref="Result{TOut}"/>.</typeparam>
    /// <param name="result">The input non-generic <see cref="Result"/>.</param>
    /// <param name="mapper">
    /// The function to apply if the input <paramref name="result"/> is successful.
    /// This function takes no arguments and returns a <see cref="Result{TOut}"/>.
    /// </param>
    /// <returns>
    /// A new <see cref="Result{TOut}"/>:
    /// If the input <paramref name="result"/> is successful, the result of applying the <paramref name="mapper"/> function.
    /// If the input <paramref name="result"/> is a failure, the original error is propagated.
    /// </returns>
    public static Result<TOut> Bind<TOut>(this Result result, Func<Result<TOut>> mapper) =>
        result.IsSuccess ? mapper() : result.Error;
    #endregion

    #endregion

    #region Result<TValue> => Result<TValue>

    #region TIn => Result<TOut>
    /// <summary>
    /// Binds a <see cref="Result{TIn}"/> to a new <see cref="Result{TOut}"/> by applying a function to its contained value.
    /// This method is used to chain operations where the next step (defined by the <paramref name="mapper"/>)
    /// takes the successful value of the current result and returns a new <see cref="Result{TOut}"/>,
    /// allowing for propagation of success or failure.
    /// </summary>
    /// <typeparam name="TIn">The type of the value contained in the input <see cref="Result{TIn}"/>.</typeparam>
    /// <typeparam name="TOut">The type of the value contained in the output <see cref="Result{TOut}"/>.</typeparam>
    /// <param name="result">The input <see cref="Result{TIn}"/>.</param>
    /// <param name="mapper">
    /// The function to apply if the input <paramref name="result"/> is successful.
    /// This function takes the successful value of type <typeparamref name="TIn"/> and returns a <see cref="Result{TOut}"/>.
    /// </param>
    /// <returns>
    /// A new <see cref="Result{TOut}"/>:
    /// If the input <paramref name="result"/> is successful, the result of applying the <paramref name="mapper"/> function to its value.
    /// If the input <paramref name="result"/> is a failure, the original error is propagated.
    /// </returns>
    public static Result<TOut> Bind<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, Result<TOut>> mapper
    ) => result.IsSuccess ? mapper(result.Value) : result.Error;
    #endregion

    #endregion
}