using LightningArc.Utils.Results;
using Xunit;

namespace LightningArc.Utils.Tests.Results
{
    public class MapSuccessExtensionsTests
    {
        [Fact]
        public void SuccessMap_With_Value_OnSuccess_TransformsValue()
        {
            // Arrange
            var success = Utils.Results.Success.Ok(5);
            static string mapper(int x) => x.ToString();

            // Act
            var mappedSuccess = success.Map(mapper);

            // Assert
            Assert.Equal("5", mappedSuccess.Value);
            Assert.Equal(success.Code, mappedSuccess.Code);
        }

        [Fact]
        public void SuccessMap_No_Value_OnSuccess_TransformsValue()
        {
            // Arrange
            Success success = Utils.Results.Success.Ok();
            static string mapper() => "success";

            // Act
            var mappedSuccess = success.Map(mapper);

            // Assert
            Assert.Equal("success", mappedSuccess.Value);
            Assert.Equal(success.Code, mappedSuccess.Code);
        }

        [Fact]
        public async Task SuccessMapAsync_With_Value_OnSuccess_TransformsValue()
        {
            // Arrange
            var success = Utils.Results.Success.Ok(5);
            static async Task<string> mapper(int x)
            {
                await Task.Delay(1);
                return x.ToString();
            }

            // Act
            var mappedSuccess = await success.MapAsync(mapper);

            // Assert
            Assert.Equal("5", mappedSuccess.Value);
            Assert.Equal(success.Code, mappedSuccess.Code);
        }

        [Fact]
        public async Task SuccessMapAsync_No_Value_OnSuccess_TransformsValue()
        {
            // Arrange
            Success success = Utils.Results.Success.Ok();
            static async Task<string> mapper()
            {
                await Task.Delay(1);
                return "success";
            }

            // Act
            var mappedSuccess = await success.MapAsync(mapper);

            // Assert
            Assert.Equal("success", mappedSuccess.Value);
            Assert.Equal(success.Code, mappedSuccess.Code);
        }
    }
}