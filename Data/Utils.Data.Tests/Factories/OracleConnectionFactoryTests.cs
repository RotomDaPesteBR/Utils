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
        var connectionString = "User Id=myUser;Password=myPassword;Data Source=myOracleDb;";
        var factory = new OracleConnectionFactory(connectionString);

        // Act
        var connection = factory.GetConnection();

        // Assert
        Assert.NotNull(connection);
        Assert.IsType<OracleConnection>(connection);
        Assert.Equal(connectionString, connection.ConnectionString);
    }
}
