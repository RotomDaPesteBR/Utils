using LightningArc.Utils.Results;
using Xunit;

namespace LightningArc.Utils.Tests.Results
{
    public class GetValueOrDefaultExtensionsTests
    {
        private static readonly Error TestError = Error.Application.Internal("Test Error");

        [Fact]
        public void GetValueOrDefault_OnSuccess_ReturnsValue()
        {
            // Arrange
            Result<int> result = 10;

            // Act
            int value = result.GetValueOrDefault();

            // Assert
            Assert.Equal(10, value);
        }

        [Fact]
        public void GetValueOrDefault_OnFailure_ReturnsDefault()
        {
            // Arrange
            Result<int> result = TestError;

            // Act
            int value = result.GetValueOrDefault();

            // Assert
            Assert.Equal(default, value);
        }

        [Fact]
        public void GetValueOrDefault_WithDefaultValue_OnSuccess_ReturnsValue()
        {
            // Arrange
            Result<int> result = 10;

            // Act
            int value = result.GetValueOrDefault(20);

            // Assert
            Assert.Equal(10, value);
        }

        [Fact]
        public void GetValueOrDefault_WithDefaultValue_OnFailure_ReturnsDefaultValue()
        {
            // Arrange
            Result<int> result = TestError;

            // Act
            int value = result.GetValueOrDefault(20);

            // Assert
            Assert.Equal(20, value);
        }

        [Fact]
        public void GetValueOrDefault_WithDefaultValueFactory_OnSuccess_ReturnsValue()
        {
            // Arrange
            Result<int> result = 10;
            static int factory() => 30;

            // Act
            int value = result.GetValueOrDefault(factory);

            // Assert
            Assert.Equal(10, value);
        }

        [Fact]
        public void GetValueOrDefault_WithDefaultValueFactory_OnFailure_ReturnsDefaultValue()
        {
            // Arrange
            Result<int> result = TestError;
            static int factory() => 30;

            // Act
            int value = result.GetValueOrDefault(factory);

            // Assert
            Assert.Equal(30, value);
        }

        [Fact]
        public async Task GetValueOrDefaultAsync_OnSuccess_ReturnsValue()
        {
            // Arrange
            Result<int> result = 10;

            // Act
            int value = await result.GetValueOrDefaultAsync(async () =>
            {
                await Task.Delay(1);
                return 20;
            });

            // Assert
            Assert.Equal(10, value);
        }
    }
}
