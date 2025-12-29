using LightningArc.Utils.Data.ADO.Factories;
using LightningArc.Utils.Data.Tests.Mocks;

namespace LightningArc.Utils.Data.Tests.Factories;

public class RepositoryFactoryTests
{
    private readonly MockConnectionFactory _connectionFactory = new();
    private readonly MockMapper _mapper = new();

    [Fact]
    public void Create_ShouldReturnRepositoryWithNullMapper_WhenNoMapperProvidedToFactory()
    {
        // Arrange
        var factory = new RepositoryFactory(_connectionFactory);

        // Act
        var repository = factory.Create<TestRepository>();

        // Assert
        Assert.NotNull(repository);
        Assert.Null(repository.InjectedMapper);
    }

    [Fact]
    public void Create_ShouldReturnRepositoryWithMapper_WhenMapperIsProvidedToFactory()
    {
        // Arrange
        var factory = new RepositoryFactory(_connectionFactory, _mapper);

        // Act
        var repository = factory.Create<TestRepository>();

        // Assert
        Assert.NotNull(repository);
        Assert.Equal(_mapper, repository.InjectedMapper);
    }
}
