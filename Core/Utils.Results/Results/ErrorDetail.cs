namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Representa um detalhe específico de um erro, fornecendo um contexto adicional e uma mensagem descritiva.
    /// </summary>
    /// <remarks>
    /// Inicializa uma nova instância da <see cref="ErrorDetail"/>
    /// com um contexto e uma mensagem de erro.
    /// </remarks>
    /// <param name="context">O identificador que fornece contexto sobre o erro.</param>
    /// <param name="message">A mensagem de erro específica para o contexto.</param>
    public readonly struct ErrorDetail(string context, string message)
    {
        /// <summary>
        /// Obtém um identificador que fornece contexto adicional sobre o erro.
        /// Pode ser um nome de campo, um ID de recurso, um nome de parâmetro, etc.
        /// </summary>
        public string Context { get; } = context;

        /// <summary>
        /// Obtém a mensagem de erro específica para o contexto.
        /// </summary>
        public string Message { get; } = message;

        /// <summary>
        /// Permite a criação implícita de um ErrorDetail a partir de uma tupla.
        /// </summary>
        public static implicit operator ErrorDetail((string Context, string Message) value)
        {
            return new ErrorDetail(value.Context, value.Message);
        }
    }
}
