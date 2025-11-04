namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Fornece métodos de extensão para a classe <see cref="Result{TValue}"/>,
    /// permitindo a composição de operações funcionais.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        ///     Asynchronously maps the error of a <see cref="Result{T}" /> to a new <see cref="Error" />.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="result">The input <see cref="Result{T}" />.</param>
        /// <param name="mapper">The asynchronous function to apply to the error.</param>
        /// <returns>A new <see cref="Result{T}" /> with the mapped error, or the original success.</returns>
        public static async Task<Result<T>> MapErrorAsync<T>(
            this Result<T> result,
            Func<Error, Task<Error>> mapper
        ) => result.IsFailure ? await mapper(result.Error).ConfigureAwait(false) : result;

        /// <summary>
        ///     Asynchronously maps the error of a <see cref="Result" /> to a new <see cref="Error" />.
        /// </summary>
        /// <param name="result">The input <see cref="Result" />.</param>
        /// <param name="mapper">The asynchronous function to apply to the error.</param>
        /// <returns>A new <see cref="Result" /> with the mapped error, or the original success.</returns>
        public static async Task<Result> MapErrorAsync(
            this Result result,
            Func<Error, Task<Error>> mapper
        ) => result.IsFailure ? await mapper(result.Error).ConfigureAwait(false) : result;

        /// <summary>
        ///     Asynchronously maps the error of a <see cref="Result{T}" /> to a new <see cref="Error" />.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="resultTask">The input <see cref="Result{T}" />.</param>
        /// <param name="mapper">The function to apply to the error.</param>
        /// <returns>A new <see cref="Result{T}" /> with the mapped error, or the original success.</returns>
        public static async Task<Result<T>> MapErrorAsync<T>(
            this Task<Result<T>> resultTask,
            Func<Error, Error> mapper
        ) => (await resultTask.ConfigureAwait(false)).MapError(mapper);

        /// <summary>
        ///     Asynchronously maps the error of a <see cref="Task{Result}" /> to a new <see cref="Error" />.
        /// </summary>
        /// <param name="resultTask">The input <see cref="Task{Result}" />.</param>
        /// <param name="mapper">The function to apply to the error.</param>
        /// <returns>A new <see cref="Result" /> with the mapped error, or the original success.</returns>
        public static async Task<Result> MapErrorAsync(
            this Task<Result> resultTask,
            Func<Error, Error> mapper
        ) => (await resultTask.ConfigureAwait(false)).MapError(mapper);

        /// <summary>
        ///     Asynchronously maps the error of a <see cref="Result{T}" /> to a new <see cref="Error" />.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="resultTask">The input <see cref="Result{T}" />.</param>
        /// <param name="mapper">The asynchronous function to apply to the error.</param>
        /// <returns>A new <see cref="Result{T}" /> with the mapped error, or the original success.</returns>
        public static async Task<Result<T>> MapErrorAsync<T>(
            this Task<Result<T>> resultTask,
            Func<Error, Task<Error>> mapper
        ) =>
            await (await resultTask.ConfigureAwait(false))
                .MapErrorAsync(mapper)
                .ConfigureAwait(false);

        /// <summary>
        ///     Asynchronously maps the error of a <see cref="Task{Result}" /> to a new <see cref="Error" />.
        /// </summary>
        /// <param name="resultTask">The input <see cref="Task{Result}" />.</param>
        /// <param name="mapper">The asynchronous function to apply to the error.</param>
        /// <returns>A new <see cref="Result" /> with the mapped error, or the original success.</returns>
        public static async Task<Result> MapErrorAsync(
            this Task<Result> resultTask,
            Func<Error, Task<Error>> mapper
        ) =>
            await (await resultTask.ConfigureAwait(false))
                .MapErrorAsync(mapper)
                .ConfigureAwait(false);
    }
}
