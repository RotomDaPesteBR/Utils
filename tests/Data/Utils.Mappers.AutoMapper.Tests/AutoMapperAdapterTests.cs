using AutoMapper;
using LightningArc.Utils.Mappers.AutoMapper.Adapters;
using Microsoft.Extensions.Logging.Abstractions;

namespace LightningArc.Utils.Mappers.Tests;

public class AutoMapperAdapterTests
{
    public class Source { public string Name { get; set; } = ""; }
    public class Destination { public string Name { get; set; } = ""; }

    [Test]
    public async Task Map_ShouldTransformSourceToDestination()
    {
        // Arrange
        MapperConfigurationExpression configExpression = new();
        configExpression.CreateMap<Source, Destination>();
        
        MapperConfiguration config = new(configExpression, NullLoggerFactory.Instance);
        IMapper? mapper = config.CreateMapper();
        AutoMapperAdapter adapter = new(mapper);
        Source source = new() { Name = "Test" };

        // Act
        Destination result = adapter.Map<Destination>(source);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result.Name).IsEqualTo(source.Name);
    }
}
