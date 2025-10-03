using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace LightningArc.Utils.AspNet.CORS.Policies
{
    /// <summary>
    /// Fornece métodos de extensão para adicionar a política CORS "AllowAll".
    /// </summary>
    public static class AllowAll
    {
        /// <summary>
        /// Adiciona uma política CORS nomeada "AllowAll" que permite todas as origens, métodos e cabeçalhos.
        /// </summary>
        /// <param name="services">A <see cref="IServiceCollection"/> à qual a política CORS será adicionada.</param>
        /// <returns>A <see cref="IServiceCollection"/> modificada, para encadeamento de chamadas.</returns>
        /// <remarks>
        /// Esta política configura o CORS para permitir:
        /// <list type="bullet">
        ///     <item><description>Todos os cabeçalhos (`*`)</description></item>
        ///     <item><description>Todos os métodos HTTP (`*`)</description></item>
        ///     <item><description>Todas as origens (`*`)</description></item>
        /// </list>
        /// Use esta política com cautela, pois ela remove as restrições de segurança padrão do CORS.
        /// </remarks>
        public static IServiceCollection AddAllowAllPolicy(this IServiceCollection services)
        {
            CorsPolicy policy = new();

            policy.Headers.Add("*");
            policy.Methods.Add("*");
            policy.Origins.Add("*");

            services.AddCors(options => { options.AddPolicy(name: "AllowAll", policy: policy); });

            return services;
        }
    }
}
