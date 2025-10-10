namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Exceção lançada quando uma tentativa de acessar o valor ou o erro de um <see cref="Result"/>
    /// é feita de forma inválida (por exemplo, acessando o valor de um <see cref="Result"/> de falha).
    /// </summary>
    public class ResultAccessFailedException : InvalidOperationException
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ResultAccessFailedException"/>.
        /// </summary>
        public ResultAccessFailedException()
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ResultAccessFailedException"/> com uma mensagem de erro especificada.
        /// </summary>
        /// <param name="message">A mensagem que descreve a exceção.</param>
        public ResultAccessFailedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ResultAccessFailedException"/> com uma mensagem de erro
        /// e uma referência à exceção interna que é a causa desta exceção.
        /// </summary>
        /// <param name="message">A mensagem de erro que descreve a exceção atual.</param>
        /// <param name="innerException">A exceção que é a causa da exceção atual. Se a exceção interna não for especificada,
        /// use <c>null</c>.</param>
        public ResultAccessFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}