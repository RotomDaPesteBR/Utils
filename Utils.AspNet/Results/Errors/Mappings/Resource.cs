using System.Net;

namespace LightningArc.Utils.Results.AspNet;

public static partial class ErrorMappingServiceExtensions
{
    /// <summary>
    /// Adiciona mapeamentos de erro relacionados a recursos.
    /// </summary>
    /// <remarks>
    /// Inclui erros como <see cref="Error.Resource.NotFoundError"/>,
    /// <see cref="Error.Resource.AlreadyExistsError"/>, entre outros.
    /// </remarks>
    /// <param name="service">A instância do serviço de mapeamento de erros.</param>
    /// <returns>A instância do serviço para encadeamento de chamadas.</returns>
    public static ErrorMappingService MapResource(this ErrorMappingService service)
    {
        // Módulo RESOURCE (Prefixo 3)
        service.Map<Error.Resource.NotFoundError>(HttpStatusCode.NotFound, "Recurso não encontrado", "urn:api-errors:resource-not-found");
        service.Map<Error.Resource.AlreadyExistsError>(HttpStatusCode.Conflict, "Recurso já existe", "urn:api-errors:resource-already-exists");
        service.Map<Error.Resource.UnavailableError>(HttpStatusCode.ServiceUnavailable, "Recurso indisponível", "urn:api-errors:resource-unavailable");
        service.Map<Error.Resource.InvalidStateError>(HttpStatusCode.Conflict, "Estado do recurso inválido", "urn:api-errors:resource-invalid-state");
        service.Map<Error.Resource.ObsoleteError>(HttpStatusCode.Gone, "Recurso obsoleto", "urn:api-errors:resource-obsolete");


        return service;
    }
}
