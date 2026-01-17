using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using LightningArc.Utils.Data.ADO.Factories;
using LightningArc.Utils.Data.ADO.Repositories;
using LightningArc.Utils.Data.Tests.Mocks;

namespace LightningArc.Utils.Data.Tests.Repositories;

public class RepositoryBaseTests
{
    private class TestDbConnection : DbConnection
    {
        public int CloseCount { get; private set; }
        public int DisposeCount { get; private set; }

        [AllowNull]
        public override string ConnectionString { get; set; } = "";
        public override string Database => "";
        public override string DataSource => "";
        public override string ServerVersion => "";
        public override ConnectionState State => ConnectionState.Open;

        public override void ChangeDatabase(string databaseName) { }

        public override void Close() => CloseCount++;

        public override void Open() { }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel) => null!;

        protected override DbCommand CreateDbCommand() => null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                DisposeCount++;
            base.Dispose(disposing);
        }
    }

    private class ConnectionFactoryStub(DbConnection connection) : IConnectionFactory
    {
        public DbConnection GetConnection() => connection;
    }

    private class InternalTestRepository : RepositoryBase
    {
        public InternalTestRepository(IConnectionFactory factory)
            : base(factory) { }

        public InternalTestRepository(DbConnection conn, DbTransaction trans)
            : base(conn, trans) { }

        public new DbConnection GetConnection() => base.GetConnection();

        public new void ReleaseConnection(DbConnection conn) => base.ReleaseConnection(conn);
    }

    [Fact]
    public void GetConnection_ShouldReturnFactoryConnection_WhenNoConnectionProvided()
    {
        // Arrange
        TestDbConnection mockConn = new();
        ConnectionFactoryStub factory = new(mockConn);
        InternalTestRepository repo = new(factory);

        // Act
        DbConnection conn = repo.GetConnection();

        // Assert
        Assert.Equal(mockConn, conn);
    }

    [Fact]
    public void GetConnection_ShouldReturnProvidedConnection_WhenConnectionProvidedInConstructor()
    {
        // Arrange
        TestDbConnection mockConn = new();
        InternalTestRepository repo = new(mockConn, null!);

        // Act
        DbConnection conn = repo.GetConnection();

        // Assert
        Assert.Equal(mockConn, conn);
    }

    [Fact]
    public void ReleaseConnection_ShouldNotClose_WhenConnectionWasProvidedInConstructor()
    {
        // Arrange
        TestDbConnection mockConn = new();
        InternalTestRepository repo = new(mockConn, null!);

        // Act
        repo.ReleaseConnection(mockConn);

        // Assert
        Assert.Equal(0, mockConn.CloseCount);
    }

    [Fact]
    public void ReleaseConnection_ShouldCloseAndDispose_WhenConnectionWasCreatedLocally()
    {
        // Arrange
        TestDbConnection mockConn = new();
        ConnectionFactoryStub factory = new(mockConn);
        InternalTestRepository repo = new(factory);

        // Act
        repo.ReleaseConnection(mockConn);

        // Assert
        Assert.Equal(1, mockConn.CloseCount);
        Assert.Equal(1, mockConn.DisposeCount);
    }
}
