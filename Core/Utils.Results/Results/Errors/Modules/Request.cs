namespace LightningArc.Utils.Results
{
    public partial class Error
    {
        /// <summary>
        /// Representa o módulo de erros de requisição.
        /// </summary>
        /// <remarks>
        /// Este módulo contém erros relacionados ao processamento da requisição HTTP,
        /// como formato, tamanho e limites de frequência. O prefixo de código
        /// para erros deste módulo é 7.
        /// </remarks>
        public partial class Request : ErrorModule
        {
            /// <summary>
            /// Obtém o prefixo de código da categoria do erro.
            /// </summary>
            /// <remarks>
            /// Este valor é usado para categorizar o erro e é combinado com um sufixo para formar o código de erro completo.
            /// </remarks>
            public new const int CodePrefix = (int)ModuleCodes.Request;

            /// <summary>
            /// Define os sufixos numéricos para os erros do módulo Request (prefixo 7).
            /// Estes valores são usados para compor o código de erro completo (ex: 7001, 7002, etc.).
            /// </summary>
            public enum Codes
            {
                /// <summary>
                /// Código '1'. A estrutura da requisição (URI, headers, body) é inválida.
                /// </summary>
                InvalidRequest = 1,

                /// <summary>
                /// Código '2'. O tamanho total da carga útil (payload) excede o limite permitido pelo servidor.
                /// </summary>
                TooLargeRequest = 2,

                /// <summary>
                /// Código '3'. O cliente excedeu o limite de requisições em um determinado período de tempo (Rate Limiting).
                /// </summary>
                TooManyRequests = 3,

                /// <summary>
                /// Código '4'. O formato de resposta solicitado (via header Accept) não é suportado pelo servidor.
                /// </summary>
                NotAcceptable = 4,

                /// <summary>
                /// Código '5'. O tipo de mídia (via header Content-Type) da requisição não é suportado para este endpoint.
                /// </summary>
                UnsupportedMediaType = 5,
            }

            // --- Classes Internas de Erro ---

            /// <summary>
            /// Representa um erro de requisição inválida (Sufixo: 01).
            /// </summary>
            internal class InvalidRequestError : Error
            {
                internal InvalidRequestError(string message, List<ErrorDetail>? details = null)
                    : base(Request.CodePrefix, (int)Codes.InvalidRequest, message, details) { }
            }

            /// <summary>
            /// Representa um erro de requisição muito grande (Sufixo: 02).
            /// </summary>
            internal class TooLargeRequestError : Error
            {
                internal TooLargeRequestError(string message, List<ErrorDetail>? details = null)
                    : base(Request.CodePrefix, (int)Codes.TooLargeRequest, message, details) { }
            }

            /// <summary>
            /// Representa um erro de excesso de requisições (Sufixo: 03).
            /// </summary>
            internal class TooManyRequestsError : Error
            {
                internal TooManyRequestsError(string message, List<ErrorDetail>? details = null)
                    : base(Request.CodePrefix, (int)Codes.TooManyRequests, message, details) { }
            }

            /// <summary>
            /// Representa um erro de "não aceitável" (Sufixo: 04).
            /// </summary>
            internal class NotAcceptableError : Error
            {
                internal NotAcceptableError(string message, List<ErrorDetail>? details = null)
                    : base(Request.CodePrefix, (int)Codes.NotAcceptable, message, details) { }
            }

            /// <summary>
            /// Representa um erro de tipo de mídia não suportado (Sufixo: 05).
            /// </summary>
            internal class UnsupportedMediaTypeError : Error
            {
                internal UnsupportedMediaTypeError(string message, List<ErrorDetail>? details = null)
                    : base(Request.CodePrefix, (int)Codes.UnsupportedMediaType, message, details) { }
            }

            // --- Construtores Estáticos ---

            /// <summary>
            /// Cria uma nova instância de um erro de requisição inválida (código 01).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "A requisição possui um formato inválido."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma requisição inválida.</returns>
            public static Error Invalid(string message = "A requisição possui um formato inválido.", List<ErrorDetail>? details = null)
                => new InvalidRequestError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de requisição muito grande (código 02).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "A carga da requisição é muito grande."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma carga de requisição muito grande.</returns>
            public static Error TooLarge(string message = "A carga da requisição é muito grande.", List<ErrorDetail>? details = null)
                => new TooLargeRequestError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de excesso de requisições (código 03).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Número excessivo de requisições."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um excesso de requisições.</returns>
            public static Error TooManyRequests(string message = "Número excessivo de requisições.", List<ErrorDetail>? details = null)
                => new TooManyRequestsError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de "não aceitável" (código 04).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "O formato da resposta não é aceitável."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um formato de resposta não aceitável.</returns>
            public static Error NotAcceptable(string message = "O formato da resposta não é aceitável.", List<ErrorDetail>? details = null)
                => new NotAcceptableError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de tipo de mídia não suportado (código 05).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "O tipo de mídia da requisição não é suportado."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um tipo de mídia não suportado.</returns>
            public static Error UnsupportedMediaType(string message = "O tipo de mídia da requisição não é suportado.", List<ErrorDetail>? details = null)
                => new UnsupportedMediaTypeError(message, details);
        }
    }
}