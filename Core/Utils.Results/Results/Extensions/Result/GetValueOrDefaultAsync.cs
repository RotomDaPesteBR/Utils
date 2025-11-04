namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Fornece métodos de extensão para a classe <see cref="Result{TValue}"/>,
    /// permitindo a composição de operações funcionais.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        ///     Asynchronously returns the value of a <see cref="Result{T}" /> if it is a success, otherwise invokes an asynchronous factory to create a default value.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="result">The input <see cref="Result{T}" />.</param>
        /// <param name="defaultValueFactory">The asynchronous function to create a default value if the result is a failure.</param>
        /// <returns>The value or the created default value.</returns>
        public static async Task<T> GetValueOrDefaultAsync<T>(
            this Result<T> result,
            Func<Task<T>> defaultValueFactory
        ) => result.IsSuccess ? result.Value! : await defaultValueFactory().ConfigureAwait(false);

        /// <summary>
        ///     Asynchronously returns the value of a <see cref="Result{T}" /> if it is a success, otherwise the default value of the type.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="resultTask">The input <see cref="Result{T}" />.</param>
        /// <returns>The value or the default of <typeparamref name="T" />.</returns>
        public static async Task<T?> GetValueOrDefaultAsync<T>(this Task<Result<T>> resultTask) =>
            (await resultTask.ConfigureAwait(false)).GetValueOrDefault();

        /// <summary>
        ///     Asynchronously returns the value of a <see cref="Result{T}" /> if it is a success, otherwise the provided default value.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="resultTask">The input <see cref="Result{T}" />.</param>
        /// <param name="defaultValue">The default value to return if the result is a failure.</param>
        /// <returns>The value or the provided default value.</returns>
        public static async Task<T> GetValueOrDefaultAsync<T>(
            this Task<Result<T>> resultTask,
            T defaultValue
        ) => (await resultTask.ConfigureAwait(false)).GetValueOrDefault(defaultValue);

        /// <summary>
        ///     Asynchronously returns the value of a <see cref="Result{T}" /> if it is a success, otherwise invokes a factory to create a default value.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="resultTask">The input <see cref="Result{T}" />.</param>
        /// <param name="defaultValueFactory">The function to create a default value if the result is a failure.</param>
        /// <returns>The value or the created default value.</returns>
        public static async Task<T> GetValueOrDefaultAsync<T>(
            this Task<Result<T>> resultTask,
            Func<T> defaultValueFactory
        ) => (await resultTask.ConfigureAwait(false)).GetValueOrDefault(defaultValueFactory());

        /// <summary>
        ///     Asynchronously returns the value of a <see cref="Result{T}" /> if it is a success, otherwise invokes an asynchronous factory to create a default value.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="resultTask">The input <see cref="Result{T}" />.</param>
        /// <param name="defaultValueFactory">The asynchronous function to create a default value if the result is a failure.</param>
        /// <returns>The value or the created default value.</returns>
        public static async Task<T> GetValueOrDefaultAsync<T>(
            this Task<Result<T>> resultTask,
            Func<Task<T>> defaultValueFactory
        )
        {
            Result<T> result = await resultTask.ConfigureAwait(false);

            return result.IsSuccess
                ? result.Value!
                : await defaultValueFactory().ConfigureAwait(false);
        }
    }
}
