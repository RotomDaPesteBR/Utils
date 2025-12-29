using LightningArc.Utils.Data.ADO.SqlServer.Factories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LightningArc.Utils.Data.Tests.Factories;

public class SqlConnectionFactoryTests
{
    [Fact]
    public void GetConnection_ShouldReturnSqlConnection()
    {
        // Arrange
        var connectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
        var factory = new SqlConnectionFactory(connectionString);

        // Act
        var connection = factory.GetConnection();

        // Assert
        Assert.NotNull(connection);
        Assert.IsType<SqlConnection>(connection);
        Assert.Equal(connectionString, connection.ConnectionString);
    }

    [Fact]
    public void Constructor_WithConfiguration_ShouldReadConnectionString()
    {
        // Arrange
        var connectionString = "Server=myServerAddress;Database=myDataBase;";
        var inMemorySettings = new Dictionary<string, string?> {
            {"ConnectionStrings:DatabaseConnection", connectionString},
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        // Act
        var factory = new SqlConnectionFactory(configuration);
        var connection = factory.GetConnection();

        // Assert
        Assert.Equal(connectionString, connection.ConnectionString);
    }
}