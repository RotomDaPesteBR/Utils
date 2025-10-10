namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Fornece métodos de extensão para a classe <see cref="Result{TValue}"/>,
    /// permitindo a composição de operações funcionais.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Aguarda um <see cref="Task{TResult}"/> contendo um <see cref="Result{TValue}"/> e retorna o valor de sucesso.
        /// Se for uma falha, retorna o valor padrão fornecido.
        /// </summary>
        /// <typeparam name="TValue">O tipo de valor.</typeparam>
        /// <param name="resultTask">A Task que contém o resultado.</param>
        /// <param name="defaultValue">O valor a ser retornado em caso de falha.</param>
        /// <returns>Uma <see cref="Task{TResult}"/> contendo o valor de sucesso ou o valor padrão.</returns>
        public static async Task<TValue> GetValueOrDefaultAsync<TValue>(
            this Task<Result<TValue>> resultTask,
            TValue defaultValue
        )
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.IsSuccess ? result.Value : defaultValue;
        }

        /// <summary>
        /// Aguarda um <see cref="Task{TResult}"/> contendo um <see cref="Result{TValue}"/> e retorna o valor de sucesso.
        /// Se for uma falha, retorna o valor padrão de <typeparamref name="TValue"/>.
        /// </summary>
        /// <typeparam name="TValue">O tipo de valor.</typeparam>
        /// <param name="resultTask">A Task que contém o resultado.</param>
        /// <returns>Uma <see cref="Task{TResult}"/> contendo o valor de sucesso ou o valor padrão.</returns>
        public static async Task<TValue?> GetValueOrDefaultAsync<TValue>(
            this Task<Result<TValue>> resultTask
        )
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.IsSuccess ? result.Value : default;
        }
    }
}
