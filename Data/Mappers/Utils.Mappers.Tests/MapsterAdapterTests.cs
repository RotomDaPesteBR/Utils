using LightningArc.Utils.Mappers.Mapster.Adapters;
using Mapster;
using MapsterMapper;

namespace LightningArc.Utils.Mappers.Tests;

public class MapsterAdapterTests
{
    public class Source { public string Name { get; set; } = ""; }
    public class Destination { public string Name { get; set; } = ""; }

    [Fact]
    public void Map_ShouldTransformSourceToDestination()
    {
        // Arrange
        TypeAdapterConfig config = new();
        Mapper mapper = new(config);
        MapsterAdapter adapter = new(mapper);
        Source source = new() { Name = "Test" };

        // Act
        Destination result = adapter.Map<Destination>(source);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(source.Name, result.Name);
    }
}
