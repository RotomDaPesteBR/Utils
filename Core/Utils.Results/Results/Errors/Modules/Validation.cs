namespace LightningArc.Utils.Results
{
    public partial class Error
    {
        /// <summary>
        /// Representa o módulo de erros de validação de entrada de dados e regras de negócio.
        /// </summary>
        /// <remarks>
        /// Este módulo contém erros relacionados à integridade, formato e conformidade dos dados com o esquema ou regras.
        /// O prefixo de código para erros deste módulo é 4.
        /// </remarks>
        public partial class Validation : ErrorModule
        {
            /// <summary>
            /// Obtém o prefixo de código da categoria do erro.
            /// </summary>
            /// <remarks>
            /// Este valor é usado para categorizar o erro e é combinado com um sufixo para formar o código de erro completo.
            /// </remarks>
            public new const int CodePrefix = (int)ModuleCodes.Validation;

            /// <summary>
            /// Define os sufixos numéricos para os erros do módulo Validation (prefixo 4).
            /// Estes valores são usados para compor o código de erro completo (ex: 4001, 4002, etc.).
            /// </summary>
            public enum Codes
            {
                /// <summary>
                /// Código '1'. O formato dos dados (JSON, XML, etc.) está incorreto ou malformado.
                /// </summary>
                InvalidFormat = 1,

                /// <summary>
                /// Código '2'. A estrutura ou o esquema dos dados não corresponde ao esperado.
                /// </summary>
                InvalidSchema = 2,

                /// <summary>
                /// Código '3'. Falha na conversão de uma string ou bytes para um objeto (desserialização).
                /// </summary>
                DeserializationFailed = 3,

                /// <summary>
                /// Código '4'. Um campo de dados obrigatório está ausente na entrada.
                /// </summary>
                MissingField = 4,

                /// <summary>
                /// Código '5'. O valor de um campo está abaixo do mínimo ou acima do máximo permitido.
                /// </summary>
                ValueOutOfRange = 5,
            }

            // --- Classes Internas de Erro (Corrigidas) ---

            /// <summary>
            /// Representa um erro de formato de dados inválido (Sufixo: 01).
            /// </summary>
            internal class InvalidFormatError : Error
            {
                internal InvalidFormatError(
                    string message,
                    IEnumerable<ErrorDetail>? details = null
                )
                    : base(Validation.CodePrefix, (int)Codes.InvalidFormat, message, details) { }
            }

            /// <summary>
            /// Representa um erro de esquema de dados inválido (Sufixo: 02).
            /// </summary>
            internal class InvalidSchemaError : Error
            {
                internal InvalidSchemaError(
                    string message,
                    IEnumerable<ErrorDetail>? details = null
                )
                    : base(Validation.CodePrefix, (int)Codes.InvalidSchema, message, details) { }
            }

            /// <summary>
            /// Representa um erro de falha na desserialização (Sufixo: 03).
            /// </summary>
            internal class DeserializationFailedError : Error
            {
                internal DeserializationFailedError(
                    string message,
                    IEnumerable<ErrorDetail>? details = null
                )
                    : base(
                        Validation.CodePrefix,
                        (int)Codes.DeserializationFailed,
                        message,
                        details
                    ) { }
            }

            /// <summary>
            /// Representa um erro de campo obrigatório ausente (Sufixo: 04).
            /// </summary>
            internal class MissingFieldError : Error
            {
                internal MissingFieldError(string message, IEnumerable<ErrorDetail>? details = null)
                    : base(Validation.CodePrefix, (int)Codes.MissingField, message, details) { }
            }

            /// <summary>
            /// Representa um erro de valor fora do intervalo (Sufixo: 05).
            /// </summary>
            internal class ValueOutOfRangeError : Error
            {
                internal ValueOutOfRangeError(
                    string message,
                    IEnumerable<ErrorDetail>? details = null
                )
                    : base(Validation.CodePrefix, (int)Codes.ValueOutOfRange, message, details) { }
            }

            // --- Construtores Estáticos ---

            /// <summary>
            /// Cria uma nova instância de um erro de formato de dados inválido (código 01).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Formato de dados inválido."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um formato inválido.</returns>
            public static Error InvalidFormat(
                string message = "Formato de dados inválido.",
                params IEnumerable<ErrorDetail>? details
            ) => new InvalidFormatError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de esquema de dados inválido (código 02).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Esquema de dados inválido."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um esquema de dados inválido.</returns>
            public static Error InvalidSchema(
                string message = "Esquema de dados inválido.",
                params IEnumerable<ErrorDetail>? details
            ) => new InvalidSchemaError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de falha na desserialização (código 03).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Falha na desserialização dos dados."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando uma falha de desserialização.</returns>
            public static Error DeserializationFailed(
                string message = "Falha na desserialização dos dados.",
                params IEnumerable<ErrorDetail>? details
            ) => new DeserializationFailedError(message, details);

            /// <summary>
            /// Cria uma nova instância de um erro de campo obrigatório ausente (código 04).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Um ou mais campos obrigatórios estão ausentes."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um campo obrigatório ausente.</returns>
            public static Error MissingField(
                string message = "Um ou mais campos obrigatórios estão ausentes.",
                params IEnumerable<ErrorDetail>? details
            ) => new MissingFieldError(message, details); // CORRIGIDO: Chamando 'MissingFieldError'

            /// <summary>
            /// Cria uma nova instância de um erro de valor fora do intervalo (código 05).
            /// </summary>
            /// <param name="message">A mensagem descritiva do erro. O valor padrão é "Um ou mais valores estão fora do intervalo permitido."</param>
            /// <param name="details">Uma lista de detalhes adicionais do erro.</param>
            /// <returns>Uma nova instância de <see cref="Error"/> representando um valor fora do intervalo.</returns>
            public static Error ValueOutOfRange(
                string message = "Um ou mais valores estão fora do intervalo permitido.",
                params IEnumerable<ErrorDetail>? details
            ) => new ValueOutOfRangeError(message, details); // CORRIGIDO: Chamando 'ValueOutOfRangeError'
        }
    }
}
