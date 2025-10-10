using System.Net;

namespace LightningArc.Utils.Results.AspNet;

public static partial class ErrorMappingServiceExtensions
{
    /// <summary>
    /// Adiciona mapeamentos de erro relacionados ao domínio da aplicação.
    /// </summary>
    /// <remarks>
    /// Inclui erros como <see cref="Error.Application.InternalError"/>,
    /// <see cref="Error.Application.InvalidParameterError"/>, entre outros.
    /// </remarks>
    /// <param name="service">A instância do serviço de mapeamento de erros.</param>
    /// <returns>A instância do serviço para encadeamento de chamadas.</returns>
    /// <summary>
    /// Adiciona mapeamentos de erro relacionados à requisição HTTP.
    /// </summary>
    public static ErrorMappingService MapRequest(this ErrorMappingService service)
    {
        // Módulo REQUEST (Prefixo 11)
        service.Map<Error.Request.InvalidRequestError>(HttpStatusCode.BadRequest, "Requisição inválida", "urn:api-errors:req-invalid");
        service.Map<Error.Request.TooLargeRequestError>(HttpStatusCode.RequestEntityTooLarge, "Conteúdo da requisição muito grande", "urn:api-errors:req-too-large");
        service.Map<Error.Request.TooManyRequestsError>(HttpStatusCode.TooManyRequests, "Excesso de requisições", "urn:api-errors:req-too-many-requests");
        service.Map<Error.Request.NotAcceptableError>(HttpStatusCode.NotAcceptable, "Inaceitável", "urn:api-errors:req-not-acceptable");
        service.Map<Error.Request.UnsupportedMediaTypeError>(HttpStatusCode.UnsupportedMediaType, "Tipo de mídia não suportado", "urn:api-errors:req-unsupported-media-type");

        return service;
    }
}
