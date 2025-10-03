namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Fornece métodos de extensão para a classe <see cref="Result{TValue}"/>,
    /// permitindo a composição de operações funcionais.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Verifica uma condição e, se for falsa, transforma o resultado de sucesso em um resultado de falha
        /// usando o erro fornecido. Não faz nada em caso de falha existente.
        /// </summary>
        /// <typeparam name="TValue">O tipo de valor.</typeparam>
        /// <param name="result">O resultado de entrada.</param>
        /// <param name="predicate">A condição a ser verificada.</param>
        /// <param name="error">O erro a ser retornado se a condição for falsa.</param>
        /// <returns>O resultado original (se for sucesso e a condição for verdadeira), ou um resultado de falha.</returns>
        public static Result<TValue> Ensure<TValue>(
            this Result<TValue> result,
            Func<TValue, bool> predicate,
            Error error
        )
        {
            if (result.IsFailure)
            {
                return result;
            }

            return predicate(result.Value) ? result : error;
        }
    }
}
