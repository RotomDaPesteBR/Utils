using LightningArc.Utils.Results.AspNet.Localization;
using System.Net;

namespace LightningArc.Utils.Results.AspNet;

public static partial class ErrorMappingServiceExtensions
{
    /// <summary>
    /// Adiciona mapeamentos de erro relacionados a serviços externos.
    /// </summary>
    /// <remarks>
    /// Inclui erros como <see cref="Error.External.RateLimitExceededError"/>,
    /// <see cref="Error.External.ApiQuotaExceededError"/>, entre outros.
    /// </remarks>
    /// <param name="service">A instância do serviço de mapeamento de erros.</param>
    /// <returns>A instância do serviço para encadeamento de chamadas.</returns>
    public static ErrorMappingService MapExternal(this ErrorMappingService service)
    {
        // Módulo EXTERNAL (Prefixo 8)
        service.Map<Error.External.RateLimitExceededError>(HttpStatusCode.TooManyRequests, LocalizationManager.GetErrorTitle("External_RateLimitExceeded"), "urn:api-errors:external-rate-limit");
        service.Map<Error.External.ApiQuotaExceededError>(HttpStatusCode.TooManyRequests, LocalizationManager.GetErrorTitle("External_ApiQuotaExceeded"), "urn:api-errors:external-quota-exceeded");
        service.Map<Error.External.InvalidApiResponseError>(HttpStatusCode.BadGateway, LocalizationManager.GetErrorTitle("External_InvalidApiResponse"), "urn:api-errors:external-invalid-api-response");
        service.Map<Error.External.ServiceUnavailableError>(HttpStatusCode.ServiceUnavailable, LocalizationManager.GetErrorTitle("External_ServiceUnavailable"), "urn:api-errors:external-service-unavailable");
        service.Map<Error.External.TimeoutError>(HttpStatusCode.GatewayTimeout, LocalizationManager.GetErrorTitle("External_Timeout"), "urn:api-errors:external-timeout");
        service.Map<Error.External.CommunicationError>(HttpStatusCode.BadGateway, LocalizationManager.GetErrorTitle("External_Communication"), "urn:api-errors:external-communication-failed");

        return service;
    }
}
