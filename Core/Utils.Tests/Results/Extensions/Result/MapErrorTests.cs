using LightningArc.Utils.Results;
using Xunit;

namespace LightningArc.Utils.Tests.Results
{
    public class MapErrorExtensionsTests
    {
        private static readonly Error TestError = Error.Application.Internal("Test Error");
        private static readonly Error AnotherError = Error.Application.InvalidParameter("Another Error");

        [Fact]
        public void MapError_With_Value_OnFailure_TransformsError()
        {
            // Arrange
            Result<int> result = TestError;
            static Error func(Error e) => AnotherError;

            // Act
            var mappedResult = result.MapError(func);

            // Assert
            Assert.True(mappedResult.IsFailure);
            Assert.Equal(AnotherError, mappedResult.Error);
        }

        [Fact]
        public void MapError_With_Value_OnSuccess_DoesNotTransform()
        {
            // Arrange
            Result<int> result = 100;
            static Error func(Error e) => AnotherError;

            // Act
            var mappedResult = result.MapError(func);

            // Assert
            Assert.True(mappedResult.IsSuccess);
            Assert.Equal(100, mappedResult.Value);
        }

        [Fact]
        public void MapError_No_Value_OnFailure_TransformsError()
        {
            // Arrange
            Result result = TestError;
            static Error func(Error e) => AnotherError;

            // Act
            var mappedResult = result.MapError(func);

            // Assert
            Assert.True(mappedResult.IsFailure);
            Assert.Equal(AnotherError, mappedResult.Error);
        }

        [Fact]
        public async Task MapErrorAsync_With_Value_OnFailure_TransformsError()
        {
            // Arrange
            Result<int> result = TestError;
            static async Task<Error> func(Error e)
            {
                await Task.Delay(1);
                return AnotherError;
            }

            // Act
            var mappedResult = await result.MapErrorAsync(func);

            // Assert
            Assert.True(mappedResult.IsFailure);
            Assert.Equal(AnotherError, mappedResult.Error);
        }

        [Fact]
        public async Task MapErrorAsync_No_Value_OnFailure_TransformsError()
        {
            // Arrange
            Result result = TestError;
            static async Task<Error> func(Error e)
            {
                await Task.Delay(1);
                return AnotherError;
            }

            // Act
            var mappedResult = await result.MapErrorAsync(func);

            // Assert
            Assert.True(mappedResult.IsFailure);
            Assert.Equal(AnotherError, mappedResult.Error);
        }
    }
}