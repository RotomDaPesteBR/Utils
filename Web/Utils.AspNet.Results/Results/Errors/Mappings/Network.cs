using System.Net;

namespace LightningArc.Utils.Results.AspNet;

public static partial class ErrorMappingServiceExtensions
{
    /// <summary>
    /// Adiciona mapeamentos de erro relacionados à rede.
    /// </summary>
    /// <remarks>
    /// Inclui erros como <see cref="Error.Network.ConnectionFailedError"/>,
    /// <see cref="Error.Network.RequestTimeoutError"/>, entre outros.
    /// </remarks>
    /// <param name="service">A instância do serviço de mapeamento de erros.</param>
    /// <returns>A instância do serviço para encadeamento de chamadas.</returns>
    public static ErrorMappingService MapNetwork(this ErrorMappingService service)
    {
        // Módulo NETWORK
        service.Map<Error.Network.ConnectionFailedError>(HttpStatusCode.ServiceUnavailable, "Falha na conexão de rede", "urn:api-errors:net-connection-failed");
        service.Map<Error.Network.RequestTimeoutError>(HttpStatusCode.GatewayTimeout, "Tempo limite da requisição", "urn:api-errors:net-request-timeout");
        service.Map<Error.Network.ServiceUnavailableError>(HttpStatusCode.ServiceUnavailable, "Serviço de rede indisponível", "urn:api-errors:net-service-unavailable");
        service.Map<Error.Network.DnsFailureError>(HttpStatusCode.ServiceUnavailable, "Falha de DNS", "urn:api-errors:net-dns-failure");
        service.Map<Error.Network.SslHandshakeFailedError>(HttpStatusCode.ServiceUnavailable, "Falha no handshake SSL", "urn:api-errors:net-ssl-handshake-failed");
        service.Map<Error.Network.ProxyFailureError>(HttpStatusCode.BadGateway, "Falha no proxy", "urn:api-errors:net-proxy-failure");


        return service;
    }
}
