namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Fornece métodos de extensão para a classe <see cref="Result{TValue}"/>,
    /// permitindo a composição de operações funcionais.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        ///     Maps the error of a <see cref="Result{T}" /> to a new <see cref="Error" />.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="result">The input <see cref="Result{T}" />.</param>
        /// <param name="mapper">The function to apply to the error.</param>
        /// <returns>A new <see cref="Result{T}" /> with the mapped error, or the original success.</returns>
        public static Result<T> MapError<T>(this Result<T> result, Func<Error, Error> mapper) =>
            !result.IsFailure ? result : (Result<T>)mapper(result.Error);

        /// <summary>
        ///     Maps the error of a <see cref="Result" /> to a new <see cref="Error" />.
        /// </summary>
        /// <param name="result">The input <see cref="Result" />.</param>
        /// <param name="mapper">The function to apply to the error.</param>
        /// <returns>A new <see cref="Result" /> with the mapped error, or the original success.</returns>
        public static Result MapError(this Result result, Func<Error, Error> mapper) =>
            !result.IsFailure ? result : (Result)mapper(result.Error);
    }
}
