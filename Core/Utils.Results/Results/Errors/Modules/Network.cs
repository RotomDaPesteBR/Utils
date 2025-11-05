namespace LightningArc.Utils.Results
{
    public partial class Error
    {
        /// <summary>
        /// Representa o módulo de erros de rede.
        /// </summary>
        /// <remarks>
        /// Este módulo contém erros que ocorrem durante a comunicação de baixo nível (TCP/IP, SSL)
        /// com serviços externos, como falhas de conexão ou timeouts. O prefixo de código
        /// para erros deste módulo é 9.
        /// </remarks>
        public partial class Network : ErrorModule
        {
            /// <summary>
            /// Obtém o prefixo de código da categoria do erro.
            /// </summary>
            /// <remarks>
            /// Este valor é usado para categorizar o erro e é combinado com um sufixo para formar o código de erro completo.
            /// </remarks>
            public new const int CodePrefix = (int)ModuleCodes.Network;

            /// <summary>
            /// Define os sufixos numéricos para os erros do módulo Network (prefixo 9).
            /// Estes valores são usados para compor o código de erro completo (ex: 9001, 9002, etc.).
            /// </summary>
            public enum Codes
            {
                /// <summary>
                /// Código '1'. Falha ao estabelecer a conexão (ex: host inalcançável, porta fechada).
                /// </summary>
                ConnectionFailed = 1,

                /// <summary>
                /// Código '2'. O tempo limite para receber uma resposta ou dados foi excedido.
                /// </summary>
                RequestTimeout = 2,

                /// <summary>
                /// Código '3'. O serviço de rede está temporariamente inoperante (geralmente status 503).
                /// </summary>
                ServiceUnavailable = 3,

                /// <summary>
                /// Código '4'. Falha na resolução do nome de domínio (DNS).
                /// </summary>
                DnsFailure = 4,

                /// <summary>
                /// Código '5'. Falha durante o handshake SSL/TLS ou certificado inválido.
                /// </summary>
                SslHandshakeFailed = 5,

                /// <summary>
                /// Código '6'. Falha ao se conectar ou autenticar através de um servidor proxy.
                /// </summary>
                ProxyFailure = 6,
            }

            // --- Classes Internas de Erro ---

            /// <summary>
            /// Representa um erro de falha na conexão de rede (Sufixo: 01).
            /// </summary>
            internal class ConnectionFailedError : Error
            {
                internal ConnectionFailedError(
                    string message,
                    IEnumerable<ErrorDetail>? details = null
                )
                    : base(Network.CodePrefix, (int)Codes.ConnectionFailed, message, details) { }
            }

            /// <summary>
            /// Representa um erro de timeout da requisição (Sufixo: 02).
            /// </summary>
            internal class RequestTimeoutError : Error
            {
                internal RequestTimeoutError(
                    string message,
                    IEnumerable<ErrorDetail>? details = null
                )
                    : base(Network.CodePrefix, (int)Codes.RequestTimeout, message, details) { }
            }

            /// <summary>
            /// Representa um erro de serviço de rede indisponível (Sufixo: 03).
            /// </summary>
            internal class ServiceUnavailableError : Error
            {
                internal ServiceUnavailableError(
                    string message,
                    IEnumerable<ErrorDetail>? details = null
                )
                    : base(Network.CodePrefix, (int)Codes.ServiceUnavailable, message, details) { }
            }

            /// <summary>
            /// Representa um erro de falha no DNS (Sufixo: 04).
            /// </summary>
            internal class DnsFailureError : Error
            {
                internal DnsFailureError(string message, IEnumerable<ErrorDetail>? details = null)
                    : base(Network.CodePrefix, (int)Codes.DnsFailure, message, details) { }
            }

            /// <summary>
            /// Representa um erro de falha de SSL/TLS (Sufixo: 05).
            /// </summary>
            internal class SslHandshakeFailedError : Error
            {
                internal SslHandshakeFailedError(
                    string message,
                    IEnumerable<ErrorDetail>? details = null
                )
                    : base(Network.CodePrefix, (int)Codes.SslHandshakeFailed, message, details) { }
            }

            /// <summary>
            /// Representa um erro de falha no proxy (Sufixo: 06).
            /// </summary>
            internal class ProxyFailureError : Error
            {
                internal ProxyFailureError(string message, IEnumerable<ErrorDetail>? details = null)
                    : base(Network.CodePrefix, (int)Codes.ProxyFailure, message, details) { }
            }

            // --- Construtores Estáticos ---

            /// <summary>
            /// Cria uma nova instância de um erro de falha de conexão (código 01).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Falha ao conectar à rede."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma falha de conexão.</returns>
            public static Error ConnectionFailed(
                string message = "Falha ao conectar à rede.",
                params IEnumerable<ErrorDetail>? details
            ) => new ConnectionFailedError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de timeout da requisição (código 02).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Tempo limite de requisição excedido."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um timeout.</returns>
            public static Error RequestTimeout(
                string message = "Tempo limite de requisição excedido.",
                params IEnumerable<ErrorDetail>? details
            ) => new RequestTimeoutError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de serviço indisponível (código 03).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Serviço de rede indisponível no momento."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um serviço indisponível.</returns>
            public static Error ServiceUnavailable(
                string message = "Serviço de rede indisponível no momento.",
                params IEnumerable<ErrorDetail>? details
            ) => new ServiceUnavailableError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de falha no DNS (código 04).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Falha na resolução de nome de domínio (DNS)."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma falha de DNS.</returns>
            public static Error DnsFailure(
                string message = "Falha na resolução de nome de domínio (DNS).",
                params IEnumerable<ErrorDetail>? details
            ) => new DnsFailureError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de falha de SSL/TLS (código 05).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Falha na validação do certificado SSL/TLS."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma falha de SSL/TLS.</returns>
            public static Error SslHandshakeFailed(
                string message = "Falha na validação do certificado SSL/TLS.",
                params IEnumerable<ErrorDetail>? details
            ) => new SslHandshakeFailedError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de falha no proxy (código 06).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Falha na conexão ou autenticação do proxy."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma falha no proxy.</returns>
            public static Error ProxyFailure(
                string message = "Falha na conexão ou autenticação do proxy.",
                params IEnumerable<ErrorDetail>? details
            ) => new ProxyFailureError(message, details);
        }
    }
}
