namespace LightningArc.Utils.Results
{
    public partial class Error
    {
        /// <summary>
        /// Representa o módulo de erros de recurso.
        /// </summary>
        /// <remarks>
        /// Este módulo contém erros que ocorrem quando há problemas
        /// na gestão de um recurso, como ele não ser encontrado ou
        /// já existir. O prefixo de código para erros deste módulo é 5.
        /// </remarks>
        public partial class Resource : ErrorModule
        {
            /// <summary>
            /// Obtém o prefixo de código da categoria do erro.
            /// </summary>
            /// <remarks>
            /// Este valor é usado para categorizar o erro e é combinado com um sufixo para formar o código de erro completo.
            /// </remarks>
            public new const int CodePrefix = (int)ModuleCodes.Resource;

            /// <summary>
            /// Define os sufixos numéricos para os erros do módulo Resource (prefixo 5).
            /// Estes valores são usados para compor o código de erro completo (ex: 5001, 5002, etc.).
            /// </summary>
            public enum Codes
            {
                /// <summary>
                /// Código '1'. O recurso solicitado não existe.
                /// </summary>
                NotFound = 1,

                /// <summary>
                /// Código '2'. Tentativa de criar um recurso que já existe.
                /// </summary>
                AlreadyExists = 2,

                /// <summary>
                /// Código '3'. O recurso existe, mas não pode ser acessado ou utilizado no momento.
                /// </summary>
                Unavailable = 3,

                /// <summary>
                /// Código '4'. O recurso não está no estado esperado para a operação (ex: tentar deletar um recurso já excluído).
                /// </summary>
                InvalidState = 4,

                /// <summary>
                /// Código '5'. O recurso solicitado está obsoleto ou descontinuado.
                /// </summary>
                Obsolete = 5,
            }

            // --- Classes Internas de Erro ---

            /// <summary>
            /// Representa um erro de recurso não encontrado (Sufixo: 01).
            /// </summary>
            internal class NotFoundError : Error
            {
                internal NotFoundError(string message, IEnumerable<ErrorDetail>? details = null)
                    : base(Resource.CodePrefix, (int)Codes.NotFound, message, details) { }
            }

            /// <summary>
            /// Representa um erro de recurso já existente (Sufixo: 02).
            /// </summary>
            internal class AlreadyExistsError : Error
            {
                internal AlreadyExistsError(
                    string message,
                    IEnumerable<ErrorDetail>? details = null
                )
                    : base(Resource.CodePrefix, (int)Codes.AlreadyExists, message, details) { }
            }

            /// <summary>
            /// Representa um erro de recurso indisponível (Sufixo: 03).
            /// </summary>
            internal class UnavailableError : Error
            {
                internal UnavailableError(string message, IEnumerable<ErrorDetail>? details = null)
                    : base(Resource.CodePrefix, (int)Codes.Unavailable, message, details) { }
            }

            /// <summary>
            /// Representa um erro de estado de recurso inválido (Sufixo: 04).
            /// </summary>
            internal class InvalidStateError : Error
            {
                internal InvalidStateError(string message, IEnumerable<ErrorDetail>? details = null)
                    : base(Resource.CodePrefix, (int)Codes.InvalidState, message, details) { }
            }

            /// <summary>
            /// Representa um erro de recurso obsoleto (Sufixo: 05).
            /// </summary>
            internal class ObsoleteError : Error
            {
                internal ObsoleteError(string message, IEnumerable<ErrorDetail>? details = null)
                    : base(Resource.CodePrefix, (int)Codes.Obsolete, message, details) { }
            }

            // --- Construtores Estáticos ---

            /// <summary>
            /// Cria uma nova instância de um erro de recurso não encontrado (código 01).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "O recurso solicitado não foi encontrado."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um recurso não encontrado.</returns>
            public static Error NotFound(
                string message = "O recurso solicitado não foi encontrado.",
                params IEnumerable<ErrorDetail>? details
            ) => new NotFoundError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de recurso já existente (código 02).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "O recurso já existe."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um recurso já existente.</returns>
            public static Error AlreadyExists(
                string message = "O recurso já existe.",
                params IEnumerable<ErrorDetail>? details
            ) => new AlreadyExistsError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de recurso indisponível (código 03).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "O recurso está indisponível no momento."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um recurso indisponível.</returns>
            public static Error Unavailable(
                string message = "O recurso está indisponível no momento.",
                params IEnumerable<ErrorDetail>? details
            ) => new UnavailableError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de estado de recurso inválido (código 04).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "O recurso não está em um estado válido para a operação."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um estado de recurso inválido.</returns>
            public static Error InvalidState(
                string message = "O recurso não está em um estado válido para a operação.",
                params IEnumerable<ErrorDetail>? details
            ) => new InvalidStateError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de recurso obsoleto (código 05).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "O recurso solicitado está obsoleto ou descontinuado."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um recurso obsoleto.</returns>
            public static Error Obsolete(
                string message = "O recurso solicitado está obsoleto ou descontinuado.",
                params IEnumerable<ErrorDetail>? details
            ) => new ObsoleteError(message, details);
        }
    }
}
