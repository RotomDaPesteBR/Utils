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
        var config = new TypeAdapterConfig();
        var mapper = new Mapper(config);
        var adapter = new MapsterAdapter(mapper);
        var source = new Source { Name = "Test" };

        // Act
        var result = adapter.Map<Destination>(source);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(source.Name, result.Name);
    }
}
