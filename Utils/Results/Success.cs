namespace LightningArc.Utils.Results
{
    /// <summary>
    /// Representa um resultado de sucesso genérico.
    /// Esta classe base é usada para padronizar as respostas de sucesso, permitindo que as subclasses
    /// definam tipos específicos de sucesso.
    /// </summary>
    public abstract class Success
    {
        /// <summary>
        /// Obtém o código numérico do sucesso.
        /// </summary>
        public int Code { get; }

        /// <summary>
        /// Obtém a mensagem de sucesso opcional.
        /// </summary>
        public string? Message { get; }

        /// <remarks>
        /// Construtor protegido para inicializar a instância base de <see cref="Success"/>.
        /// </remarks>
        /// <param name="code">O código numérico do sucesso.</param>
        /// <param name="message">A mensagem opcional de sucesso.</param>
        protected Success(int code, string? message)
        {
            if (code <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(code),
                    "Success code must be a positive integer."
                );
            }

            Code = code;
            Message = message;
        }

        /// <summary>
        /// Cria uma instância de sucesso com o código genérico para "OK".
        /// </summary>
        /// <param name="message">A mensagem opcional de sucesso. O padrão é "Operação concluída com sucesso."</param>
        /// <returns>Uma nova instância de <see cref="Success"/>.</returns>
        public static Success Ok(string? message = "Operação concluída com sucesso.") => new OkSuccess(message);

        /// <summary>
        /// Cria uma instância de sucesso com o código genérico para "Created".
        /// </summary>
        /// <param name="message">A mensagem opcional de sucesso. O padrão é "Recurso criado com sucesso."</param>
        /// <returns>Uma nova instância de <see cref="Success"/>.</returns>
        public static Success Created(string? message = "Recurso criado com sucesso.") => new CreatedSuccess(message);

        /// <summary>
        /// Cria uma instância de sucesso com o código genérico para "Accepted".
        /// </summary>
        /// <param name="message">A mensagem opcional de sucesso. O padrão é "A solicitação foi aceita para processamento."</param>
        /// <returns>Uma nova instância de <see cref="Success"/>.</returns>
        public static Success Accepted(string? message = "A solicitação foi aceita para processamento.") => new AcceptedSuccess(message);

        /// <summary>
        /// Cria uma instância de sucesso com o código genérico para "No Content".
        /// </summary>
        /// <param name="message">A mensagem opcional de sucesso. O padrão é null, pois esta resposta geralmente não deve ter corpo.</param>
        /// <returns>Uma nova instância de <see cref="Success"/>.</returns>
        public static Success NoContent(string? message = null) => new NoContentSuccess(message);

        /// <summary>
        /// Representa o tipo de sucesso para uma operação "Ok".
        /// </summary>
        public sealed class OkSuccess : Success
        {
            internal OkSuccess(string? message = null)
                : base(100, message) { }
        }

        /// <summary>
        /// Representa o tipo de sucesso para uma operação "Created".
        /// </summary>
        public sealed class CreatedSuccess : Success
        {
            internal CreatedSuccess(string? message = null)
                : base(101, message) { }
        }

        /// <summary>
        /// Representa o tipo de sucesso para uma operação "Accepted".
        /// </summary>
        public sealed class AcceptedSuccess : Success
        {
            internal AcceptedSuccess(string? message = null)
                : base(102, message) { }
        }

        /// <summary>
        /// Representa o tipo de sucesso para uma operação "No Content".
        /// </summary>
        public sealed class NoContentSuccess : Success
        {
            internal NoContentSuccess(string? message = null)
                : base(103, message) { }
        }
    }
}
