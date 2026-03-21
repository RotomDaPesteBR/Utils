using System.Data.Common;
using LightningArc.Utils.Data.ADO.Oracle.Factories;
using Oracle.ManagedDataAccess.Client;

namespace LightningArc.Utils.Data.Tests.Factories;

public class OracleConnectionFactoryTests
{
    [Test]
    public async Task GetConnection_ShouldReturnOracleConnection()
    {
        // Arrange
        string connectionString = "User Id=myUser;Password=myPassword;Data Source=myOracleDb;";
        OracleConnectionFactory factory = new(connectionString);

        // Act
        DbConnection connection = factory.GetConnection();

        // Assert
        await Assert.That(connection).IsNotNull();
        await Assert.That(connection).IsTypeOf<OracleConnection>();
        await Assert.That(connection.ConnectionString).IsEqualTo(connectionString);
    }
}
