using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using LightningArc.Utils.AspNet.CORS.Policies;

namespace LightningArc.Utils.AspNet.CORS
{
    /// <summary>
    /// Fornece métodos de extensão para configurar políticas CORS (Cross-Origin Resource Sharing) predefinidas.
    /// </summary>
    public static class CorsExtensions
    {
        /// <summary>
        /// Adiciona as políticas CORS predefinidas à coleção de serviços.
        /// </summary>
        /// <param name="services">A <see cref="IServiceCollection"/> à qual as políticas CORS serão adicionadas.</param>
        /// <returns>A <see cref="IServiceCollection"/> modificada, para encadeamento de chamadas.</returns>
        /// <remarks>
        /// Este método registra as seguintes políticas CORS:
        /// <list type="bullet">
        ///     <item><description><see cref="AllowAll"/>.AddAllowAllPolicy(): Adiciona uma política CORS que permite todas as origens, métodos e cabeçalhos.</description></item>
        /// </list>
        /// </remarks>
        public static IServiceCollection AddCorsPolicies(this IServiceCollection services)
        {
            services.AddAllowAllPolicy();

            return services;
        }

        /// <summary>
        /// Adiciona as políticas CORS predefinidas ao middleware da aplicação.
        /// </summary>
        /// <param name="app">A <see cref="IApplicationBuilder"/> à qual as políticas do middleware CORS serão adicionadas.</param>
        /// <returns>A <see cref="IApplicationBuilder"/> modificada, para encadeamento de chamadas.</returns>
        /// <remarks>
        /// Este método registra as seguintes políticas CORS:
        /// <list type="bullet">
        ///     <item><description><see cref="AllowAll"/>.UseCors("AllowAll"): Adiciona uma política CORS que permite todas as origens, métodos e cabeçalhos.</description></item>
        /// </list>
        /// </remarks>
        public static IApplicationBuilder UseCorsPolicies(this IApplicationBuilder app)
        {
            app.UseCors("AllowAll");

            return app;
        }
    }
}
