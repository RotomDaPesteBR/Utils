namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Provides extension methods for the <see cref="Success{TValue}"/> class,
    /// enabling fluent value transformation (mapping).
    /// </summary>
    public static partial class SuccessExtensions
    {
        /// <summary>
        ///     Maps a <see cref="Success{TIn}" /> to <see cref="Success{TOut}" /> by applying a function to the contained value.
        /// </summary>
        /// <typeparam name="TIn">The type of the input value.</typeparam>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="success">The input <see cref="Success{TIn}" />.</param>
        /// <param name="mapper">The function to apply to the value.</param>
        /// <returns>A new <see cref="Success{TOut}" /> with the mapped value.</returns>
        public static Success<TOut> Map<TIn, TOut>(
            this Success<TIn> success,
            Func<TIn, TOut> mapper
        ) => success.WithValue(mapper(success.Value!));

        /// <summary>
        ///     Maps a <see cref="Success" /> to <see cref="Success{TOut}" /> by applying a function.
        /// </summary>
        /// <typeparam name="TOut">The type of the output value.</typeparam>
        /// <param name="success">The input <see cref="Success" />.</param>
        /// <param name="mapper">The function to apply.</param>
        /// <returns>A new <see cref="Success{TOut}" /> with the mapped value.</returns>
        public static Success<TOut> Map<TOut>(this Success success, Func<TOut> mapper) =>
            success.WithValue(mapper());
    }
}
