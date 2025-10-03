namespace LightningArc.Utils.JsonConverter
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
        public static ICollection<System.Text.Json.Serialization.JsonConverter> AddJsonConverters(this ICollection<System.Text.Json.Serialization.JsonConverter> converters)
        {
            converters.Add(new EmailJsonConverter());

            return converters;
        }
    }
}
