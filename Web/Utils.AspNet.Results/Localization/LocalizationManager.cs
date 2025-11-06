using System.Globalization;
using System.Resources;

namespace LightningArc.Utils.Results.AspNet.Localization;

/// <summary>
/// Manages localization for error titles within the ASP.NET Results library.
/// This class can be optionally configured by the consumer to set a specific culture
/// and resource manager for error title lookup.
/// </summary>
public static class LocalizationManager
{
    private static CultureInfo _currentCulture = CultureInfo.InvariantCulture;
    private static ResourceManager _errorResourceManager = new(
        "LightningArc.Utils.Results.AspNet.Resources.ErrorTitles",
        typeof(LocalizationManager).Assembly
    );

    /// <summary>
    /// Gets the currently configured culture for titles.
    /// Defaults to <see cref="CultureInfo.InvariantCulture"/> if not explicitly configured.
    /// </summary>
    public static CultureInfo CurrentCulture => _currentCulture;

    /// <summary>
    /// Configures the localization settings for titles.
    /// This method should be called once at application startup if custom localization is desired.
    /// </summary>
    /// <param name="cultureName">The name of the culture to use (e.g., "en-US", "pt-BR").</param>
    /// <param name="errorResourceManager">Optional. A custom <see cref="ResourceManager"/> to use for error title lookup.
    /// If not provided, the library's default error resource manager will be used.</param>
    public static void Configure(
        string cultureName,
        ResourceManager? errorResourceManager = null
    )
    {
        _currentCulture = new CultureInfo(cultureName);
        if (errorResourceManager != null)
        {
            _errorResourceManager = errorResourceManager;
        }
    }

    internal static string GetErrorTitle(string key)
    {
        return _errorResourceManager.GetString(key, _currentCulture) ?? key;
    }
}
