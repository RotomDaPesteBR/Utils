namespace LightningArc.Utils.Results
{
    public partial class Error
    {
        /// <summary>
        /// Representa o módulo de erros de aplicação.
        /// </summary>
        /// <remarks>
        /// Este módulo contém erros de alto nível e genéricos da aplicação,
        /// como falhas internas e problemas de fluxo. O prefixo de código
        /// para erros deste módulo é 1.
        /// </remarks>
        public partial class Application : ErrorModule
        {
            /// <summary>
            /// Obtém o prefixo de código da categoria do erro.
            /// </summary>
            /// <remarks>
            /// Este valor é usado para categorizar o erro e é combinado com um sufixo para formar o código de erro completo.
            /// </remarks>
            public new const int CodePrefix = (int)ModuleCodes.Application;

            /// <summary>
            /// Define os sufixos numéricos para os erros do módulo Application (prefixo 1).
            /// Estes valores são usados para compor o código de erro completo (ex: 001, 002, etc.).
            /// </summary>
            public enum Codes
            {
                /// <summary>
                /// Código '1'. O erro genérico para qualquer falha interna não mapeada (Código: 001).
                /// </summary>
                Internal = 1,

                /// <summary>
                /// Código '2'. Um ou mais parâmetros em uma chamada interna de função são inválidos (Código: 002).
                /// </summary>
                InvalidParameter = 2,

                /// <summary>
                /// Código '3'. Uma operação é logicamente inválida no estado atual do objeto ou sistema (Código: 003).
                /// </summary>
                InvalidOperation = 3,

                /// <summary>
                /// Código '4'. A operação foi interrompida ou cancelada prematuramente (Código: 004).
                /// </summary>
                TaskCanceled = 4,

                /// <summary>
                /// Código '5'. Recurso, método ou funcionalidade ainda não implementada (Código: 005).
                /// </summary>
                NotImplemented = 5,
            }

            // --- Classes Internas de Erro ---

            /// <summary>
            /// Representa um erro interno de aplicação (001).
            /// </summary>
            internal class InternalError : Error
            {
                internal InternalError(string message, IEnumerable<ErrorDetail>? details = null)
                    : base(Application.CodePrefix, (int)Codes.Internal, message, details) { }
            }

            /// <summary>
            /// Representa um erro de parâmetro inválido (002).
            /// </summary>
            internal class InvalidParameterError : Error
            {
                internal InvalidParameterError(
                    string message,
                    IEnumerable<ErrorDetail>? details = null
                )
                    : base(Application.CodePrefix, (int)Codes.InvalidParameter, message, details)
                { }
            }

            /// <summary>
            /// Representa um erro de operação inválida (003).
            /// </summary>
            internal class InvalidOperationError : Error
            {
                internal InvalidOperationError(
                    string message,
                    IEnumerable<ErrorDetail>? details = null
                )
                    : base(Application.CodePrefix, (int)Codes.InvalidOperation, message, details)
                { }
            }

            /// <summary>
            /// Representa um erro de tarefa cancelada (004).
            /// </summary>
            internal class TaskCanceledError : Error
            {
                internal TaskCanceledError(string message, IEnumerable<ErrorDetail>? details = null)
                    : base(Application.CodePrefix, (int)Codes.TaskCanceled, message, details) { }
            }

            /// <summary>
            /// Representa um erro de funcionalidade não implementada (005).
            /// </summary>
            internal class NotImplementedError : Error
            {
                internal NotImplementedError(
                    string message,
                    IEnumerable<ErrorDetail>? details = null
                )
                    : base(Application.CodePrefix, (int)Codes.NotImplemented, message, details) { }
            }

            // --- Construtores Estáticos ---

            /// <summary>
            /// Cria uma nova instância de um erro interno (código 01).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Erro interno da aplicação."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um erro interno.</returns>
            public static Error Internal(
                string message = "Erro interno da aplicação.",
                params IEnumerable<ErrorDetail>? details
            ) => new InternalError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de parâmetro inválido (código 02).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Um ou mais parâmetros são inválidos."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um parâmetro inválido.</returns>
            public static Error InvalidParameter(
                string message = "Um ou mais parâmetros são inválidos.",
                params IEnumerable<ErrorDetail>? details
            ) => new InvalidParameterError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de operação inválida (código 03).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "A operação solicitada é inválida no estado atual do sistema."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma operação inválida.</returns>
            public static Error InvalidOperation(
                string message = "A operação solicitada é inválida no estado atual do sistema.",
                params IEnumerable<ErrorDetail>? details
            ) => new InvalidOperationError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de tarefa cancelada (código 04).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "A operação foi cancelada."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma operação cancelada.</returns>
            public static Error TaskCanceled(
                string message = "A operação foi cancelada.",
                params IEnumerable<ErrorDetail>? details
            ) => new TaskCanceledError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de funcionalidade não implementada (código 05).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "A funcionalidade ainda não foi implementada."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma funcionalidade não implementada.</returns>
            public static Error NotImplemented(
                string message = "A funcionalidade ainda não foi implementada.",
                params IEnumerable<ErrorDetail>? details
            ) => new NotImplementedError(message, details);
        }
    }
}
