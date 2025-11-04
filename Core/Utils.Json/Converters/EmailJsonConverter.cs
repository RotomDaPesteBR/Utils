using System.Text.Json;
using System.Text.Json.Serialization;
using LightningArc.Utils.Abstractions.ValueObjects;

namespace LightningArc.Utils.Json.Converters
{
    /// <summary>
    /// Converte um objeto <see cref="Email"/> para e de uma string JSON.
    /// Isso permite que o campo Email seja tratado como uma string simples na API.
    /// </summary>
    public class EmailJsonConverter : JsonConverter<Email>
    {
        /// <summary>
        /// Lê o valor JSON e o converte para um objeto <see cref="Email"/>.
        /// </summary>
        /// <param name="reader">O leitor JSON.</param>
        /// <param name="typeToConvert">O tipo que está sendo convertido (Email).</param>
        /// <param name="options">As opções de serialização.</param>
        /// <returns>Uma nova instância de <see cref="Email"/>.</returns>
        /// <exception cref="JsonException">Lançada se o token JSON não for uma string.</exception>
        public override Email Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                throw new JsonException("O campo 'email' não pode ser nulo.");
            }

            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException(
                    $"Esperado um token de string para o e-mail, mas recebeu '{reader.TokenType}'."
                );
            }

            string? emailString = reader.GetString();

            try
            {
                return Email.Create(emailString!);
            }
            catch (ArgumentException ex)
            {
                throw new JsonException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Escreve um objeto <see cref="Email"/> como uma string JSON.
        /// </summary>
        /// <param name="writer">O escritor JSON.</param>
        /// <param name="value">O objeto <see cref="Email"/> a ser escrito.</param>
        /// <param name="options">As opções de serialização.</param>
        public override void Write(
            Utf8JsonWriter writer,
            Email value,
            JsonSerializerOptions options
        ) =>
            // Escreve apenas o valor da string do Email
            writer.WriteStringValue(value.Value);
    }
}
