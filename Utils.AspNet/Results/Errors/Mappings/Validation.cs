using System.Net;

namespace LightningArc.Utils.Results.AspNet;

public static partial class ErrorMappingServiceExtensions
{
    /// <summary>
    /// Adiciona mapeamentos de erro relacionados a dados e formatos.
    /// </summary>
    /// <remarks>
    /// Inclui erros como <see cref="Error.Validation.InvalidFormatError"/>,
    /// <see cref="Error.Validation.InvalidSchemaError"/>, entre outros.
    /// </remarks>
    /// <param name="service">A instância do serviço de mapeamento de erros.</param>
    /// <returns>A instância do serviço para encadeamento de chamadas.</returns>
    public static ErrorMappingService MapValidation(this ErrorMappingService service)
    {
        // Módulo VALIDATION
        service.Map<Error.Validation.InvalidFormatError>(HttpStatusCode.BadRequest, "Formato de dado inválido", "urn:api-errors:data-invalid-format");
        service.Map<Error.Validation.InvalidSchemaError>(HttpStatusCode.BadRequest, "Esquema de dado inválido", "urn:api-errors:data-invalid-schema");
        service.Map<Error.Validation.DeserializationFailedError>(HttpStatusCode.BadRequest, "Falha na desserialização", "urn:api-errors:data-deserialization-failed");
        service.Map<Error.Validation.MissingFieldError>(HttpStatusCode.BadRequest, "Campo obrigatório ausente", "urn:api-errors:data-missing-field");
        service.Map<Error.Validation.ValueOutOfRangeError>(HttpStatusCode.BadRequest, "Valor fora do intervalo", "urn:api-errors:data-out-of-range");

        return service;
    }
}
