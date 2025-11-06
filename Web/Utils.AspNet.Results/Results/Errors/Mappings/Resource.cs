using LightningArc.Utils.Results.AspNet.Localization;
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
        service.Map<Error.Resource.NotFoundError>(HttpStatusCode.NotFound, LocalizationManager.GetErrorTitle("Resource_NotFound"), "urn:api-errors:resource-not-found");
        service.Map<Error.Resource.AlreadyExistsError>(HttpStatusCode.Conflict, LocalizationManager.GetErrorTitle("Resource_AlreadyExists"), "urn:api-errors:resource-already-exists");
        service.Map<Error.Resource.UnavailableError>(HttpStatusCode.ServiceUnavailable, LocalizationManager.GetErrorTitle("Resource_Unavailable"), "urn:api-errors:resource-unavailable");
        service.Map<Error.Resource.InvalidStateError>(HttpStatusCode.Conflict, LocalizationManager.GetErrorTitle("Resource_InvalidState"), "urn:api-errors:resource-invalid-state");
        service.Map<Error.Resource.ObsoleteError>(HttpStatusCode.Gone, LocalizationManager.GetErrorTitle("Resource_Obsolete"), "urn:api-errors:resource-obsolete");


        return service;
    }
}
