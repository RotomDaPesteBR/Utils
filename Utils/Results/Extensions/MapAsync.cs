namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Fornece métodos de extensão para a classe <see cref="Result{TValue}"/>,
    /// permitindo a composição de operações funcionais.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Mapeia o valor de um <see cref="Result{TValue}"/> de sucesso para um novo tipo de valor.
        /// Se o resultado atual for uma falha, o erro é propagado para o novo resultado sem executar o mapeador.
        /// </summary>
        /// <typeparam name="TIn">O tipo de valor de entrada.</typeparam>
        /// <typeparam name="TOut">O tipo de valor de saída.</typeparam>
        /// <param name="result">O resultado de entrada.</param>
        /// <param name="mapper">A função de mapeamento a ser aplicada ao valor de sucesso.</param>
        /// <returns>Um novo <see cref="Result{TValue}"/> contendo o valor mapeado em caso de sucesso,
        /// ou o erro original em caso de falha.</returns>
        public async static Task<Result<TOut>> MapAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Task<TOut>> mapper
        )
        {
            if (result.IsSuccess)
            {
                return Result.Success(
                    await mapper(result.Value).ConfigureAwait(false),
                    result.SuccessDetails!
                );
            }
            else
            {
                return result.Error;
            }
        }

        /// <summary>
        /// Mapeia o valor de um <see cref="Result{TValue}"/> de sucesso para um novo tipo de valor.
        /// Se o resultado atual for uma falha, o erro é propagado para o novo resultado sem executar o mapeador.
        /// </summary>
        /// <typeparam name="TIn">O tipo de valor de entrada.</typeparam>
        /// <typeparam name="TOut">O tipo de valor de saída.</typeparam>
        /// <param name="result">O resultado de entrada.</param>
        /// <param name="mapper">A função de mapeamento a ser aplicada ao valor de sucesso.</param>
        /// <returns>Um novo <see cref="Result{TValue}"/> contendo o valor mapeado em caso de sucesso,
        /// ou o erro original em caso de falha.</returns>
        public async static Task<Result<TOut>> MapAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Task<Result<TOut>>> mapper
        )
        {
            if (result.IsSuccess)
            {
                return await mapper(result.Value).ConfigureAwait(false);
            }
            else
            {
                return result.Error;
            }
        }

        /// <summary>
        /// Mapeia o valor de um <see cref="Result{TValue}"/> de sucesso para um novo tipo de valor.
        /// Se o resultado atual for uma falha, o erro é propagado para o novo resultado sem executar o mapeador.
        /// </summary>
        /// <typeparam name="TIn">O tipo de valor de entrada.</typeparam>
        /// <typeparam name="TOut">O tipo de valor de saída.</typeparam>
        /// <param name="taskResult">O resultado de entrada.</param>
        /// <param name="mapper">A função de mapeamento a ser aplicada ao valor de sucesso.</param>
        /// <returns>Um novo <see cref="Result{TValue}"/> contendo o valor mapeado em caso de sucesso,
        /// ou o erro original em caso de falha.</returns>
        public async static Task<Result<TOut>> MapAsync<TIn, TOut>(
            this Task<Result<TIn>> taskResult,
            Func<TIn, Task<TOut>> mapper
        )
        {
            var result = await taskResult.ConfigureAwait(false);
            if (result.IsSuccess)
            {
                return Result.Success(
                    await mapper(result.Value).ConfigureAwait(false),
                    result.SuccessDetails!
                );
            }
            else
            {
                return result.Error;
            }
        }

        /// <summary>
        /// Mapeia o valor de um <see cref="Result{TValue}"/> de sucesso para um novo tipo de valor.
        /// Se o resultado atual for uma falha, o erro é propagado para o novo resultado sem executar o mapeador.
        /// </summary>
        /// <typeparam name="TIn">O tipo de valor de entrada.</typeparam>
        /// <typeparam name="TOut">O tipo de valor de saída.</typeparam>
        /// <param name="taskResult">O resultado de entrada.</param>
        /// <param name="mapper">A função de mapeamento a ser aplicada ao valor de sucesso.</param>
        /// <returns>Um novo <see cref="Result{TValue}"/> contendo o valor mapeado em caso de sucesso,
        /// ou o erro original em caso de falha.</returns>
        public async static Task<Result<TOut>> MapAsync<TIn, TOut>(
            this Task<Result<TIn>> taskResult,
            Func<TIn, Task<Result<TOut>>> mapper
        )
        {
            var result = await taskResult.ConfigureAwait(false);
            if (result.IsSuccess)
            {
                return await mapper(result.Value).ConfigureAwait(false);
            }
            else
            {
                return result.Error;
            }
        }

        /// <summary>
        /// Mapeia o valor de um <see cref="Result{TValue}"/> de sucesso para um novo tipo de valor.
        /// Se o resultado atual for uma falha, o erro é propagado para o novo resultado sem executar o mapeador.
        /// </summary>
        /// <typeparam name="TIn">O tipo de valor de entrada.</typeparam>
        /// <typeparam name="TOut">O tipo de valor de saída.</typeparam>
        /// <param name="taskResult">O resultado de entrada.</param>
        /// <param name="mapper">A função de mapeamento a ser aplicada ao valor de sucesso.</param>
        /// <returns>Um novo <see cref="Result{TValue}"/> contendo o valor mapeado em caso de sucesso,
        /// ou o erro original em caso de falha.</returns>
        public async static Task<Result<TOut>> MapAsync<TIn, TOut>(
            this Task<Result<TIn>> taskResult,
            Func<TIn, TOut> mapper
        )
        {
            var result = await taskResult.ConfigureAwait(false);
            if (result.IsSuccess)
            {
                return Result.Success(mapper(result.Value), result.SuccessDetails!);
            }
            else
            {
                return result.Error;
            }
        }

        /// <summary>
        /// Mapeia o valor de um <see cref="Result{TValue}"/> de sucesso para um novo tipo de valor.
        /// Se o resultado atual for uma falha, o erro é propagado para o novo resultado sem executar o mapeador.
        /// </summary>
        /// <typeparam name="TIn">O tipo de valor de entrada.</typeparam>
        /// <typeparam name="TOut">O tipo de valor de saída.</typeparam>
        /// <param name="taskResult">O resultado de entrada.</param>
        /// <param name="mapper">A função de mapeamento a ser aplicada ao valor de sucesso.</param>
        /// <returns>Um novo <see cref="Result{TValue}"/> contendo o valor mapeado em caso de sucesso,
        /// ou o erro original em caso de falha.</returns>
        public async static Task<Result<TOut>> MapAsync<TIn, TOut>(
            this Task<Result<TIn>> taskResult,
            Func<TIn, Result<TOut>> mapper
        )
        {
            var result = await taskResult.ConfigureAwait(false);
            if (result.IsSuccess)
            {
                return mapper(result.Value);
            }
            else
            {
                return result.Error;
            }
        }
    }
}
