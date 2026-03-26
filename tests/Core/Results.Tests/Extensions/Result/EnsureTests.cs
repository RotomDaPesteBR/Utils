using LightningArc.Results;

namespace LightningArc.Results.Tests
{
    public class EnsureExtensionsTests
    {
        private static readonly Error TestError = Error.Application.Internal("Test Error");
        private static readonly Error AnotherError = Error.Application.InvalidParameter("Another Error");

        [Test]
        public async Task Ensure_With_Value_OnSuccess_WhenTrue_ReturnsSuccess()
        {
            // Arrange
            Result<int> result = 5;

            // Act
            var ensuredResult = result.Ensure(x => x > 0, TestError);

            // Assert
            await Assert.That(ensuredResult.IsSuccess).IsTrue();
            await Assert.That(ensuredResult.Value).IsEqualTo(5);
        }

        [Test]
        public async Task Ensure_With_Value_OnSuccess_WhenFalse_ReturnsFailure()
        {
            // Arrange
            Result<int> result = 5;

            // Act
            var ensuredResult = result.Ensure(x => x < 0, TestError);

            // Assert
            await Assert.That(ensuredResult.IsFailure).IsTrue();
            await Assert.That(ensuredResult.Error).IsEqualTo(TestError);
        }

        [Test]
        public async Task Ensure_With_Value_OnFailure_ReturnsFailure()
        {
            // Arrange
            Result<int> result = AnotherError;

            // Act
            var ensuredResult = result.Ensure(x => x > 0, TestError);

            // Assert
            await Assert.That(ensuredResult.IsFailure).IsTrue();
            await Assert.That(ensuredResult.Error).IsEqualTo(AnotherError);
        }

        [Test]
        public async Task Ensure_No_Value_OnSuccess_WhenTrue_ReturnsSuccess()
        {
            // Arrange
            Result result = Result.Success();

            // Act
            Result ensuredResult = result.Ensure(true, TestError);

            // Assert
            await Assert.That(ensuredResult.IsSuccess).IsTrue();
        }

        [Test]
        public async Task Ensure_No_Value_OnSuccess_WhenFalse_ReturnsFailure()
        {
            // Arrange
            Result result = Result.Success();

            // Act
            Result ensuredResult = result.Ensure(false, TestError);

            // Assert
            await Assert.That(ensuredResult.IsFailure).IsTrue();
            await Assert.That(ensuredResult.Error).IsEqualTo(TestError);
        }

        [Test]
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
            await Assert.That(ensuredResult.IsSuccess).IsTrue();
            await Assert.That(ensuredResult.Value).IsEqualTo(5);
        }

        [Test]
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
            await Assert.That(ensuredResult.IsSuccess).IsTrue();
        }
    }
}


