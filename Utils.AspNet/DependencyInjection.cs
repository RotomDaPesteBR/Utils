global using Microsoft.AspNetCore.Http;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using LightningArc.Utils.Results;
using LightningArc.Utils.Results.AspNet;
using LightningArc.Utils.Results.Errors;

namespace LightningArc.Utils.AspNet;

/// <summary>
/// Fornece métodos de extensão para registrar os serviços de mapeamento de resultados de endpoint
/// na coleção de serviços da aplicação.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adiciona os serviços de mapeamento de resultados de endpoint à coleção de serviços.
    /// </summary>
    /// <param name="services">A coleção de serviços da aplicação.</param>
    /// <param name="configure">Uma ação para configurar o serviço de mapeamento de erros, permitindo
    /// a adição de mapeamentos customizados.</param>
    /// <returns>A mesma instância de <see cref="IServiceCollection"/> para encadeamento.</returns>
    public static IServiceCollection AddEndpointResultMappers(
        this IServiceCollection services,
        Action<SuccessMappingConfigurator, ErrorMappingConfigurator>? configure = null
    )
    {
        // Cria uma instância de opções e a configura com a ação do usuário
        var options = new ResultOptions();
        var errorConfigurator = new ErrorMappingConfigurator(options);
        var successConfigurator = new SuccessMappingConfigurator(options);
        configure?.Invoke(successConfigurator, errorConfigurator);

        // Registra as opções configuradas no contêiner de DI
        services.AddSingleton(Options.Create(options));

        services.AddSingleton<SuccessMappingService>();
        services.AddSingleton<ErrorMappingService>();

        return services;
    }
}

/// <summary>
/// Fornece métodos de extensão para configurar a pipeline de requisição do ASP.NET Core.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Força a inicialização do <see cref="SuccessMappingService"/> e do <see cref="ErrorMappingService"/> durante o startup da aplicação.
    /// </summary>
    /// <remarks>
    /// Este método deve ser chamado para garantir que os mapeamentos de erros sejam
    /// registrados imediatamente, sem esperar pela primeira requisição.
    /// </remarks>
    /// <param name="app">O construtor de aplicação.</param>
    public static void UseEndpointResultMappers(this WebApplication app)
    {
        // Força a resolução do serviço singleton, o que dispara a execução da sua fábrica.
        app.Services.GetRequiredService<SuccessMappingService>();

        app.Services.GetRequiredService<ErrorMappingService>();
    }

    /// <summary>
    /// Gera e salva um arquivo markdown contendo a lista de erros e seus códigos.
    /// </summary>
    /// <remarks>
    /// O arquivo é sobrescrito a cada chamada. Este método é útil em ambientes de desenvolvimento
    /// para manter um registro atualizado dos erros definidos na aplicação.
    /// </remarks>
    /// <param name="app">A instância da aplicação web.</param>
    /// <param name="filePath">O caminho completo para o arquivo markdown. O padrão é "ErrorsList.md" na pasta de execução do assembly.</param>
    /// <param name="withHttpMappings">Indica se deve incluir os mapeamentos de status HTTP na lista.</param>
    public static void OutputErrorsList(
        this WebApplication app,
        string? filePath,
        bool withHttpMappings = true
    )
    {
        var logger = app.Services.GetService<ILogger<WebApplication>>();

        string finalPath = string.IsNullOrEmpty(filePath)
            ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ErrorsList.md")
            : filePath;

        var markdownBuilder = new StringBuilder();

        logger?.LogInformation("Iniciando a geração do arquivo de lista de erros.");

        markdownBuilder.AppendLine("# Lista de erros:");
        markdownBuilder.AppendLine();

        // Obtém a lista de erros da biblioteca principal
        Dictionary<string, Dictionary<Type, ErrorInformation>> errorModules = Error.GetErrorList();

        // Tenta obter os mapeamentos HTTP da extensão ASP.NET Core
        ReadOnlyDictionary<Type, ErrorMapping>? httpMappings = null;
        if (withHttpMappings)
        {
            try
            {
                var mappingService = app.Services.GetService<ErrorMappingService>();
                if (mappingService != null)
                {
                    httpMappings = mappingService.Mappings;
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(
                    "Ocorreu um erro ao obter o ErrorMappingService. A lista será gerada sem códigos HTTP. Mensagem: {Message}",
                    ex.Message
                );
            }
        }

        foreach (var module in errorModules)
        {
            // Adiciona o nome do módulo como um cabeçalho Markdown
            markdownBuilder.AppendLine($"## {module.Key}");
            markdownBuilder.AppendLine();

            if (module.Value.Count > 0)
            {
                // Constrói o cabeçalho da tabela dinamicamente
                string tableHeader = "| Código | Erro ";
                string tableSeparator = "|:---|:---";
                if (withHttpMappings)
                {
                    tableHeader += "| Status HTTP ";
                    tableSeparator += "|:---";
                }
                tableHeader += "|";
                tableSeparator += "|";

                markdownBuilder.AppendLine(tableHeader);
                markdownBuilder.AppendLine(tableSeparator);

                foreach (var error in module.Value)
                {
                    string httpStatusCell = "";
                    if (
                        withHttpMappings
                        && httpMappings != null
                        && httpMappings.TryGetValue(error.Key, out var mapping)
                    )
                    {
                        httpStatusCell = $"| `{(int)mapping.StatusCode}` ";
                    }
                    else if (withHttpMappings)
                    {
                        httpStatusCell = "| `N/A` ";
                    }

                    var errorInfo = error.Value;

                    markdownBuilder.AppendLine(
                        $"| `{errorInfo.Code.ToString().PadLeft(5, '0')}` | `{errorInfo.Name}` {httpStatusCell}|"
                    );
                }
            }
            markdownBuilder.AppendLine();
        }

        try
        {
            File.WriteAllText(finalPath, markdownBuilder.ToString());
            logger?.LogInformation("Lista de erros gerada com sucesso em '{FilePath}'", Path.GetFullPath(finalPath));
        }
        catch (Exception ex)
        {
            logger?.LogError(
                "Ocorreu um erro ao tentar salvar o arquivo de lista de erros: {Message}",
                ex.Message
            );
        }
    }
}
