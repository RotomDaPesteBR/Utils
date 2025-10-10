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
        service.Map<Error.Database.ConnectionFailedError>(HttpStatusCode.InternalServerError, "Falha na conexão com o banco de dados", "urn:api-errors:db-connection-failed");
        service.Map<Error.Database.QueryExecutionFailedError>(HttpStatusCode.InternalServerError, "Falha na execução da consulta", "urn:api-errors:db-query-failed");
        service.Map<Error.Database.ConstraintViolationError>(HttpStatusCode.Conflict, "Violação de restrição no banco de dados", "urn:api-errors:db-constraint-violation");
        service.Map<Error.Database.TransientError>(HttpStatusCode.ServiceUnavailable, "Erro temporário no banco de dados", "urn:api-errors:db-transient-error");
        service.Map<Error.Database.DeadlockError>(HttpStatusCode.ServiceUnavailable, "Deadlock no banco de dados", "urn:api-errors:db-deadlock");

        return service;
    }
}
