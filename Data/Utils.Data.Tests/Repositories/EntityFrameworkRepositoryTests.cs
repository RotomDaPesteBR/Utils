using Microsoft.EntityFrameworkCore;
using LightningArc.Utils.Data.EntityFramework.Repositories;

namespace LightningArc.Utils.Data.Tests.Repositories;

public class EntityFrameworkRepositoryTests
{
    public class TestEntity { public int Id { get; set; } }
    
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }
        public DbSet<TestEntity> TestEntities { get; set; } = null!;
    }

    public class TestRepository : RepositoryBase<TestDbContext, TestEntity>
    {
        public TestRepository(TestDbContext context) : base(context) { }
        
        public DbSet<TestEntity> GetDbSet() => DbSet;
    }

    [Fact]
    public void Repository_ShouldInitializeDbSet()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        using var context = new TestDbContext(options);
        var repository = new TestRepository(context);

        // Act
        var dbSet = repository.GetDbSet();

        // Assert
        Assert.NotNull(dbSet);
    }
}
