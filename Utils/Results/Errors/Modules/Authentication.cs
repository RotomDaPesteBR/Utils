namespace LightningArc.Utils.Results
{
    public partial class Error
    {
        /// <summary>
        /// Representa o módulo de erros de autenticação e autorização.
        /// </summary>
        /// <remarks>
        /// Este módulo contém erros que ocorrem durante o processo de
        /// autenticação (identidade) e autorização (permissão). O prefixo de código
        /// para erros deste módulo é 6.
        /// </remarks>
        public partial class Authentication : ErrorModule
        {
            /// <summary>
            /// Obtém o prefixo de código da categoria do erro.
            /// </summary>
            /// <remarks>
            /// Este valor é usado para categorizar o erro e é combinado com um sufixo para formar o código de erro completo.
            /// </remarks>
            public new const int CodePrefix = (int)ModuleCodes.Authentication;

            /// <summary>
            /// Define os sufixos numéricos para os erros do módulo Authentication (prefixo 6).
            /// Estes valores são usados para compor o código de erro completo (ex: 6001, 6002, etc.).
            /// </summary>
            public enum Codes
            {
                /// <summary>
                /// Código '1'. Falha genérica na tentativa de autenticação (ex: token inválido ou ausente).
                /// </summary>
                Unauthorized = 1,

                /// <summary>
                /// Código '2'. O usuário foi autenticado (identidade verificada), mas não possui permissão para a operação (Autorização).
                /// </summary>
                Forbidden = 2,

                /// <summary>
                /// Código '3'. O token de acesso (JWT, OAuth) foi rejeitado porque seu tempo de vida expirou.
                /// </summary>
                TokenExpired = 3,

                /// <summary>
                /// Código '4'. O nome de usuário/e-mail ou senha fornecidos estão incorretos.
                /// </summary>
                InvalidCredentials = 4,

                /// <summary>
                /// Código '5'. A conta de usuário está desativada, bloqueada ou pendente de ativação.
                /// </summary>
                InactiveAccount = 5,

                /// <summary>
                /// Código '6'. A sessão do usuário foi encerrada por inatividade ou logoff (diferente de TokenExpired).
                /// </summary>
                ExpiredSession = 6,
            }

            // --- Classes Internas de Erro ---

            /// <summary>
            /// Representa um erro de autenticação (Sufixo: 01).
            /// </summary>
            internal class UnauthorizedError : Error
            {
                internal UnauthorizedError(string message, List<ErrorDetail>? details = null)
                    : base(Authentication.CodePrefix, (int)Codes.Unauthorized, message, details) { }
            }

            /// <summary>
            /// Representa um erro de acesso proibido (Sufixo: 02).
            /// </summary>
            internal class ForbiddenError : Error
            {
                internal ForbiddenError(string message, List<ErrorDetail>? details = null)
                    : base(Authentication.CodePrefix, (int)Codes.Forbidden, message, details) { }
            }

            /// <summary>
            /// Representa um erro de token expirado (Sufixo: 03).
            /// </summary>
            internal class TokenExpiredError : Error
            {
                internal TokenExpiredError(string message, List<ErrorDetail>? details = null)
                    : base(Authentication.CodePrefix, (int)Codes.TokenExpired, message, details) { }
            }

            /// <summary>
            /// Representa um erro de credenciais inválidas (Sufixo: 04).
            /// </summary>
            internal class InvalidCredentialsError : Error
            {
                internal InvalidCredentialsError(string message, List<ErrorDetail>? details = null)
                    : base(Authentication.CodePrefix, (int)Codes.InvalidCredentials, message, details) { }
            }

            /// <summary>
            /// Representa um erro de conta inativa (Sufixo: 05).
            /// </summary>
            internal class InactiveAccountError : Error
            {
                internal InactiveAccountError(string message, List<ErrorDetail>? details = null)
                    : base(Authentication.CodePrefix, (int)Codes.InactiveAccount, message, details) { }
            }

            /// <summary>
            /// Representa um erro de sessão expirada (Sufixo: 06).
            /// </summary>
            internal class ExpiredSessionError : Error
            {
                internal ExpiredSessionError(string message, List<ErrorDetail>? details = null)
                    : base(Authentication.CodePrefix, (int)Codes.ExpiredSession, message, details) { }
            }

            // --- Construtores Estáticos ---

            /// <summary>
            /// Cria uma nova instância de um erro de falha de autenticação (código 01).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Acesso não autorizado. Credenciais ausentes ou inválidas."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma falha de autenticação.</returns>
            public static Error Unauthorized(
                string message = "Acesso não autorizado. Credenciais ausentes ou inválidas.",
                List<ErrorDetail>? details = null
            ) => new UnauthorizedError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de acesso proibido (código 02).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Acesso proibido. Permissão insuficiente."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um acesso proibido.</returns>
            public static Error Forbidden(
                string message = "Acesso proibido. Permissão insuficiente.",
                List<ErrorDetail>? details = null
            ) => new ForbiddenError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de token expirado (código 03).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "O token de autenticação expirou."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um token expirado.</returns>
            public static Error TokenExpired(
                string message = "O token de autenticação expirou.",
                List<ErrorDetail>? details = null
            ) => new TokenExpiredError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de credenciais inválidas (código 04).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "As credenciais fornecidas são inválidas."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando credenciais inválidas.</returns>
            public static Error InvalidCredentials(
                string message = "As credenciais fornecidas são inválidas.",
                List<ErrorDetail>? details = null
            ) => new InvalidCredentialsError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de conta inativa (código 05).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "A conta de usuário está inativa."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma conta inativa.</returns>
            public static Error InactiveAccount(
                string message = "A conta de usuário está inativa.",
                List<ErrorDetail>? details = null
            ) => new InactiveAccountError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de sessão expirada (código 06).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "A sessão de usuário expirou."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma sessão expirada.</returns>
            public static Error ExpiredSession(
                string message = "A sessão de usuário expirou.",
                List<ErrorDetail>? details = null
            ) => new ExpiredSessionError(message, details);
        }
    }
}