using LightningArc.Utils.Results.AspNet.Localization;
using System.Net;

namespace LightningArc.Utils.Results.AspNet;

public static partial class ErrorMappingServiceExtensions
{
    /// <summary>
    /// Adiciona mapeamentos de erro relacionados ao sistema.
    /// </summary>
    /// <remarks>
    /// Inclui erros como <see cref="Error.System.ConfigurationError"/>,
    /// <see cref="Error.System.DependencyNotRegisteredError"/>, entre outros.
    /// </remarks>
    /// <param name="service">A instância do serviço de mapeamento de erros.</param>
    /// <returns>A instância do serviço para encadeamento de chamadas.</returns>
    public static ErrorMappingService MapSystem(this ErrorMappingService service)
    {
        // Módulo SYSTEM (Prefixo 10)
        service.Map<Error.System.ConfigurationError>(HttpStatusCode.InternalServerError, LocalizationManager.GetErrorTitle("System_Configuration"), "urn:api-errors:sys-configuration-error");
        service.Map<Error.System.DependencyNotRegisteredError>(HttpStatusCode.InternalServerError, LocalizationManager.GetErrorTitle("System_DependencyNotRegistered"), "urn:api-errors:sys-dependency-not-registered");
        service.Map<Error.System.SystemMaintenanceError>(HttpStatusCode.ServiceUnavailable, LocalizationManager.GetErrorTitle("System_SystemMaintenance"), "urn:api-errors:sys-maintenance");
        service.Map<Error.System.OutOfMemoryError>(HttpStatusCode.InternalServerError, LocalizationManager.GetErrorTitle("System_OutOfMemory"), "urn:api-errors:sys-out-of-memory");
        service.Map<Error.System.ThreadAbortedError>(HttpStatusCode.InternalServerError, LocalizationManager.GetErrorTitle("System_ThreadAborted"), "urn:api-errors:sys-thread-aborted");

        return service;
    }
}
