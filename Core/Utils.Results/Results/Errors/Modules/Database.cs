namespace LightningArc.Utils.Results
{
    public partial class Error
    {
        /// <summary>
        /// Representa o módulo de erros de banco de dados.
        /// </summary>
        /// <remarks>
        /// Este módulo contém erros que ocorrem durante a interação com o banco de dados,
        /// como falhas de conexão ou violações de restrições. O prefixo de código
        /// para erros deste módulo é 3.
        /// </remarks>
        public partial class Database : ErrorModule
        {
            /// <summary>
            /// Obtém o prefixo de código da categoria do erro.
            /// </summary>
            /// <remarks>
            /// Este valor é usado para categorizar o erro e é combinado com um sufixo para formar o código de erro completo.
            /// </remarks>
            public new const int CodePrefix = (int)ModuleCodes.Database;

            /// <summary>
            /// Define os sufixos numéricos para os erros do módulo Database (prefixo 3).
            /// Estes valores são usados para compor o código de erro completo (ex: 3001, 3002, etc.).
            /// </summary>
            public enum Codes
            {
                /// <summary>
                /// Código '1'. Falha ao estabelecer uma conexão com o servidor de banco de dados.
                /// </summary>
                ConnectionFailed = 1,

                /// <summary>
                /// Código '2'. Erro durante a execução de um comando SQL (query) no banco de dados.
                /// </summary>
                QueryExecutionFailed = 2,

                /// <summary>
                /// Código '3'. Tentativa de violar uma regra (chave única, chave estrangeira, etc.) do esquema do banco de dados.
                /// </summary>
                ConstraintViolation = 3,

                /// <summary>
                /// Código '4'. Erro temporário que pode ser resolvido com uma nova tentativa (retry).
                /// </summary>
                Transient = 4,

                /// <summary>
                /// Código '5'. Duas ou mais transações se bloquearam mutuamente.
                /// </summary>
                Deadlock = 5,
            }

            // --- Classes Internas de Erro ---

            /// <summary>
            /// Representa um erro de conexão com o banco de dados (Sufixo: 01).
            /// </summary>
            internal class ConnectionFailedError : Error
            {
                internal ConnectionFailedError(string message, List<ErrorDetail>? details = null)
                    : base(Database.CodePrefix, (int)Codes.ConnectionFailed, message, details) { }
            }

            /// <summary>
            /// Representa um erro de execução de consulta (Sufixo: 02).
            /// </summary>
            internal class QueryExecutionFailedError : Error
            {
                internal QueryExecutionFailedError(
                    string message,
                    List<ErrorDetail>? details = null
                )
                    : base(Database.CodePrefix, (int)Codes.QueryExecutionFailed, message, details) { }
            }

            /// <summary>
            /// Representa um erro de violação de restrição (Sufixo: 03).
            /// </summary>
            internal class ConstraintViolationError : Error
            {
                internal ConstraintViolationError(string message, List<ErrorDetail>? details = null)
                    : base(Database.CodePrefix, (int)Codes.ConstraintViolation, message, details) { }
            }

            /// <summary>
            /// Representa um erro temporário no banco de dados (Sufixo: 04).
            /// </summary>
            internal class TransientError : Error
            {
                internal TransientError(string message, List<ErrorDetail>? details = null)
                    : base(Database.CodePrefix, (int)Codes.Transient, message, details) { }
            }

            /// <summary>
            /// Representa um erro de deadlock (Sufixo: 05).
            /// </summary>
            internal class DeadlockError : Error
            {
                internal DeadlockError(string message, List<ErrorDetail>? details = null)
                    : base(Database.CodePrefix, (int)Codes.Deadlock, message, details) { }
            }

            // --- Construtores Estáticos ---

            /// <summary>
            /// Cria uma nova instância de um erro de falha de conexão (código 01).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Falha ao conectar ao banco de dados."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma falha de conexão.</returns>
            public static Error ConnectionFailed(
                string message = "Falha ao conectar ao banco de dados.",
                List<ErrorDetail>? details = null
            ) => new ConnectionFailedError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de falha de execução de consulta (código 02).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Falha ao executar consulta no banco de dados."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma falha de execução de consulta.</returns>
            public static Error QueryExecutionFailed(
                string message = "Falha ao executar consulta no banco de dados.",
                List<ErrorDetail>? details = null
            ) => new QueryExecutionFailedError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de violação de restrição (código 03).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Violação de restrição do banco de dados."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma violação de restrição.</returns>
            public static Error ConstraintViolation(
                string message = "Violação de restrição do banco de dados.",
                List<ErrorDetail>? details = null
            ) => new ConstraintViolationError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro temporário no banco de dados (código 04).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Erro temporário do banco de dados. Tente novamente."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um erro temporário.</returns>
            public static Error Transient(
                string message = "Erro temporário do banco de dados. Tente novamente.",
                List<ErrorDetail>? details = null
            ) => new TransientError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de deadlock (código 05).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Ocorreu um deadlock."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um deadlock.</returns>
            public static Error Deadlock(
                string message = "Ocorreu um deadlock.",
                List<ErrorDetail>? details = null
            ) => new DeadlockError(message, details);
        }
    }
}