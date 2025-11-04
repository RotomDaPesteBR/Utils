namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Provides extension methods for the <see cref="Result{TValue}"/> class,
    /// enabling the composition of functional operations.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        ///     Binds a <see cref="Result{TIn}" /> to a <see cref="Result{TOut}" /> by applying a function to the contained value.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="result">The input <see cref="Result{TIn}" />.</param>
        /// <param name="binder">The function to apply to the value.</param>
        /// <returns>The <see cref="Result{TOut}" /> from the binder function.</returns>
        public static Result<TOut> Bind<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Result<TOut>> binder
        ) => !result.IsSuccess ? Result<TOut>.Failure(result.Error) : binder(result.Value!);

        /// <summary>
        ///     Binds a <see cref="Result" /> to a <see cref="Result{T}" /> by applying a function.
        /// </summary>
        /// <typeparam name="T">The type of the output value.</typeparam>
        /// <param name="result">The input <see cref="Result" />.</param>
        /// <param name="binder">The function to apply.</param>
        /// <returns>The <see cref="Result{T}" /> from the binder function.</returns>
        public static Result<T> Bind<T>(this Result result, Func<Result<T>> binder) =>
            !result.IsSuccess ? Result<T>.Failure(result.Error) : binder();

        /// <summary>
        ///     Binds a <see cref="Result" /> to a <see cref="Result" /> by applying a function.
        /// </summary>
        /// <param name="result">The input <see cref="Result" />.</param>
        /// <param name="binder">The function to apply.</param>
        /// <returns>The <see cref="Result" /> from the binder function.</returns>
        public static Result Bind(this Result result, Func<Result> binder) =>
            !result.IsSuccess ? result : binder();
    }
}
