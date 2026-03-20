using System.Data.Common;
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
        string connectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
        SqlConnectionFactory factory = new(connectionString);

        // Act
        DbConnection connection = factory.GetConnection();

        // Assert
        Assert.NotNull(connection);
        Assert.IsType<SqlConnection>(connection);
        Assert.Equal(connectionString, connection.ConnectionString);
    }

    [Fact]
    public void Constructor_WithConfiguration_ShouldReadConnectionString()
    {
        // Arrange
        string connectionString = "Server=myServerAddress;Database=myDataBase;";
        var inMemorySettings = new Dictionary<string, string?> {
            {"ConnectionStrings:DatabaseConnection", connectionString},
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        // Act
        SqlConnectionFactory factory = new(configuration);
        DbConnection connection = factory.GetConnection();

        // Assert
        Assert.Equal(connectionString, connection.ConnectionString);
    }
}