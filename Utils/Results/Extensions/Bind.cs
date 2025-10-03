namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Fornece métodos de extensão para a classe <see cref="Result{TValue}"/>,
    /// permitindo a composição de operações funcionais.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Encapsula uma operação que pode falhar, transformando o valor de sucesso
        /// em um novo <see cref="Result{TValue}"/>. Útil para encadear operações que retornam <see cref="Result"/>.
        /// </summary>
        /// <typeparam name="TIn">O tipo de valor de entrada.</typeparam>
        /// <typeparam name="TOut">O tipo de valor de saída.</typeparam>
        /// <param name="result">O resultado de entrada.</param>
        /// <param name="func">A função a ser aplicada ao valor de sucesso, que retorna um novo <see cref="Result{TValue}"/>.</param>
        /// <returns>O novo <see cref="Result{TOut}"/> ou o erro original.</returns>
        public static Result<TOut> Bind<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Result<TOut>> func
        )
        {
            if (result.IsSuccess)
            {
                return func(result.Value);
            }

            return result.Error;
        }

        /// <summary>
        /// Encapsula uma operação que pode falhar, transformando o valor de sucesso
        /// em um novo <see cref="Result{TValue}"/>. Útil para encadear operações que retornam <see cref="Result"/>.
        /// </summary>
        /// <typeparam name="TIn">O tipo de valor de entrada.</typeparam>
        /// <typeparam name="TOut">O tipo de valor de saída.</typeparam>
        /// <param name="result">O resultado de entrada.</param>
        /// <param name="func">A função a ser aplicada ao valor de sucesso, que retorna um novo <see cref="Result{TValue}"/>.</param>
        /// <returns>O novo <see cref="Result{TOut}"/> ou o erro original.</returns>
        public static Result<TOut> Bind<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, TOut> func
        )
        {
            if (result.IsSuccess)
            {
                return Result.Success(func(result.Value), result.SuccessDetails);
            }

            return result.Error;
        }
    }
}
