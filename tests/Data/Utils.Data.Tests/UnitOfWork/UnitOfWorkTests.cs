using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using LightningArc.Utils.Data.ADO.Factories;
using LightningArc.Utils.Data.ADO.UnitOfWork;

namespace LightningArc.Utils.Data.Tests.UnitOfWork;

public class UnitOfWorkTests
{
    private class TestDbConnection : DbConnection
    {
        public int CloseCount { get; private set; }
        public int DisposeCount { get; private set; }
        public int OpenCount { get; private set; }
        public TestDbTransaction? LastTransaction { get; private set; }

        [AllowNull]
        public override string ConnectionString { get; set; } = "";
        public override string Database => "";
        public override string DataSource => "";
        public override string ServerVersion => "";
        public override ConnectionState State => ConnectionState.Open;

        public override void ChangeDatabase(string databaseName) { }

        public override void Close() => CloseCount++;

        public override void Open() => OpenCount++;

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            LastTransaction = new TestDbTransaction(this);
            return LastTransaction;
        }

        protected override DbCommand CreateDbCommand() => null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                DisposeCount++;
            base.Dispose(disposing);
        }
    }

    private class TestDbTransaction(DbConnection connection) : DbTransaction
    {
        public int CommitCount { get; private set; }
        public int RollbackCount { get; private set; }
        public int DisposeCount { get; private set; }

        protected override DbConnection DbConnection => connection;
        public override IsolationLevel IsolationLevel => IsolationLevel.ReadCommitted;

        public override void Commit() => CommitCount++;
        public override void Rollback() => RollbackCount++;

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

    [Fact]
    public void Begin_ShouldStartTransaction()
    {
        // Arrange
        TestDbConnection mockConn = new();
        ConnectionFactoryStub factory = new(mockConn);
        var uow = new LightningArc.Utils.Data.ADO.UnitOfWork.UnitOfWork(factory);

        // Act
        uow.Begin();

        // Assert
        Assert.Equal(mockConn, uow.Connection);
        Assert.NotNull(uow.Transaction);
        Assert.Equal(mockConn.LastTransaction, uow.Transaction);
    }

    [Fact]
    public void Begin_ShouldThrow_WhenAlreadyStarted()
    {
        // Arrange
        TestDbConnection mockConn = new();
        ConnectionFactoryStub factory = new(mockConn);
        var uow = new LightningArc.Utils.Data.ADO.UnitOfWork.UnitOfWork(factory);
        uow.Begin();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => uow.Begin());
    }

    [Fact]
    public void Commit_ShouldCommitAndDispose()
    {
        // Arrange
        TestDbConnection mockConn = new();
        ConnectionFactoryStub factory = new(mockConn);
        var uow = new LightningArc.Utils.Data.ADO.UnitOfWork.UnitOfWork(factory);
        uow.Begin();
        var trans = (TestDbTransaction)uow.Transaction;

        // Act
        uow.Commit();

        // Assert
        Assert.Equal(1, trans.CommitCount);
        Assert.Equal(1, trans.DisposeCount);
        Assert.Equal(1, mockConn.CloseCount);
        Assert.Equal(1, mockConn.DisposeCount);
    }

    [Fact]
    public void Commit_ShouldThrow_WhenNotStarted()
    {
        // Arrange
        TestDbConnection mockConn = new();
        ConnectionFactoryStub factory = new(mockConn);
        var uow = new LightningArc.Utils.Data.ADO.UnitOfWork.UnitOfWork(factory);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => uow.Commit());
    }

    [Fact]
    public void Rollback_ShouldRollbackAndDispose()
    {
        // Arrange
        TestDbConnection mockConn = new();
        ConnectionFactoryStub factory = new(mockConn);
        var uow = new LightningArc.Utils.Data.ADO.UnitOfWork.UnitOfWork(factory);
        uow.Begin();
        var trans = (TestDbTransaction)uow.Transaction;

        // Act
        uow.Rollback();

        // Assert
        Assert.Equal(1, trans.RollbackCount);
        Assert.Equal(1, trans.DisposeCount);
        Assert.Equal(1, mockConn.CloseCount);
        Assert.Equal(1, mockConn.DisposeCount);
    }

    [Fact]
    public void Rollback_ShouldNotThrow_WhenNotStarted()
    {
        // Arrange
        TestDbConnection mockConn = new();
        ConnectionFactoryStub factory = new(mockConn);
        var uow = new LightningArc.Utils.Data.ADO.UnitOfWork.UnitOfWork(factory);

        // Act
        uow.Rollback();

        // Assert (No exception expected)
    }

    [Fact]
    public void Connection_ShouldThrow_WhenAccessedBeforeBegin()
    {
        // Arrange
        TestDbConnection mockConn = new();
        ConnectionFactoryStub factory = new(mockConn);
        var uow = new LightningArc.Utils.Data.ADO.UnitOfWork.UnitOfWork(factory);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => uow.Connection);
    }

    [Fact]
    public void Transaction_ShouldThrow_WhenAccessedBeforeBegin()
    {
        // Arrange
        TestDbConnection mockConn = new();
        ConnectionFactoryStub factory = new(mockConn);
        var uow = new LightningArc.Utils.Data.ADO.UnitOfWork.UnitOfWork(factory);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => uow.Transaction);
    }
}