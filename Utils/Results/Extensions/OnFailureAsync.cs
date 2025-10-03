namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Fornece métodos de extensão para a classe <see cref="Result{TValue}"/>,
    /// permitindo a composição de operações funcionais.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Executa assincronamente uma ação no erro de um <see cref="Result{TValue}"/> se a operação falhou.
        /// </summary>
        public static async Task<Result<TValue>> OnFailureAsync<TValue>(
            this Task<Result<TValue>> resultTask,
            Action<Error> action
        )
        {
            var result = await resultTask.ConfigureAwait(false);
            if (result.IsFailure)
            {
                action(result.Error);
            }
            return result;
        }

        /// <summary>
        /// Executa assincronamente uma ação no erro de um <see cref="Result{TValue}"/> se a operação falhou.
        /// </summary>
        public static async Task<Result<TValue>> OnFailureAsync<TValue>(
            this Result<TValue> result,
            Func<Error, Task> action
        )
        {
            if (result.IsFailure)
            {
                await action(result.Error).ConfigureAwait(false);
            }
            return result;
        }

        /// <summary>
        /// Executa assincronamente uma ação no erro de um <see cref="Result{TValue}"/> se a operação falhou.
        /// </summary>
        public static async Task<Result<TValue>> OnFailureAsync<TValue>(
            this Task<Result<TValue>> resultTask,
            Func<Error, Task> action
        )
        {
            var result = await resultTask.ConfigureAwait(false);
            if (result.IsFailure)
            {
                await action(result.Error).ConfigureAwait(false);
            }
            return result;
        }
    }
}
