using System.Text.Json;
using System.Text.Json.Serialization;
using LightningArc.Abstractions.ValueObjects;

namespace LightningArc.Json.Converters
{
    /// <summary>
    /// Converts an <see cref="Email"/> object to and from a JSON string.
    /// This allows the Email field to be treated as a simple string in the API.
    /// </summary>
    public class EmailJsonConverter : JsonConverter<Email>
    {
        /// <summary>
        /// Reads the JSON value and converts it to an <see cref="Email"/> object.
        /// </summary>
        /// <param name="reader">The JSON reader.</param>
        /// <param name="typeToConvert">The type being converted (Email).</param>
        /// <param name="options">The serialization options.</param>
        /// <returns>A new instance of <see cref="Email"/>.</returns>
        /// <exception cref="JsonException">Thrown if the JSON token is not a string.</exception>
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
        /// Writes an <see cref="Email"/> object as a JSON string.
        /// </summary>
        /// <param name="writer">The JSON writer.</param>
        /// <param name="value">The <see cref="Email"/> object to be written.</param>
        /// <param name="options">The serialization options.</param>
        public override void Write(
            Utf8JsonWriter writer,
            Email value,
            JsonSerializerOptions options
        ) =>
            // Escreve apenas o valor da string do Email
            writer.WriteStringValue(value.Value);
    }
}

