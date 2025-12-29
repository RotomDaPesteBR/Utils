using AutoMapper;
using LightningArc.Utils.Mappers.AutoMapper.Adapters;
using Microsoft.Extensions.Logging.Abstractions;

namespace LightningArc.Utils.Mappers.Tests;

public class AutoMapperAdapterTests
{
    public class Source { public string Name { get; set; } = ""; }
    public class Destination { public string Name { get; set; } = ""; }

    [Fact]
    public void Map_ShouldTransformSourceToDestination()
    {
        // Arrange
        var configExpression = new MapperConfigurationExpression();
        configExpression.CreateMap<Source, Destination>();
        
        var config = new MapperConfiguration(configExpression, NullLoggerFactory.Instance);
        var mapper = config.CreateMapper();
        var adapter = new AutoMapperAdapter(mapper);
        var source = new Source { Name = "Test" };

        // Act
        var result = adapter.Map<Destination>(source);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(source.Name, result.Name);
    }
}