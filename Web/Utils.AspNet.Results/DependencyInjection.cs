global using Microsoft.AspNetCore.Http;
using System.Collections.ObjectModel;
using System.IO;
using LightningArc.Utils.Results;
using LightningArc.Utils.Results.AspNet;
using LightningArc.Utils.Results.AspNet.Interfaces;
using LightningArc.Utils.Results.AspNet.Services;
using LightningArc.Utils.Results.Errors;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Utils.AspNet.Results.Results.Success;

namespace LightningArc.Utils.AspNet;

/// <summary>
/// Provides extension methods for registering the endpoint results feature
/// in the application's service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <param name="services">The application's service collection.</param>
    /// <param name="wrapSuccessResponses">If <c>true</c>, wraps all success responses in a standard object defined by <paramref name="successResponseBuilder"/>. If <c>false</c>, returns the value directly. Defaults to <c>false</c>.</param>
    /// <param name="configureMappings">An action to configure custom success and error mappings to HTTP status codes.</param>
    /// <param name="successResponseBuilder">A function to build a custom success response object. If null, a default format will be used.</param>
    /// <param name="errorResponseBuilder">A function to build a custom error response object. If null, a default RFC 7807 ProblemDetails format will be used.</param>
    /// <param name="defaultCharset">The default charset to be used for text-based responses. Defaults to "utf-8".</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
    public static IServiceCollection AddEndpointResults(
        this IServiceCollection services,
        bool wrapSuccessResponses = false,
        Func<SuccessDetail, object?>? successResponseBuilder = null,
        Func<Error, HttpContext, object>? errorResponseBuilder = null,
        Action<SuccessMappingConfigurator, ErrorMappingConfigurator>? configureMappings = null,
        string? defaultCharset = null
    )
    {
        var options = new EndpointResultOptions();
        var errorConfigurator = new ErrorMappingConfigurator(options);
        var successConfigurator = new SuccessMappingConfigurator(options);
        configureMappings?.Invoke(successConfigurator, errorConfigurator);

        options.WrapSuccessResponses = wrapSuccessResponses;
        options.ErrorResponseBuilder = errorResponseBuilder;

        if (options.WrapSuccessResponses)
        {
            options.SuccessResponseBuilder =
                successResponseBuilder
                ?? (
                    success => new
                    {
                        success.Status,
                        success.Message,
                        success.Data,
                    }
                );
        }

        if (defaultCharset is not null)
        {
            options.DefaultCharset = defaultCharset;
        }

        services.AddSingleton(Options.Create(options));

        services.AddSingleton<SuccessMappingService>();
        services.AddSingleton<ErrorMappingService>();

        services.AddSingleton<IErrorListProvider, DefaultErrorListProvider>();
        services.AddTransient<MarkdownErrorListFormatter>();

        services.AddHostedService<EndpointResultInitializerService>();

        return services;
    }
}

/// <summary>
/// Provides extension methods for configuring the ASP.NET Core request pipeline.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Generates and saves a markdown file containing the list of defined errors and their codes.
    /// </summary>
    /// <remarks>
    /// The file is overwritten on each call. This method is useful in development environments
    /// to maintain an up-to-date record of the errors defined in the application.
    /// </remarks>
    /// <param name="app">The web application instance.</param>
    /// <param name="filePath">The full path for the markdown file. Defaults to "ErrorsList.md" in the assembly's execution folder.</param>
    /// <param name="formatter">An optional custom formatter. If not provided, the default <see cref="MarkdownErrorListFormatter"/> will be used.</param>
    public static void OutputErrorsList(
        this WebApplication app,
        string? filePath = null,
        IErrorListFormatter? formatter = null
    )
    {
        var logger = app.Services.GetRequiredService<ILogger<WebApplication>>();

        try
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Starting generation of the error list file.");
            }

            var provider = app.Services.GetRequiredService<IErrorListProvider>();
            var errors = provider.GetErrorMetadata();

            var listFormatter =
                formatter ?? app.Services.GetRequiredService<MarkdownErrorListFormatter>();
            string formattedContent = listFormatter.Format(errors);

            string finalPath = string.IsNullOrEmpty(filePath)
                ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ErrorsList.md")
                : filePath;

            File.WriteAllText(finalPath, formattedContent);

            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation(
                    "Error list successfully generated at '{FilePath}'",
                    Path.GetFullPath(finalPath)
                );
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while generating the error list file.");
        }
    }
}
