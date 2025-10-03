namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Fornece métodos de extensão para a classe <see cref="Result{TValue}"/>,
    /// permitindo a composição de operações funcionais.
    /// </summary>
    public static partial class ResultExtensions
    {
        #region Result
        /// <summary>
        /// Mapeia um resultado não genérico (sem valor) para um novo Result{TOut}.
        /// </summary>
        /// <typeparam name="TOut">O tipo de valor de saída.</typeparam>
        /// <param name="result">O resultado de entrada (sem valor).</param>
        /// <param name="success">A função a ser aplicada ao sucesso, retornando Result{TOut}.</param>
        /// <param name="failure">A função a ser aplicada ao erro, retornando Result{TOut}.</param>
        /// <returns>O novo Result{TOut} mapeado.</returns>
        public static Result<TOut> Match<TOut>(
            this Result result,
            Func<Result<TOut>> success,
            Func<Error, Result<TOut>> failure
        )
        {
            return result.IsSuccess ? success() : failure(result.Error);
        }

        /// <summary>
        /// Mapeia um resultado não genérico (sem valor) para um novo Result{TOut} onde o sucesso retorna apenas TOut.
        /// </summary>
        /// <typeparam name="TOut">O tipo de valor de saída.</typeparam>
        /// <param name="result">O resultado de entrada (sem valor).</param>
        /// <param name="success">A função a ser aplicada ao sucesso, retornando TOut (que é empacotado).</param>
        /// <param name="failure">A função a ser aplicada ao erro, retornando Result{TOut}.</param>
        /// <returns>O novo Result{TOut} mapeado.</returns>
        public static Result<TOut> Match<TOut>(
            this Result result,
            Func<TOut> success,
            Func<Error, Result<TOut>> failure
        )
        {
            // Usa a conversão implícita de TOut para Result<TOut> ou Result.Success() explícito.
            return result.IsSuccess
                ? Result.Success(success(), result.SuccessDetails)
                : failure(result.Error);
        }

        /// <summary>
        /// Mapeia um resultado não genérico (sem valor) para outro Result.
        /// </summary>
        /// <param name="result">O resultado de entrada (sem valor).</param>
        /// <param name="success">A função a ser aplicada ao sucesso, retornando Result.</param>
        /// <param name="failure">A função a ser aplicada ao erro, retornando Result.</param>
        /// <returns>O novo Result mapeado.</returns>
        public static Result Match(
            this Result result,
            Func<Result> success,
            Func<Error, Result> failure
        )
        {
            return result.IsSuccess ? success() : failure(result.Error);
        }

        /// <summary>
        /// Desembrulha um resultado não genérico (sem valor) aplicando uma das duas funções,
        /// retornando o valor final TOut. Este método encerra o encadeamento do Result.
        /// </summary>
        /// <typeparam name="TOut">O tipo de valor de saída (desembrulhado).</typeparam>
        /// <param name="result">O resultado de entrada (sem valor).</param>
        /// <param name="success">A função a ser aplicada ao sucesso, que retorna TOut.</param>
        /// <param name="failure">A função a ser aplicada ao erro, que retorna TOut.</param>
        /// <returns>O valor final TOut.</returns>
        public static TOut Match<TOut>(
            this Result result,
            Func<TOut> success,
            Func<Error, TOut> failure
        )
        {
            return result.IsSuccess ? success() : failure(result.Error!);
        }

        /// <summary>
        /// Mapeia um resultado não genérico (sem valor) aplicando uma das duas funções,
        /// onde a função de sucesso recebe o Result original.
        /// </summary>
        /// <param name="result">O resultado de entrada (sem valor).</param>
        /// <param name="success">A função a ser aplicada ao Result de sucesso, retornando Result.</param>
        /// <param name="failure">A função a ser aplicada ao erro, retornando Result.</param>
        /// <returns>O novo Result mapeado.</returns>
        public static Result Match(
            this Result result,
            Func<Result, Result> success,
            Func<Error, Result> failure
        )
        {
            return result.IsSuccess ? success(result) : failure(result.Error);
        }
        #endregion

        #region Result<TValue>
        /// <summary>
        /// Mapeia o resultado aplicando uma das duas funções: uma para sucesso e outra para falha.
        /// Este método encerra a cadeia de Result, retornando o valor desembrulhado de TOut.
        /// </summary>
        /// <typeparam name="TIn">O tipo de valor de entrada.</typeparam>
        /// <typeparam name="TOut">O tipo de valor de saída.</typeparam>
        /// <param name="result">O resultado de entrada.</param>
        /// <param name="success">A função a ser aplicada ao valor de sucesso.</param>
        /// <param name="failure">A função a ser aplicada ao erro.</param>
        /// <returns>O valor TOut mapeado a partir do valor de sucesso ou do erro.</returns>
        public static Result<TOut> Match<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, TOut> success,
            Func<Error, Result<TOut>> failure
        )
        {
            return result.IsSuccess
                ? Result.Success(success(result.Value), result.SuccessDetails)
                : failure(result.Error);
        }

        /// <summary>
        /// Mapeia o resultado aplicando uma das duas funções: uma para sucesso e outra para falha.
        /// Este método encerra a cadeia de Result, retornando o valor desembrulhado de TOut.
        /// </summary>
        /// <typeparam name="TIn">O tipo de valor de entrada.</typeparam>
        /// <typeparam name="TOut">O tipo de valor de saída.</typeparam>
        /// <param name="result">O resultado de entrada.</param>
        /// <param name="success">A função a ser aplicada ao valor de sucesso.</param>
        /// <param name="failure">A função a ser aplicada ao erro.</param>
        /// <returns>O valor TOut mapeado a partir do valor de sucesso ou do erro.</returns>
        public static Result<TOut> Match<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, Result<TOut>> success,
            Func<Error, Result<TOut>> failure
        )
        {
            return result.IsSuccess ? success(result.Value) : failure(result.Error);
        }

        /// <summary>
        /// Desembrulha o resultado de um <see cref="Result{TIn}"/> aplicando uma das duas funções,
        /// retornando o valor final <typeparamref name="TOut"/>. Este método encerra o encadeamento do <see cref="Result"/>.
        /// </summary>
        /// <typeparam name="TIn">O tipo de valor de entrada.</typeparam>
        /// <typeparam name="TOut">O tipo de valor de saída (desembrulhado).</typeparam>
        /// <param name="result">O resultado de entrada.</param>
        /// <param name="success">A função a ser aplicada ao valor de sucesso, que retorna <typeparamref name="TOut"/>.</param>
        /// <param name="failure">A função a ser aplicada ao erro, que retorna <typeparamref name="TOut"/>.</param>
        /// <returns>O valor final <typeparamref name="TOut"/>.</returns>
        public static TOut Match<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, TOut> success,
            Func<Error, TOut> failure
        )
        {
            return result.IsSuccess ? success(result.Value!) : failure(result.Error!);
        }

        /// <summary>
        /// Mapeia o resultado aplicando uma das duas funções, onde a função de sucesso recebe
        /// o Result{TIn} original.
        /// </summary>
        /// <typeparam name="TIn">O tipo de valor de entrada.</typeparam>
        /// <typeparam name="TOut">O tipo de valor de saída.</typeparam>
        /// <param name="result">O resultado de entrada.</param>
        /// <param name="success">A função a ser aplicada ao Result{TIn} de sucesso, retornando Result{TOut}.</param>
        /// <param name="failure">A função a ser aplicada ao erro, retornando Result{TOut}.</param>
        /// <returns>O novo Result{TOut} mapeado.</returns>
        public static Result<TOut> Match<TIn, TOut>(
            this Result<TIn> result,
            Func<Result<TIn>, Result<TOut>> success,
            Func<Error, Result<TOut>> failure
        )
        {
            return result.IsSuccess ? success(result) : failure(result.Error);
        }
        #endregion
    }
}
