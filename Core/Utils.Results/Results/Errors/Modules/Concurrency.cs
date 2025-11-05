namespace LightningArc.Utils.Results
{
    public partial class Error
    {
        /// <summary>
        /// Representa o módulo de erros de concorrência.
        /// </summary>
        /// <remarks>
        /// Este módulo contém erros que ocorrem quando múltiplos processos tentam
        /// acessar ou modificar o mesmo recurso simultaneamente, resultando em conflitos.
        /// O prefixo de código para erros deste módulo é 10.
        /// </remarks>
        public partial class Concurrency : ErrorModule
        {
            /// <summary>
            /// Obtém o prefixo de código da categoria do erro.
            /// </summary>
            /// <remarks>
            /// Este valor é usado para categorizar o erro e é combinado com um sufixo para formar o código de erro completo.
            /// </remarks>
            public new const int CodePrefix = (int)ModuleCodes.Concurrency;

            /// <summary>
            /// Define os sufixos numéricos para os erros do módulo Concurrency (prefixo 10).
            /// Estes valores são usados para compor o código de erro completo (ex: 10001, 10002, etc.).
            /// </summary>
            public enum Codes
            {
                /// <summary>
                /// Código '1'. Um conflito de concorrência genérico detectado (ex: tentativa de salvar dados que foram modificados por outro processo).
                /// </summary>
                Conflict = 1,

                /// <summary>
                /// Código '2'. O recurso está explicitamente bloqueado por uma transação ou lock de outro processo.
                /// </summary>
                Locked = 2,

                /// <summary>
                /// Código '3'. Os dados que estão sendo usados para uma operação já estão obsoletos (stale) e a operação falhou para prevenir inconsistência.
                /// </summary>
                StaleData = 3,

                /// <summary>
                /// Código '4'. O recurso está temporariamente indisponível porque está sendo processado ativamente por outra thread ou operação.
                /// </summary>
                ResourceInUse = 4,
            }

            // --- Classes Internas de Erro ---

            /// <summary>
            /// Representa um erro de conflito de concorrência (Sufixo: 01).
            /// </summary>
            internal class ConflictError : Error
            {
                internal ConflictError(string message, IEnumerable<ErrorDetail>? details = null)
                    : base(Concurrency.CodePrefix, (int)Codes.Conflict, message, details) { }
            }

            /// <summary>
            /// Representa um erro de recurso bloqueado (Sufixo: 02).
            /// </summary>
            internal class LockedError : Error
            {
                internal LockedError(string message, IEnumerable<ErrorDetail>? details = null)
                    : base(Concurrency.CodePrefix, (int)Codes.Locked, message, details) { }
            }

            /// <summary>
            /// Representa um erro de dados obsoletos (stale data) (Sufixo: 03).
            /// </summary>
            internal class StaleDataError : Error
            {
                internal StaleDataError(string message, IEnumerable<ErrorDetail>? details = null)
                    : base(Concurrency.CodePrefix, (int)Codes.StaleData, message, details) { }
            }

            /// <summary>
            /// Representa um erro de recurso em uso (Sufixo: 04).
            /// </summary>
            internal class ResourceInUseError : Error
            {
                internal ResourceInUseError(
                    string message,
                    IEnumerable<ErrorDetail>? details = null
                )
                    : base(Concurrency.CodePrefix, (int)Codes.ResourceInUse, message, details) { }
            }

            // --- Construtores Estáticos ---

            /// <summary>
            /// Cria uma nova instância de um erro de conflito (código 01).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Conflito de concorrência detectado."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um conflito.</returns>
            public static Error Conflict(
                string message = "Conflito de concorrência detectado.",
                params IEnumerable<ErrorDetail>? details
            ) => new ConflictError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de recurso bloqueado (código 02).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "O recurso está bloqueado por outro processo."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um recurso bloqueado.</returns>
            public static Error Locked(
                string message = "O recurso está bloqueado por outro processo.",
                params IEnumerable<ErrorDetail>? details
            ) => new LockedError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de dados obsoletos (código 03).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Os dados estão desatualizados devido à concorrência."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando dados obsoletos.</returns>
            public static Error StaleData(
                string message = "Os dados estão desatualizados devido à concorrência.",
                params IEnumerable<ErrorDetail>? details
            ) => new StaleDataError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de recurso em uso (código 04).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "O recurso já está em uso por outra operação."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um recurso em uso.</returns>
            public static Error ResourceInUse(
                string message = "O recurso já está em uso por outra operação.",
                params IEnumerable<ErrorDetail>? details
            ) => new ResourceInUseError(message, details);
        }
    }
}
