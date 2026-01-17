using System.Data.Common;
using LightningArc.Utils.Data.ADO.Oracle.Factories;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Configuration;

namespace LightningArc.Utils.Data.Tests.Factories;

public class OracleConnectionFactoryTests
{
    [Fact]
    public void GetConnection_ShouldReturnOracleConnection()
    {
        // Arrange
        string connectionString = "User Id=myUser;Password=myPassword;Data Source=myOracleDb;";
        OracleConnectionFactory factory = new(connectionString);

        // Act
        DbConnection connection = factory.GetConnection();

        // Assert
        Assert.NotNull(connection);
        Assert.IsType<OracleConnection>(connection);
        Assert.Equal(connectionString, connection.ConnectionString);
    }
}
