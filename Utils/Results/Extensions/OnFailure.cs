namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Fornece métodos de extensão para a classe <see cref="Result{TValue}"/>,
    /// permitindo a composição de operações funcionais.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Executa uma ação no erro de um <see cref="Result{TValue}"/> se a operação falhou.
        /// Não altera o tipo do resultado.
        /// </summary>
        /// <typeparam name="TValue">O tipo de valor.</typeparam>
        /// <param name="result">O resultado.</param>
        /// <param name="action">A ação a ser executada no erro.</param>
        /// <returns>O resultado original.</returns>
        public static Result<TValue> OnFailure<TValue>(
            this Result<TValue> result,
            Action<Error> action
        )
        {
            if (result.IsFailure)
            {
                action(result.Error);
            }
            return result;
        }
    }
}
