namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Fornece métodos de extensão para a classe <see cref="Result{TValue}"/>,
    /// permitindo a composição de operações funcionais.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Executa uma ação no valor de sucesso de um <see cref="Result{TValue}"/> se a operação foi bem-sucedida.
        /// Não altera o tipo do resultado.
        /// </summary>
        /// <typeparam name="TValue">O tipo de valor.</typeparam>
        /// <param name="result">O resultado.</param>
        /// <param name="action">A ação a ser executada.</param>
        /// <returns>O resultado original.</returns>
        public static Result<TValue> Tap<TValue>(this Result<TValue> result, Action<TValue> action)
        {
            if (result.IsSuccess)
            {
                action(result.Value);
            }
            return result;
        }
    }
}
