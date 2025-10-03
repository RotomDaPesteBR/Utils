namespace LightningArc.Utils.Results
{
    public partial class Error
    {
        /// <summary>
        /// Representa o módulo de erros de sistemas externos.
        /// </summary>
        /// <remarks>
        /// Este módulo contém erros que ocorrem durante a interação com APIs ou
        /// serviços de terceiros, como limites de uso ou respostas inesperadas.
        /// O prefixo de código para erros deste módulo é 8.
        /// </remarks>
        public partial class External : ErrorModule
        {
            /// <summary>
            /// Obtém o prefixo de código da categoria do erro.
            /// </summary>
            /// <remarks>
            /// Este valor é usado para categorizar o erro e é combinado com um sufixo para formar o código de erro completo.
            /// </remarks>
            public new const int CodePrefix = (int)ModuleCodes.External;

            /// <summary>
            /// Define os sufixos numéricos para os erros do módulo External (prefixo 8).
            /// Estes valores são usados para compor o código de erro completo (ex: 8001, 8002, etc.).
            /// </summary>
            public enum Codes
            {
                /// <summary>
                /// Código '1'. O limite de requisições por período de tempo do serviço externo foi excedido.
                /// </summary>
                RateLimitExceeded = 1,

                /// <summary>
                /// Código '2'. A cota de uso total da API externa (mensal/diária) foi atingida.
                /// </summary>
                ApiQuotaExceeded = 2,

                /// <summary>
                /// Código '3'. A resposta da API externa não estava no formato esperado ou continha dados inconsistentes.
                /// </summary>
                InvalidApiResponse = 3,

                /// <summary>
                /// Código '4'. O serviço externo está inoperante ou temporariamente indisponível.
                /// </summary>
                ServiceUnavailable = 4,

                /// <summary>
                /// Código '5'. A requisição para o serviço externo atingiu o tempo limite (timeout).
                /// </summary>
                Timeout = 5,

                /// <summary>
                /// Código '6'. Falha de comunicação de baixo nível (ex: erro de SSL/TLS, falha de handshake).
                /// </summary>
                Communication = 6,
            }

            // --- Classes Internas de Erro ---

            /// <summary>
            /// Representa um erro de limite de taxa excedido (Sufixo: 01).
            /// </summary>
            internal class RateLimitExceededError : Error
            {
                internal RateLimitExceededError(string message, List<ErrorDetail>? details = null)
                    : base(External.CodePrefix, (int)Codes.RateLimitExceeded, message, details) { }
            }

            /// <summary>
            /// Representa um erro de cota de API excedida (Sufixo: 02).
            /// </summary>
            internal class ApiQuotaExceededError : Error
            {
                internal ApiQuotaExceededError(string message, List<ErrorDetail>? details = null)
                    : base(External.CodePrefix, (int)Codes.ApiQuotaExceeded, message, details) { }
            }

            /// <summary>
            /// Representa um erro de resposta de API inválida (Sufixo: 03).
            /// </summary>
            internal class InvalidApiResponseError : Error
            {
                internal InvalidApiResponseError(string message, List<ErrorDetail>? details = null)
                    : base(External.CodePrefix, (int)Codes.InvalidApiResponse, message, details) { }
            }

            /// <summary>
            /// Representa um erro de serviço indisponível (Sufixo: 04).
            /// </summary>
            internal class ServiceUnavailableError : Error
            {
                internal ServiceUnavailableError(string message, List<ErrorDetail>? details = null)
                    : base(External.CodePrefix, (int)Codes.ServiceUnavailable, message, details) { }
            }

            /// <summary>
            /// Representa um erro de timeout (Sufixo: 05).
            /// </summary>
            internal class TimeoutError : Error
            {
                internal TimeoutError(string message, List<ErrorDetail>? details = null)
                    : base(External.CodePrefix, (int)Codes.Timeout, message, details) { }
            }

            /// <summary>
            /// Representa um erro de comunicação (Sufixo: 06).
            /// </summary>
            internal class CommunicationError : Error
            {
                internal CommunicationError(string message, List<ErrorDetail>? details = null)
                    : base(External.CodePrefix, (int)Codes.Communication, message, details) { }
            }

            // --- Construtores Estáticos ---

            /// <summary>
            /// Cria uma nova instância de um erro de limite de taxa excedido (código 01).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Limite de requisições excedido."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um limite de taxa excedido.</returns>
            public static Error RateLimitExceeded(
                string message = "Limite de requisições excedido.",
                List<ErrorDetail>? details = null
            ) => new RateLimitExceededError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de cota de API excedida (código 02).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Cota de API excedida."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma cota de API excedida.</returns>
            public static Error ApiQuotaExceeded(
                string message = "Cota de API excedida.",
                List<ErrorDetail>? details = null
            ) => new ApiQuotaExceededError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de resposta de API inválida (código 03).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Resposta inválida da API externa."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma resposta de API inválida.</returns>
            public static Error InvalidApiResponse(
                string message = "Resposta inválida da API externa.",
                List<ErrorDetail>? details = null
            ) => new InvalidApiResponseError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de serviço indisponível (código 04).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "O serviço solicitado não está disponível no momento."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um serviço indisponível.</returns>
            public static Error ServiceUnavailable(
                string message = "O serviço solicitado não está disponível no momento.",
                List<ErrorDetail>? details = null
            ) => new ServiceUnavailableError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de tempo limite (código 05).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "A requisição para o serviço externo atingiu o tempo limite."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um tempo limite.</returns>
            public static Error Timeout(
                string message = "A requisição para o serviço externo atingiu o tempo limite.",
                List<ErrorDetail>? details = null
            ) => new TimeoutError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de comunicação (código 06).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Falha de comunicação com o serviço externo."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma falha de comunicação.</returns>
            public static Error Communication(
                string message = "Falha de comunicação com o serviço externo.",
                List<ErrorDetail>? details = null
            ) => new CommunicationError(message, details);
        }
    }
}