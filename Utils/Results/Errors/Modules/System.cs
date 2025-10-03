namespace LightningArc.Utils.Results
{
    public partial class Error
    {
        /// <summary>
        /// Representa o módulo de erros de sistema e ambiente.
        /// </summary>
        /// <remarks>
        /// Este módulo contém erros que ocorrem devido a problemas de infraestrutura,
        /// como configurações incorretas ou dependências ausentes.
        /// O prefixo de código para erros deste módulo é 10.
        /// </remarks>
        public partial class System : ErrorModule
        {
            /// <summary>
            /// Obtém o prefixo de código da categoria do erro.
            /// </summary>
            /// <remarks>
            /// Este valor é usado para categorizar o erro e é combinado com um sufixo para formar o código de erro completo.
            /// </remarks>
            public new const int CodePrefix = (int)ModuleCodes.System;

            /// <summary>
            /// Define os sufixos numéricos para os erros do módulo System (prefixo 2).
            /// Estes valores são usados para compor o código de erro completo (ex: 001, 002, etc.).
            /// </summary>
            public enum Codes
            {
                /// <summary>
                /// Código '1'. Falha ao carregar ou processar configurações críticas (Código: 001).
                /// </summary>
                Configuration = 1,

                /// <summary>
                /// Código '2'. Uma dependência (ex: serviço injetado) essencial não foi registrada ou inicializada (Código: 002).
                /// </summary>
                DependencyNotRegistered = 2,

                /// <summary>
                /// Código '3'. Falha devido à exaustão de memória no host (Código: 003).
                /// </summary>
                OutOfMemory = 3,

                /// <summary>
                /// Código '4'. Uma thread de execução foi abortada inesperadamente (Código: 004).
                /// </summary>
                ThreadAborted = 4,

                /// <summary>
                /// Código '5'. O sistema está em manutenção agendada ou temporariamente indisponível (Código: 005).
                /// </summary>
                SystemMaintenance = 5,
            }

            // --- Classes Internas de Erro ---

            /// <summary>
            /// Representa um erro de configuração (001).
            /// </summary>
            internal class ConfigurationError : Error
            {
                internal ConfigurationError(string message, List<ErrorDetail>? details = null)
                    : base(System.CodePrefix, (int)Codes.Configuration, message, details) { }
            }

            /// <summary>
            /// Representa um erro de dependência não registrada (002).
            /// </summary>
            internal class DependencyNotRegisteredError : Error
            {
                internal DependencyNotRegisteredError(string message, List<ErrorDetail>? details = null)
                    : base(System.CodePrefix, (int)Codes.DependencyNotRegistered, message, details) { }
            }

            /// <summary>
            /// Representa um erro de falta de memória (003).
            /// </summary>
            internal class OutOfMemoryError : Error
            {
                internal OutOfMemoryError(string message, List<ErrorDetail>? details = null)
                    : base(System.CodePrefix, (int)Codes.OutOfMemory, message, details) { }
            }

            /// <summary>
            /// Representa um erro de thread abortada (004).
            /// </summary>
            internal class ThreadAbortedError : Error
            {
                internal ThreadAbortedError(string message, List<ErrorDetail>? details = null)
                    : base(System.CodePrefix, (int)Codes.ThreadAborted, message, details) { }
            }

            /// <summary>
            /// Representa um erro de manutenção do sistema (005).
            /// </summary>
            internal class SystemMaintenanceError : Error
            {
                internal SystemMaintenanceError(string message, List<ErrorDetail>? details = null)
                    : base(System.CodePrefix, (int)Codes.SystemMaintenance, message, details) { }
            }

            // --- Construtores Estáticos ---

            /// <summary>
            /// Cria uma nova instância de um erro de configuração (código 01).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Configuração do sistema inválida ou ausente."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um erro de configuração.</returns>
            public static Error Configuration(
                string message = "Configuração do sistema inválida ou ausente.",
                List<ErrorDetail>? details = null
            ) => new ConfigurationError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de dependência não registrada (código 02).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Dependência de sistema não registrada ou ausente."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma dependência ausente.</returns>
            public static Error DependencyNotRegistered(
                string message = "Dependência de sistema não registrada ou ausente.",
                List<ErrorDetail>? details = null
            ) => new DependencyNotRegisteredError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de falta de memória (código 03).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Não há memória suficiente para completar a operação."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma falta de memória.</returns>
            public static Error OutOfMemory(
                string message = "Não há memória suficiente para completar a operação.",
                List<ErrorDetail>? details = null
            ) => new OutOfMemoryError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de thread abortada (código 04).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "A thread de execução foi abortada."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma thread abortada.</returns>
            public static Error ThreadAborted(
                string message = "A thread de execução foi abortada.",
                List<ErrorDetail>? details = null
            ) => new ThreadAbortedError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de manutenção do sistema (código 05).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "O sistema está em manutenção."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um erro de manutenção.</returns>
            public static Error SystemMaintenance(
                string message = "O sistema está em manutenção.",
                List<ErrorDetail>? details = null
            ) => new SystemMaintenanceError(message, details);
        }
    }
}