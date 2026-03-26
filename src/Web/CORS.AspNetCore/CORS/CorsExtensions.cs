using LightningArc.CORS.AspNetCore.Policies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LightningArc.CORS.AspNetCore
{
    /// <summary>
    /// Provides extension methods for configuring predefined CORS (Cross-Origin Resource Sharing) policies.
    /// </summary>
    public static class CorsExtensions
    {
        /// <summary>
        /// Adds predefined CORS policies to the service collection.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to which the CORS policies will be added.</param>
        /// <returns>The modified <see cref="IServiceCollection"/>, for call chaining.</returns>
        /// <remarks>
        /// This method registers the following CORS policies:
        /// <list type="bullet">
        ///     <item><description><see cref="AllowAll"/>.AddAllowAllPolicy(): Adds a CORS policy that allows all origins, methods and headers.</description></item>
        /// </list>
        /// </remarks>
        public static IServiceCollection AddCorsPolicies(this IServiceCollection services)
        {
            services.AddAllowAllPolicy();

            return services;
        }

        /// <summary>
        /// Adds predefined CORS policies to the application middleware.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> to which the CORS middleware policies will be added.</param>
        /// <returns>The modified <see cref="IApplicationBuilder"/>, for call chaining.</returns>
        /// <remarks>
        /// This method registers the following CORS policies:
        /// <list type="bullet">
        ///     <item><description><see cref="AllowAll"/>.UseCors("AllowAll"): Adds a CORS policy that allows all origins, methods and headers.</description></item>
        /// </list>
        /// </remarks>
        public static IApplicationBuilder UseCorsPolicies(this IApplicationBuilder app)
        {
            app.UseCors("AllowAll");

            return app;
        }
    }
}




