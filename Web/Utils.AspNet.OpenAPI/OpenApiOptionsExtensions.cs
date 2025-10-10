using Microsoft.AspNetCore.OpenApi;
using LightningArc.Utils.OpenAPI.Filters;

namespace LightningArc.Utils.OpenAPI
{
    /// <summary>
    /// Métodos de extensão para OpenApiOptionsExtensions
    /// </summary>
    public static partial class OpenApiOptionsExtensions
    {
        /// <summary>
        /// Adiciona e configura os SchemaTransformers para os ValueObjects.
        /// </summary>
        /// <param name="openApiOptions">As configurações de OpenAPI.</param>
        /// <returns>A mesma coleção de serviços com os serviços da Infraestrutura adicionados.</returns>
        public static OpenApiOptions AddSchemaTransformers(this OpenApiOptions openApiOptions)
        {
            openApiOptions.AddSchemaTransformer<EmailSchemaTransformer>();

            return openApiOptions;
        }
    }
}
