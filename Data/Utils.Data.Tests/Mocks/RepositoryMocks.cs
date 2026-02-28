using System.Data.Common;
using LightningArc.Utils.Data.Abstractions.Mappers;
using LightningArc.Utils.Data.ADO.Factories;
using LightningArc.Utils.Data.ADO.Repositories;

namespace LightningArc.Utils.Data.Tests.Mocks;

public class MockConnectionFactory : IConnectionFactory
{
    public DbConnection GetConnection() => null!;
}

public class MockMapper : IMapper
{
    public object Instance => null!;

    public TDestination Map<TDestination>(object source) => default!;

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination) =>
        default!;
}

public class TestRepository : RepositoryBase, IDbRepository<TestRepository>
{
    public IConnectionFactory? InjectedConnectionFactory => ConnectionFactory;
    public IMapper InjectedMapper => Mapper;

    public TestRepository(IConnectionFactory connectionFactory, IMapper? mapper = null)
        : base(connectionFactory, mapper) { }

    public TestRepository(
        DbConnection connection,
        DbTransaction transaction,
        IMapper? mapper = null
    )
        : base(connection, transaction, mapper) { }

    public static TestRepository Create(
        DbConnection connection,
        DbTransaction transaction,
        IMapper? mapper = null,
        Microsoft.Extensions.Logging.ILogger<TestRepository>? logger = null
    ) => new(connection, transaction, mapper);

    public static TestRepository Create(
        IConnectionFactory connectionFactory,
        IMapper? mapper = null,
        Microsoft.Extensions.Logging.ILogger<TestRepository>? logger = null
    ) => new(connectionFactory, mapper);
}
