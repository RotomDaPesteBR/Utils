namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Fornece métodos de extensão para a classe <see cref="Result{TValue}"/>,
    /// permitindo a composição de operações funcionais.
    /// </summary>
    public static partial class ResultExtensions
    {
        /// <summary>
        /// Retorna o valor de sucesso contido no <see cref="Result{TValue}"/>.
        /// Se o resultado for uma falha, retorna o valor padrão fornecido.
        /// </summary>
        /// <typeparam name="TValue">O tipo de valor.</typeparam>
        /// <param name="result">O resultado.</param>
        /// <param name="defaultValue">O valor a ser retornado em caso de falha.</param>
        /// <returns>O valor de sucesso ou o valor padrão.</returns>
        public static TValue GetValueOrDefault<TValue>(this Result<TValue> result, TValue defaultValue)
        {
            return result.IsSuccess
                ? result.Value
                : defaultValue;
        }

        /// <summary>
        /// Retorna o valor de sucesso contido no <see cref="Result{TValue}"/>.
        /// Se o resultado for uma falha, retorna o valor padrão de <typeparamref name="TValue"/> (i.e., null para referência/anulável, 0 para int, etc.).
        /// </summary>
        /// <typeparam name="TValue">O tipo de valor.</typeparam>
        /// <param name="result">O resultado.</param>
        /// <returns>O valor de sucesso ou o valor padrão de <typeparamref name="TValue"/>.</returns>
        public static TValue? GetValueOrDefault<TValue>(this Result<TValue> result)
        {
            return result.IsSuccess
                ? result.Value
                : default;
        }
    }
}
