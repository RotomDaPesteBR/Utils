using LightningArc.Utils.Results;
using Xunit;

namespace LightningArc.Utils.Tests.Results
{
    public class EnsureExtensionsTests
    {
        private static readonly Error TestError = Error.Application.Internal("Test Error");
        private static readonly Error AnotherError = Error.Application.InvalidParameter("Another Error");

        [Fact]
        public void Ensure_With_Value_OnSuccess_WhenTrue_ReturnsSuccess()
        {
            // Arrange
            Result<int> result = 5;

            // Act
            var ensuredResult = result.Ensure(x => x > 0, TestError);

            // Assert
            Assert.True(ensuredResult.IsSuccess);
            Assert.Equal(5, ensuredResult.Value);
        }

        [Fact]
        public void Ensure_With_Value_OnSuccess_WhenFalse_ReturnsFailure()
        {
            // Arrange
            Result<int> result = 5;

            // Act
            var ensuredResult = result.Ensure(x => x < 0, TestError);

            // Assert
            Assert.True(ensuredResult.IsFailure);
            Assert.Equal(TestError, ensuredResult.Error);
        }

        [Fact]
        public void Ensure_With_Value_OnFailure_ReturnsFailure()
        {
            // Arrange
            Result<int> result = AnotherError;

            // Act
            var ensuredResult = result.Ensure(x => x > 0, TestError);

            // Assert
            Assert.True(ensuredResult.IsFailure);
            Assert.Equal(AnotherError, ensuredResult.Error);
        }

        [Fact]
        public void Ensure_No_Value_OnSuccess_WhenTrue_ReturnsSuccess()
        {
            // Arrange
            Result result = Result.Success();

            // Act
            Result ensuredResult = result.Ensure(true, TestError);

            // Assert
            Assert.True(ensuredResult.IsSuccess);
        }

        [Fact]
        public void Ensure_No_Value_OnSuccess_WhenFalse_ReturnsFailure()
        {
            // Arrange
            Result result = Result.Success();

            // Act
            Result ensuredResult = result.Ensure(false, TestError);

            // Assert
            Assert.True(ensuredResult.IsFailure);
            Assert.Equal(TestError, ensuredResult.Error);
        }

        [Fact]
        public async Task EnsureAsync_With_Value_OnSuccess_WhenTrue_ReturnsSuccess()
        {
            // Arrange
            Result<int> result = 5;

            // Act
            var ensuredResult = await result.EnsureAsync(async x =>
            {
                await Task.Delay(1);
                return x > 0;
            }, TestError);

            // Assert
            Assert.True(ensuredResult.IsSuccess);
            Assert.Equal(5, ensuredResult.Value);
        }

        [Fact]
        public async Task EnsureAsync_No_Value_OnSuccess_WhenTrue_ReturnsSuccess()
        {
            // Arrange
            Result result = Result.Success();

            // Act
            Result ensuredResult = await result.EnsureAsync(async () =>
            {
                await Task.Delay(1);
                return true;
            }, TestError);

            // Assert
            Assert.True(ensuredResult.IsSuccess);
        }
    }
}