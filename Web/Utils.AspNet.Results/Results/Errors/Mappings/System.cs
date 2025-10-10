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
        service.Map<Error.System.ConfigurationError>(HttpStatusCode.InternalServerError, "Erro de configuração", "urn:api-errors:sys-configuration-error");
        service.Map<Error.System.DependencyNotRegisteredError>(HttpStatusCode.InternalServerError, "Dependência não registrada", "urn:api-errors:sys-dependency-not-registered");
        service.Map<Error.System.SystemMaintenanceError>(HttpStatusCode.ServiceUnavailable, "Manutenção do sistema", "urn:api-errors:sys-maintenance");
        service.Map<Error.System.OutOfMemoryError>(HttpStatusCode.InternalServerError, "Memória insuficiente", "urn:api-errors:sys-out-of-memory");
        service.Map<Error.System.ThreadAbortedError>(HttpStatusCode.InternalServerError, "Execução da thread abortada", "urn:api-errors:sys-thread-aborted");

        return service;
    }
}
