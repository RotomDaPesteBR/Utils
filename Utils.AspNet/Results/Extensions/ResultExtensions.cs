namespace LightningArc.Utils.Results.AspNet.Extensions
{
    /// <summary>
    /// Fornece métodos de extensão para a classe <see cref="Result{TValue}"/>,
    /// permitindo a configuração do tipo de conteúdo (Content-Type) para a resposta HTTP.
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// Cria um <see cref="EndpointResult{TValue}"/> a partir de um <see cref="Result{TValue}"/>
        /// </summary>
        /// <typeparam name="TValue">O tipo do valor de sucesso contido no resultado.</typeparam>
        /// <param name="result">A instância de <see cref="Result{TValue}"/> a ser convertida.</param>
        /// <returns>
        /// Um <see cref="EndpointResult{TValue}"/> que encapsula o resultado original
        /// </returns>
        public static EndpointResult<TValue> ToEndpointResult<TValue>(
            this Result<TValue> result
        ) => EndpointResult<TValue>.FromResult(result, null);

        /// <summary>
        /// Cria um <see cref="EndpointResult{TValue}"/> a partir de um <see cref="Result{TValue}"/>,
        /// especificando o tipo de conteúdo para a serialização da resposta HTTP.
        /// </summary>
        /// <typeparam name="TValue">O tipo do valor de sucesso contido no resultado.</typeparam>
        /// <param name="result">A instância de <see cref="Result{TValue}"/> a ser convertida.</param>
        /// <param name="contentType">O tipo de conteúdo (ex: "text/plain") a ser usado na resposta HTTP.</param>
        /// <returns>
        /// Um <see cref="EndpointResult{TValue}"/> que encapsula o resultado original
        /// e a informação do tipo de conteúdo.
        /// </returns>
        public static EndpointResult<TValue> WithContentType<TValue>(
            this Result<TValue> result,
            string contentType
        ) => EndpointResult<TValue>.FromResult(result, contentType);
    }
}
