using System.Net;

namespace LightningArc.Utils.Results.AspNet;

public static partial class ErrorMappingServiceExtensions
{
    /// <summary>
    /// Adiciona mapeamentos de erro relacionados à autenticação.
    /// </summary>
    /// <remarks>
    /// Inclui erros como <see cref="Error.Authentication.UnauthorizedError"/>,
    /// <see cref="Error.Authentication.ForbiddenError"/>, entre outros.
    /// </remarks>
    /// <param name="service">A instância do serviço de mapeamento de erros.</param>
    /// <returns>A instância do serviço para encadeamento de chamadas.</returns>
    public static ErrorMappingService MapAuthentication(this ErrorMappingService service)
    {
        // Módulo AUTHENTICATION (Prefixo 4)
        service.Map<Error.Authentication.UnauthorizedError>(HttpStatusCode.Unauthorized, "Falha de autenticação", "urn:api-errors:auth-failed");
        service.Map<Error.Authentication.ForbiddenError>(HttpStatusCode.Forbidden, "Acesso proibido", "urn:api-errors:auth-forbidden");
        service.Map<Error.Authentication.TokenExpiredError>(HttpStatusCode.Unauthorized, "Token expirado", "urn:api-errors:auth-token-expired");
        service.Map<Error.Authentication.InvalidCredentialsError>(HttpStatusCode.Unauthorized, "Credenciais inválidas", "urn:api-errors:auth-invalid-credentials");
        service.Map<Error.Authentication.InactiveAccountError>(HttpStatusCode.Forbidden, "Conta inativa", "urn:api-errors:auth-inactive-account");
        service.Map<Error.Authentication.ExpiredSessionError>(HttpStatusCode.Unauthorized, "Sessão expirada", "urn:api-errors:auth-session-expired");


        return service;
    }
}
