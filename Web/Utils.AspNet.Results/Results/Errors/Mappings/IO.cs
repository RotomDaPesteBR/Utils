using System.Net;

namespace LightningArc.Utils.Results.AspNet;

public static partial class ErrorMappingServiceExtensions
{
    /// <summary>
    /// Adiciona mapeamentos de erro relacionados a operações de I/O (entrada e saída).
    /// </summary>
    /// <remarks>
    /// Inclui erros como <see cref="Error.IO.FileNotFoundError"/>,
    /// <see cref="Error.IO.PermissionDeniedError"/>, entre outros.
    /// </remarks>
    /// <param name="service">A instância do serviço de mapeamento de erros.</param>
    /// <returns>A instância do serviço para encadeamento de chamadas.</returns>
    public static ErrorMappingService MapIO(this ErrorMappingService service)
    {
        // Módulo IO
        service.Map<Error.IO.FileNotFoundError>(HttpStatusCode.NotFound, "Arquivo não encontrado", "urn:api-errors:io-file-not-found");
        service.Map<Error.IO.DirectoryNotFoundError>(HttpStatusCode.NotFound, "Diretório não encontrado", "urn:api-errors:io-directory-not-found");
        service.Map<Error.IO.PermissionDeniedError>(HttpStatusCode.InternalServerError, "Permissão negada (servidor)", "urn:api-errors:io-permission-denied");
        service.Map<Error.IO.DiskFullError>(HttpStatusCode.InsufficientStorage, "Disco cheio", "urn:api-errors:io-disk-full");
        service.Map<Error.IO.CorruptedFileError>(HttpStatusCode.InternalServerError, "Arquivo corrompido", "urn:api-errors:io-corrupted-file");

        return service;
    }
}
