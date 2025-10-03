namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Representa um objeto de erro padronizado para a aplicação.
    /// </summary>
    /// <remarks>
    /// Esta classe base é usada em conjunto com o padrão Result para fornecer
    /// informações claras sobre falhas de operações. Os erros podem ser
    /// mapeados para códigos de status HTTP na camada de apresentação da aplicação.
    /// <para>
    /// O código de erro é composto por um prefixo (categoria) e um sufixo (erro específico).
    /// Por exemplo: 1001 (prefixo 10 para "Aplicação", sufixo 01 para "Interno").
    /// </para>
    /// </remarks>
    public partial class Error
    {
        /// <summary>
        /// Obtém o código completo do erro, composto por um prefixo de categoria e um sufixo específico.
        /// </summary>
        public int Code => CodePrefix * 1000 + CodeSuffix;

        /// <summary>
        /// Obtém o prefixo do código de erro, que representa a categoria do erro (ex: 10 para Aplicação).
        /// </summary>
        private protected int CodePrefix { get; }

        /// <summary>
        /// Obtém o sufixo do código de erro, que representa o erro específico dentro de uma categoria (ex: 01 para Erro Interno).
        /// </summary>
        private protected int CodeSuffix { get; }

        /// <summary>
        /// Obtém a mensagem descritiva do erro.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Obtém uma lista de detalhes adicionais do erro, útil para validações com múltiplos problemas.
        /// </summary>
        public IReadOnlyList<ErrorDetail> Details { get; }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Error"/> com um código de erro e uma mensagem específicos.
        /// </summary>
        /// <param name="codePrefix">O prefixo do código de erro, representando a categoria.</param>
        /// <param name="codeSuffix">O sufixo do código de erro, representando o erro específico.</param>
        /// <param name="message">A mensagem descritiva do erro.</param>
        /// <param name="details">Detalhes adicionais do erro.</param>
        protected Error(
            int codePrefix,
            int codeSuffix,
            string message,
            IEnumerable<ErrorDetail>? details = null
        )
        {
            if (message is null)
            {
                throw new ArgumentException(
                    "Error message cannot be null.",
                    nameof(message)
                );
            }

            if (codePrefix <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(codePrefix),
                    "Code prefix must be a positive integer."
                );
            }

            if (codeSuffix <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(codeSuffix),
                    "Code suffix must be a positive integer."
                );
            }

            CodePrefix = codePrefix;
            CodeSuffix = codeSuffix;
            Message = message;
            Details = details?.ToList() ?? [];
        }
    }
}
