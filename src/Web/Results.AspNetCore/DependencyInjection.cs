
global using Microsoft.AspNetCore.Http;
using LightningArc.Results;
using LightningArc.Results.AspNetCore;
using LightningArc.Results.AspNetCore.Interfaces;
using LightningArc.Results.AspNetCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResultsAspNetLocalization = LightningArc.Results.AspNetCore.Localization;
using ResultsLocalization = LightningArc.Results.Localization;

namespace LightningArc.Results.AspNetCore;

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
    /// <param name="defaultCulture">The default culture to use for localizing error messages (e.g., "en-US", "pt-BR"). If not provided, the invariant culture will be used.</param>
    /// <param name="defaultCharset">The default charset to be used for text-based responses. Defaults to "utf-8".</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
    public static IServiceCollection AddEndpointResults(
        this IServiceCollection services,
        bool wrapSuccessResponses = false,
        Func<SuccessDetail, HttpContext, object?>? successResponseBuilder = null,
        Func<Error, HttpContext, object>? errorResponseBuilder = null,
        string? defaultCulture = null,
        string? defaultCharset = null,
        Action<SuccessMappingConfigurator, ErrorMappingConfigurator>? configureMappings = null
    )
    {
        EndpointResultOptions options = new();
        ErrorMappingConfigurator errorConfigurator = new(options);
        SuccessMappingConfigurator successConfigurator = new(options);
        configureMappings?.Invoke(successConfigurator, errorConfigurator);

        options.WrapSuccessResponses = wrapSuccessResponses;
        options.ErrorResponseBuilder = errorResponseBuilder;

        if (options.WrapSuccessResponses)
        {
            options.SuccessResponseBuilder = successResponseBuilder ?? ((success, _) => success);
        }

        if (defaultCharset is not null)
        {
            options.DefaultCharset = defaultCharset;
        }

        // Configure the localization manager for the library
        if (defaultCulture is not null)
        {
            ResultsLocalization.LocalizationManager.Configure(defaultCulture);
            ResultsAspNetLocalization.LocalizationManager.Configure(defaultCulture);
        }

        services.AddSingleton(Options.Create(options));

        services.AddSingleton<SuccessMappingService>();
        services.AddSingleton<ErrorMappingService>();

        services.AddSingleton<IErrorListProvider, DefaultErrorListProvider>();
        services.AddTransient<MarkdownErrorListFormatter>();

        services.AddHostedService<EndpointResultInitializerService>();

        return services;
    }

#if NET8_0_OR_GREATER
    /// <summary>
    /// Registers a global exception handler that converts unhandled exceptions into standardized library <see cref="Error"/> responses.
    /// This method also registers <see cref="ProblemDetailsServiceCollectionExtensions.AddProblemDetails(IServiceCollection)"/> to enable standardized RFC 7807 responses.
    /// </summary>
    /// <remarks>
    /// To use this handler, remember to call <c>app.UseExceptionHandler()</c> or <c>app.UseEndpointExceptionHandler()</c> 
    /// in your middleware pipeline.
    /// </remarks>
    /// <param name="services">The application's service collection.</param>
    /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
    public static IServiceCollection AddEndpointExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<ResultExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
#endif
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

            IErrorListProvider provider = app.Services.GetRequiredService<IErrorListProvider>();
            var errors = provider.GetErrorMetadata();

            IErrorListFormatter listFormatter =
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






