using System.Collections.ObjectModel;
using LightningArc.Utils.Results.AspNet.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LightningArc.Utils.Results.AspNet.Services;

/// <summary>
/// The default implementation of <see cref="IErrorListProvider"/>.
/// It gathers error information from the core <see cref="Error"/> class and combines it with HTTP mapping data.
/// </summary>
internal sealed class DefaultErrorListProvider(IServiceProvider serviceProvider) : IErrorListProvider
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public IEnumerable<ErrorMetadata> GetErrorMetadata()
    {
        var errorModules = Error.GetErrorList();
        var httpMappings = GetHttpMappings();
        var metadataList = new List<ErrorMetadata>();

        foreach (var module in errorModules)
        {
            foreach (var errorInfo in module.Value)
            {
                httpMappings.TryGetValue(errorInfo.Key, out var httpMapping);

                metadataList.Add(new ErrorMetadata
                {
                    Module = module.Key,
                    Code = errorInfo.Value.Code,
                    Name = errorInfo.Value.Name,
                    Message = errorInfo.Value.Message, 
                    HttpStatusCode = httpMapping?.StatusCode
                });
            }
        }

        return metadataList.OrderBy(e => e.Code);
    }

    private ReadOnlyDictionary<Type, ErrorMapping> GetHttpMappings()
    {
        // We resolve the service here to avoid circular dependency issues during startup.
        var mappingService = _serviceProvider.GetService<ErrorMappingService>();
        return mappingService?.Mappings ?? new ReadOnlyDictionary<Type, ErrorMapping>(new Dictionary<Type, ErrorMapping>());
    }
}
