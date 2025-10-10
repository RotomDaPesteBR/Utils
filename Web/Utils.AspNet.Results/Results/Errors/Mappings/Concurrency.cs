using System.Net;

namespace LightningArc.Utils.Results.AspNet;

public static partial class ErrorMappingServiceExtensions
{
    /// <summary>
    /// Adiciona mapeamentos de erro relacionados à concorrência.
    /// </summary>
    /// <remarks>
    /// Inclui erros como <see cref="Error.Concurrency.ConflictError"/>,
    /// <see cref="Error.Concurrency.LockedError"/>, entre outros.
    /// </remarks>
    /// <param name="service">A instância do serviço de mapeamento de erros.</param>
    /// <returns>A instância do serviço para encadeamento de chamadas.</returns>
    public static ErrorMappingService MapConcurrency(this ErrorMappingService service)
    {
        // Módulo CONCURRENCY (Prefixo 9)
        service.Map<Error.Concurrency.ConflictError>(HttpStatusCode.Conflict, "Conflito de concorrência", "urn:api-errors:concurrency-conflict");
        service.Map<Error.Concurrency.LockedError>(HttpStatusCode.Locked, "Recurso bloqueado", "urn:api-errors:concurrency-locked");
        service.Map<Error.Concurrency.StaleDataError>(HttpStatusCode.Conflict, "Dados desatualizados", "urn:api-errors:concurrency-stale-data");
        service.Map<Error.Concurrency.ResourceInUseError>(HttpStatusCode.Locked, "Recurso em uso", "urn:api-errors:concurrency-resource-in-use");

        return service;
    }
}
