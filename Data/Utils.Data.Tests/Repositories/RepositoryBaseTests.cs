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

        public new void FinalizeConnection(DbConnection conn) => base.FinalizeConnection(conn);
    }

    [Fact]
    public void GetConnection_ShouldReturnFactoryConnection_WhenNoConnectionProvided()
    {
        // Arrange
        var mockConn = new TestDbConnection();
        var factory = new ConnectionFactoryStub(mockConn);
        var repo = new InternalTestRepository(factory);

        // Act
        var conn = repo.GetConnection();

        // Assert
        Assert.Equal(mockConn, conn);
    }

    [Fact]
    public void GetConnection_ShouldReturnProvidedConnection_WhenConnectionProvidedInConstructor()
    {
        // Arrange
        var mockConn = new TestDbConnection();
        var repo = new InternalTestRepository(mockConn, null!);

        // Act
        var conn = repo.GetConnection();

        // Assert
        Assert.Equal(mockConn, conn);
    }

    [Fact]
    public void FinalizeConnection_ShouldNotClose_WhenConnectionWasProvidedInConstructor()
    {
        // Arrange
        var mockConn = new TestDbConnection();
        var repo = new InternalTestRepository(mockConn, null!);

        // Act
        repo.FinalizeConnection(mockConn);

        // Assert
        Assert.Equal(0, mockConn.CloseCount);
    }

    [Fact]
    public void FinalizeConnection_ShouldCloseAndDispose_WhenConnectionWasCreatedLocally()
    {
        // Arrange
        var mockConn = new TestDbConnection();
        var factory = new ConnectionFactoryStub(mockConn);
        var repo = new InternalTestRepository(factory);

        // Act
        repo.FinalizeConnection(mockConn);

        // Assert
        Assert.Equal(1, mockConn.CloseCount);
        Assert.Equal(1, mockConn.DisposeCount);
    }
}
