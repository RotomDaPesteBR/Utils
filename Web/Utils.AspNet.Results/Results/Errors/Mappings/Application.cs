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
    public static ErrorMappingService MapApplication(this ErrorMappingService service)
    {
        // Módulo APPLICATION (Prefixo 1)
        service.Map<Error.Application.InternalError>(HttpStatusCode.InternalServerError, "Erro interno", "urn:api-errors:internal");
        service.Map<Error.Application.InvalidParameterError>(HttpStatusCode.BadRequest, "Parâmetro inválido", "urn:api-errors:invalid-parameter");
        service.Map<Error.Application.InvalidOperationError>(HttpStatusCode.BadRequest, "Operação inválida", "urn:api-errors:invalid-operation");
        service.Map<Error.Application.TaskCanceledError>(HttpStatusCode.Conflict, "Operação cancelada", "urn:api-errors:task-canceled");
        service.Map<Error.Application.NotImplementedError>(HttpStatusCode.NotImplemented, "Não implementado", "urn:api-errors:not-implemented");

        return service;
    }
}
