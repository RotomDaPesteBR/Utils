using System.Text.RegularExpressions;

namespace LightningArc.Utils.Abstractions.ValueObjects
{
    /// <summary>
    /// Representa um tipo de dado para um endereço de e-mail,
    /// garantindo sua validade no momento da criação através de um método de fábrica.
    /// </summary>
    /// <remarks>
    /// Este 'record' é um tipo de valor imutável. A validação do e-mail é imposta
    /// no construtor privado, acessível por meio do método de fábrica estático <see cref="Create(string)"/>.
    /// </remarks>
    public record Email
    {
        /// <summary>
        /// Expressão regular para validação de formato de e-mail.
        /// Uma regex comum e robusta para validar a maioria dos formatos de e-mail válidos.
        /// </summary>
        private const string EmailRegexPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        /// <summary>
        /// Obtém o valor da string do endereço de e-mail.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Construtor privado da classe Email.
        /// Este construtor realiza a validação do formato da string de e-mail.
        /// </summary>
        /// <param name="value">A string que representa o endereço de e-mail.</param>
        /// <exception cref="ArgumentException">Lançada se 'value' for nulo, vazio ou não for um formato de e-mail válido.</exception>
        private Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("O endereço de e-mail não pode ser nulo ou vazio.", nameof(value));
            }

            if (!Regex.IsMatch(value, EmailRegexPattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                throw new ArgumentException($"O valor '{value}' não é um endereço de e-mail válido.", nameof(value));
            }

            Value = value;
        }

        /// <summary>
        /// Cria uma nova instância de <see cref="Email"/> após validar o formato da string.
        /// Este é o método público preferencial para construir objetos <see cref="Email"/>.
        /// </summary>
        /// <param name="value">A string que representa o endereço de e-mail.</param>
        /// <returns>Uma nova instância de <see cref="Email"/> se a string for um e-mail válido.</returns>
        /// <exception cref="ArgumentException">Lançada se a string fornecida não for um e-mail válido.</exception>
        public static Email Create(string value)
        {
            return new Email(value);
        }

        /// <summary>
        /// Converte implicitamente um objeto <see cref="Email"/> para sua representação em <see cref="string"/>.
        /// </summary>
        /// <param name="email">O objeto <see cref="Email"/> a ser convertido.</param>
        /// <returns>A string do endereço de e-mail.</returns>
        public static implicit operator string(Email email) => email.Value;

        /// <summary>
        /// Converte implicitamente uma <see cref="string"/> para um objeto <see cref="Email"/>.
        /// Este operador utiliza o método de fábrica <see cref="Create(string)"/> para garantir a validação.
        /// </summary>
        /// <param name="value">A string a ser convertida.</param>
        /// <returns>Um novo objeto <see cref="Email"/>.</returns>
        /// <exception cref="ArgumentException">Lançada se a string não for um e-mail válido.</exception>
        public static implicit operator Email(string value) => Create(value);

        /// <summary>
        /// Retorna a representação em string do endereço de e-mail.
        /// </summary>
        /// <returns>O endereço de e-mail como uma string.</returns>
        public override string ToString() => Value;
    }
}