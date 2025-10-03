namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Fornece métodos de extensão para a classe <see cref="Result{TValue}"/>,
    /// permitindo a composição de operações funcionais.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Encapsula assincronamente uma operação que pode falhar, transformando o valor de sucesso
        /// em um novo <see cref="Result{TValue}"/>.
        /// </summary>
        public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Task<Result<TOut>>> func
        )
        {
            if (result.IsSuccess)
            {
                return await func(result.Value).ConfigureAwait(false);
            }
            return result.Error;
        }

        /// <summary>
        /// Encapsula assincronamente uma operação que pode falhar, transformando o valor de sucesso
        /// em um novo <see cref="Result{TValue}"/>.
        /// </summary>
        public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, Task<TOut>> func
        )
        {
            var result = await resultTask.ConfigureAwait(false);
            if (result.IsSuccess)
            {
                return Result.Success(
                    await func(result.Value).ConfigureAwait(false),
                    result.SuccessDetails
                );
            }
            return result.Error;
        }

        /// <summary>
        /// Encapsula assincronamente uma operação que pode falhar, transformando o valor de sucesso
        /// em um novo <see cref="Result{TValue}"/>.
        /// </summary>
        public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, Task<Result<TOut>>> func
        )
        {
            var result = await resultTask.ConfigureAwait(false);
            if (result.IsSuccess)
            {
                return await func(result.Value).ConfigureAwait(false);
            }
            return result.Error;
        }

        /// <summary>
        /// Encapsula assincronamente uma operação que pode falhar, transformando o valor de sucesso
        /// em um novo <see cref="Result{TValue}"/>.
        /// </summary>
        public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, TOut> func
        )
        {
            var result = await resultTask.ConfigureAwait(false);
            if (result.IsSuccess)
            {
                return Result.Success(func(result.Value), result.SuccessDetails);
            }
            return result.Error;
        }

        /// <summary>
        /// Encapsula assincronamente uma operação que pode falhar, transformando o valor de sucesso
        /// em um novo <see cref="Result{TValue}"/>.
        /// </summary>
        public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
            this Task<Result<TIn>> resultTask,
            Func<TIn, Result<TOut>> func
        )
        {
            var result = await resultTask.ConfigureAwait(false);
            if (result.IsSuccess)
            {
                return func(result.Value);
            }
            return result.Error;
        }
    }
}
