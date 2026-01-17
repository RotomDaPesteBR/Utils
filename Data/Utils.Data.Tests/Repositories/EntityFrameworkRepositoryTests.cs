using Microsoft.EntityFrameworkCore;
using LightningArc.Utils.Data.EntityFramework.Repositories;

namespace LightningArc.Utils.Data.Tests.Repositories;

public class EntityFrameworkRepositoryTests
{
    public class TestEntity { public int Id { get; set; } }
    
    public class TestDbContext(DbContextOptions<TestDbContext> options) : DbContext(options)
    {
        public DbSet<TestEntity> TestEntities { get; set; } = null!;
    }

    public class TestRepository(TestDbContext context) : RepositoryBase<TestDbContext, TestEntity>(context)
    {
        public DbSet<TestEntity> GetDbSet() => DbSet;
    }

    [Fact]
    public void Repository_ShouldInitializeDbSet()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        using TestDbContext context = new(options);
        TestRepository repository = new(context);

        // Act
        var dbSet = repository.GetDbSet();

        // Assert
        Assert.NotNull(dbSet);
    }
}
