using Microsoft.Extensions.Hosting;

namespace LightningArc.Utils.Results.AspNet.Services;

/// <summary>
/// A hosted service that ensures the result mapping services are initialized on application startup.
/// This triggers the construction of mapping services, allowing them to log their mappings.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="EndpointResultInitializerService"/> class.
/// </remarks>
/// <param name="successMappingService">The success mapping service to initialize.</param>
/// <param name="errorMappingService">The error mapping service to initialize.</param>
internal sealed class EndpointResultInitializerService(
    SuccessMappingService successMappingService,
    ErrorMappingService errorMappingService) : IHostedService
{
    // By injecting these services here, we ensure their constructors are called on startup.
    private readonly SuccessMappingService _successMappingService = successMappingService;
    private readonly ErrorMappingService _errorMappingService = errorMappingService;

    /// <summary>
    /// Triggered when the application host is ready to start the service.
    /// </summary>
    /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
    public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    /// <summary>
    /// Triggered when the application host is performing a graceful shutdown.
    /// </summary>
    /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
