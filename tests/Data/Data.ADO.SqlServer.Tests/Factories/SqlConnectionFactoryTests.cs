using System.Data.Common;
using LightningArc.Data.ADO.SqlServer.Factories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LightningArc.Data.Tests.Factories;

public class SqlConnectionFactoryTests
{
    [Test]
    public async Task GetConnection_ShouldReturnSqlConnection()
    {
        // Arrange
        string connectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
        SqlConnectionFactory factory = new(connectionString);

        // Act
        DbConnection connection = factory.GetConnection();

        // Assert
        await Assert.That(connection).IsNotNull();
        await Assert.That(connection).IsTypeOf<SqlConnection>();
        await Assert.That(connection.ConnectionString).IsEqualTo(connectionString);
    }

    [Test]
    public async Task Constructor_WithConfiguration_ShouldReadConnectionString()
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
        await Assert.That(connection.ConnectionString).IsEqualTo(connectionString);
    }
}

