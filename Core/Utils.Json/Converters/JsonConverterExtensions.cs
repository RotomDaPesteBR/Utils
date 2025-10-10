using System.Text.Json.Serialization;
using LightningArc.Utils.Abstractions.ValueObjects;

namespace LightningArc.Utils.Json.Converters
{
    /// <summary>
    /// Métodos de extensão para JsonConverter
    /// </summary>
    public static class JsonConverterExtensions
    {
        /// <summary>
        /// Adiciona e configura os JsonConverters para os ValueObjects.
        /// </summary>
        /// <param name="converters">A coleção de serviços a ser configurada.</param>
        /// <returns>A mesma coleção de serviços com os serviços da Infraestrutura adicionados.</returns>
        public static ICollection<JsonConverter> AddJsonConverters(this ICollection<JsonConverter> converters)
        {
            converters.Add(new EmailJsonConverter());

            return converters;
        }
    }
}
