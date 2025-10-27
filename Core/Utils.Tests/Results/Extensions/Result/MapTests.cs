using LightningArc.Utils.Results;
using Xunit;

namespace LightningArc.Utils.Tests.Results
{
    public class MapExtensionsTests
    {
        private static readonly Error TestError = Error.Application.Internal("Test Error");

        [Fact]
        public void Map_With_Value_OnSuccess_TransformsValue()
        {
            // Arrange
            Result<int> result = 5;
            static string mapper(int x) => x.ToString();

            // Act
            var mappedResult = result.Map(mapper);

            // Assert
            Assert.True(mappedResult.IsSuccess);
            Assert.Equal("5", mappedResult.Value);
        }

        [Fact]
        public void Map_With_Value_OnFailure_PropagatesError()
        {
            // Arrange
            Result<int> result = TestError;
            static string mapper(int x) => x.ToString();

            // Act
            var mappedResult = result.Map(mapper);

            // Assert
            Assert.True(mappedResult.IsFailure);
            Assert.Equal(TestError, mappedResult.Error);
        }

        [Fact]
        public void Map_No_Value_OnSuccess_TransformsValue()
        {
            // Arrange
            Result result = Result.Success();
            static string mapper() => "success";

            // Act
            var mappedResult = result.Map(mapper);

            // Assert
            Assert.True(mappedResult.IsSuccess);
            Assert.Equal("success", mappedResult.Value);
        }

        [Fact]
        public async Task MapAsync_With_Value_OnSuccess_TransformsValue()
        {
            // Arrange
            Result<int> result = 5;
            static async Task<string> mapper(int x)
            {
                await Task.Delay(1);
                return x.ToString();
            }

            // Act
            var mappedResult = await result.MapAsync(mapper);

            // Assert
            Assert.True(mappedResult.IsSuccess);
            Assert.Equal("5", mappedResult.Value);
        }

        [Fact]
        public async Task MapAsync_No_Value_OnSuccess_TransformsValue()
        {
            // Arrange
            Result result = Result.Success();
            static async Task<string> mapper()
            {
                await Task.Delay(1);
                return "success";
            }

            // Act
            var mappedResult = await result.MapAsync(mapper);

            // Assert
            Assert.True(mappedResult.IsSuccess);
            Assert.Equal("success", mappedResult.Value);
        }
    }
}