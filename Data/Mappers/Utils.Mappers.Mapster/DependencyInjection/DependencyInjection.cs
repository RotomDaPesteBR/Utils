using Microsoft.Extensions.DependencyInjection;
using LightningArc.Utils.Data.Abstractions.Mappers;
using LightningArc.Utils.Mappers.Mapster.Adapters;
using Mapster;
using MapsterMapper;

namespace LightningArc.Utils.Mappers.Mapster.DependencyInjection;

/// <summary>
/// Extension methods for registering Mapster adapter in the <see cref="IServiceCollection"/>.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers Mapster and its adapter implementation of <see cref="LightningArc.Utils.Data.Abstractions.Mappers.IMapper"/>.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="config">Optional Mapster configuration.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddMapsterAdapter(this IServiceCollection services, TypeAdapterConfig? config = null)
    {
        // Use the standard Mapster registration
        services.AddMapster();

        if (config != null)
        {
            services.AddSingleton(config);
        }

        // Register our specific adapter for our abstraction
        services.AddScoped<LightningArc.Utils.Data.Abstractions.Mappers.IMapper, MapsterAdapter>();

        return services;
    }
}
