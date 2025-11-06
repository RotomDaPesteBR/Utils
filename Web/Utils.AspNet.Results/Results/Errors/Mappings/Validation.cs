using LightningArc.Utils.Results.AspNet.Localization;
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
        service.Map<Error.Validation.InvalidFormatError>(HttpStatusCode.BadRequest, LocalizationManager.GetErrorTitle("Validation_InvalidFormat"), "urn:api-errors:data-invalid-format");
        service.Map<Error.Validation.InvalidSchemaError>(HttpStatusCode.BadRequest, LocalizationManager.GetErrorTitle("Validation_InvalidSchema"), "urn:api-errors:data-invalid-schema");
        service.Map<Error.Validation.DeserializationFailedError>(HttpStatusCode.BadRequest, LocalizationManager.GetErrorTitle("Validation_DeserializationFailed"), "urn:api-errors:data-deserialization-failed");
        service.Map<Error.Validation.MissingFieldError>(HttpStatusCode.BadRequest, LocalizationManager.GetErrorTitle("Validation_MissingField"), "urn:api-errors:data-missing-field");
        service.Map<Error.Validation.ValueOutOfRangeError>(HttpStatusCode.BadRequest, LocalizationManager.GetErrorTitle("Validation_ValueOutOfRange"), "urn:api-errors:data-out-of-range");

        return service;
    }
}
