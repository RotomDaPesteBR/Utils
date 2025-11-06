using LightningArc.Utils.Results.AspNet.Localization;
using System.Net;

namespace LightningArc.Utils.Results.AspNet;

public static partial class ErrorMappingServiceExtensions
{
    /// <summary>
    /// Adiciona mapeamentos de erro relacionados ao banco de dados.
    /// </summary>
    /// <remarks>
    /// Inclui erros como <see cref="Error.Database.ConnectionFailedError"/>,
    /// <see cref="Error.Database.QueryExecutionFailedError"/>, entre outros.
    /// </remarks>
    /// <param name="service">A instância do serviço de mapeamento de erros.</param>
    /// <returns>A instância do serviço para encadeamento de chamadas.</returns>
    public static ErrorMappingService MapDatabase(this ErrorMappingService service)
    {
        // Módulo DATABASE
        service.Map<Error.Database.ConnectionFailedError>(HttpStatusCode.InternalServerError, LocalizationManager.GetErrorTitle("Database_ConnectionFailed"), "urn:api-errors:db-connection-failed");
        service.Map<Error.Database.QueryExecutionFailedError>(HttpStatusCode.InternalServerError, LocalizationManager.GetErrorTitle("Database_QueryExecutionFailed"), "urn:api-errors:db-query-failed");
        service.Map<Error.Database.ConstraintViolationError>(HttpStatusCode.Conflict, LocalizationManager.GetErrorTitle("Database_ConstraintViolation"), "urn:api-errors:db-constraint-violation");
        service.Map<Error.Database.TransientError>(HttpStatusCode.ServiceUnavailable, LocalizationManager.GetErrorTitle("Database_Transient"), "urn:api-errors:db-transient-error");
        service.Map<Error.Database.DeadlockError>(HttpStatusCode.ServiceUnavailable, LocalizationManager.GetErrorTitle("Database_Deadlock"), "urn:api-errors:db-deadlock");

        return service;
    }
}
