namespace LightningArc.Utils.Results.AspNet;

/// <summary>
/// Fornece métodos de extensão para o <see cref="ErrorMappingService"/>
/// para agrupar e adicionar mapeamentos de erro de forma modular.
/// </summary>
public static partial class ErrorMappingServiceExtensions
{
    /// <summary>
    /// Adiciona todos os mapeamentos de erro padrão da biblioteca.
    /// </summary>
    /// <param name="service">A instância do serviço de mapeamento de erros.</param>
    /// <returns>A instância do serviço para encadeamento.</returns>
    public static ErrorMappingService MapDefaultErrors(this ErrorMappingService service)
    {
        return service
            .MapApplication()
            .MapSystem()
            .MapDatabase()
            .MapValidation()
            .MapResource()
            .MapAuthentication()
            .MapRequest()
            .MapExternal()
            .MapNetwork()
            .MapConcurrency()
            .MapIO();
    }
}
