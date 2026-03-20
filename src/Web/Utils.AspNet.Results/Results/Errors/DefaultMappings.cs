namespace LightningArc.Utils.Results.AspNet;

/// <summary>
/// Provides extension methods for the <see cref="ErrorMappingService"/>
/// to group and add error mappings in a modular way.
/// </summary>
public static partial class ErrorMappingServiceExtensions
{
    /// <summary>
    /// Adds all of the library's default error mappings.
    /// </summary>
    /// <param name="service">The error mapping service instance.</param>
    /// <returns>The service instance for chaining.</returns>
    public static ErrorMappingService MapDefaultErrors(this ErrorMappingService service) => service
            .MapApplication()
            .MapSystem()
            .MapDatabase()
            .MapValidation()
            .MapResource()
            .MapAuthentication()
            .MapRequest()
            .MapExternal()
            .MapNetwork()
            .MapConcurrency()
            .MapIO();
}
