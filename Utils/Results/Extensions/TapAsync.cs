namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Fornece métodos de extensão para a classe <see cref="Result{TValue}"/>,
    /// permitindo a composição de operações funcionais.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Executa assincronamente uma função assíncrona no valor de sucesso de um <see cref="Result{TValue}"/> se a operação foi bem-sucedida.
        /// </summary>
        public static async Task<Result<TValue>> TapAsync<TValue>(
            this Task<Result<TValue>> taskResult,
            Action<TValue> action
        )
        {
            var result = await taskResult.ConfigureAwait(false);
            if (result.IsSuccess)
            {
                action(result.Value);
            }
            return result;
        }

        /// <summary>
        /// Executa assincronamente uma função assíncrona no valor de sucesso de um <see cref="Result{TValue}"/> se a operação foi bem-sucedida.
        /// </summary>
        public static async Task<Result<TValue>> TapAsync<TValue>(
            this Result<TValue> result,
            Func<TValue, Task> action
        )
        {
            if (result.IsSuccess)
            {
                await action(result.Value).ConfigureAwait(false);
            }
            return result;
        }

        /// <summary>
        /// Executa assincronamente uma função assíncrona no valor de sucesso de um <see cref="Result{TValue}"/> se a operação foi bem-sucedida.
        /// </summary>
        public static async Task<Result<TValue>> TapAsync<TValue>(
            this Task<Result<TValue>> resultTask,
            Func<TValue, Task> action
        )
        {
            var result = await resultTask.ConfigureAwait(false);
            if (result.IsSuccess)
            {
                await action(result.Value).ConfigureAwait(false);
            }
            return result;
        }
    }
}
