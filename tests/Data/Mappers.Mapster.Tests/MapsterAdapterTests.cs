using LightningArc.Mappers.Mapster.Adapters;
using Mapster;
using MapsterMapper;

namespace LightningArc.Mappers.Tests;

public class MapsterAdapterTests
{
    public class Source { public string Name { get; set; } = ""; }
    public class Destination { public string Name { get; set; } = ""; }

    [Test]
    public async Task Map_ShouldTransformSourceToDestination()
    {
        // Arrange
        TypeAdapterConfig config = new();
        Mapper mapper = new(config);
        MapsterAdapter adapter = new(mapper);
        Source source = new() { Name = "Test" };

        // Act
        Destination result = adapter.Map<Destination>(source);

        // Assert
        await Assert.That(result).IsNotNull();
        await Assert.That(result.Name).IsEqualTo(source.Name);
    }
}

