using Microsoft.AspNetCore.OpenApi;
using LightningArc.Utils.OpenAPI.Filters;

namespace LightningArc.Utils.OpenAPI
{
    /// <summary>
    /// Extension methods for OpenApiOptions.
    /// </summary>
    public static partial class OpenApiOptionsExtensions
    {
        /// <summary>
        /// Adds and configures SchemaTransformers for ValueObjects.
        /// </summary>
        /// <param name="openApiOptions">The OpenAPI configuration options.</param>
        /// <returns>The same OpenApiOptions instance with Infrastructure services added.</returns>
        public static OpenApiOptions AddSchemaTransformers(this OpenApiOptions openApiOptions)
        {
            openApiOptions.AddSchemaTransformer<EmailSchemaTransformer>();

            return openApiOptions;
        }
    }
}
