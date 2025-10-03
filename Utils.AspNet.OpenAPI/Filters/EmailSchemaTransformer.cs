using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using LightningArc.Utils.ValueObjects;

namespace LightningArc.Utils.OpenAPI.Filters
{
    /// <summary>
    /// Um transformador de esquema OpenAPI que garante que o tipo <see cref="Email"/>
    /// seja representado como uma string simples no documento OpenAPI.
    /// </summary>
    public class EmailSchemaTransformer : IOpenApiSchemaTransformer
    {
        /// <summary>
        /// Transforma o esquema OpenAPI para o tipo <see cref="Email"/>,
        /// garantindo que ele seja representado como uma string com formato "email".
        /// </summary>
        /// <param name="schema">O esquema OpenAPI atual a ser transformado.</param>
        /// <param name="context">O contexto do transformador de esquema, contendo informações sobre o esquema em processamento.</param>
        /// <param name="cancellationToken">Um token para observar o cancelamento da operação.</param>
        /// <returns>Uma <see cref="Task"/> que representa a operação assíncrona.</returns>
        /// <remarks>
        /// Se o <paramref name="context"/> indicar que o esquema pertence ao tipo <see cref="Email"/>,
        /// suas propriedades serão removidas, seu tipo será definido como "string",
        /// seu formato como "email" e um exemplo será adicionado para melhor documentação.
        /// </remarks>
        public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
        {
            if (context.JsonTypeInfo?.Type == typeof(Email))
            {
                schema.Properties.Clear();
                schema.Type = "string";
                schema.Format = "email";
                schema.Example = new OpenApiString("usuario@exemplo.com");
            }

            return Task.CompletedTask;
        }
    }
}