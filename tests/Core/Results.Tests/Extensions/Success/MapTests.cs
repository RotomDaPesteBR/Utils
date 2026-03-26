using LightningArc.Results;

namespace LightningArc.Results.Tests
{
    public class MapSuccessExtensionsTests
    {
        [Test]
        public async Task SuccessMap_With_Value_OnSuccess_TransformsValue()
        {
            // Arrange
            var success = Success.Ok(5);

            // Act
            var mappedSuccess = success.Map(Mapper);

            // Assert
            await Assert.That(mappedSuccess.Value).IsEqualTo("5");
            await Assert.That(mappedSuccess.Code).IsEqualTo(success.Code);
            return;

            static string Mapper(int x) => x.ToString();
        }

        [Test]
        public async Task SuccessMap_No_Value_OnSuccess_TransformsValue()
        {
            // Arrange
            Success success = Success.Ok();

            // Act
            var mappedSuccess = success.Map(Mapper);

            // Assert
            await Assert.That(mappedSuccess.Value).IsEqualTo("success");
            await Assert.That(mappedSuccess.Code).IsEqualTo(success.Code);
            return;

            static string Mapper() => "success";
        }

        [Test]
        public async Task SuccessMapAsync_With_Value_OnSuccess_TransformsValue()
        {
            // Arrange
            var success = Success.Ok(5);

            // Act
            var mappedSuccess = await success.MapAsync(Mapper);

            // Assert
            await Assert.That(mappedSuccess.Value).IsEqualTo("5");
            await Assert.That(mappedSuccess.Code).IsEqualTo(success.Code);
            return;

            static async Task<string> Mapper(int x)
            {
                await Task.Delay(1);
                return x.ToString();
            }
        }

        [Test]
        public async Task SuccessMapAsync_No_Value_OnSuccess_TransformsValue()
        {
            // Arrange
            Success success = Success.Ok();

            // Act
            var mappedSuccess = await success.MapAsync(Mapper);

            // Assert
            await Assert.That(mappedSuccess.Value).IsEqualTo("success");
            await Assert.That(mappedSuccess.Code).IsEqualTo(success.Code);
            return;

            static async Task<string> Mapper()
            {
                await Task.Delay(1);
                return "success";
            }
        }
    }
}


