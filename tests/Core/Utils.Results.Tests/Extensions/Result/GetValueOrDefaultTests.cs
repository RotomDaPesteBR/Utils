using LightningArc.Utils.Results;

namespace LightningArc.Utils.Tests.Results
{
    public class GetValueOrDefaultExtensionsTests
    {
        private static readonly Error TestError = Error.Application.Internal("Test Error");

        [Test]
        public async Task GetValueOrDefault_OnSuccess_ReturnsValueAsync()
        {
            // Arrange
            Result<int> result = 10;

            // Act
            int value = result.GetValueOrDefault();

            // Assert
            await Assert.That(value).IsEqualTo<int>((int)10);
        }

        [Test]
        public async Task GetValueOrDefault_OnFailure_ReturnsDefaultAsync()
        {
            // Arrange
            Result<int> result = TestError;

            // Act
            int value = result.GetValueOrDefault();

            // Assert
            await Assert.That(value).IsEqualTo<int>((int)0);
        }

        [Test]
        public async Task GetValueOrDefault_WithDefaultValue_OnSuccess_ReturnsValueAsync()
        {
            // Arrange
            Result<int> result = 10;

            // Act
            int value = result.GetValueOrDefault(20);

            // Assert
            await Assert.That(value).IsEqualTo<int>((int)10);
        }

        [Test]
        public async Task GetValueOrDefault_WithDefaultValue_OnFailure_ReturnsDefaultValueAsync()
        {
            // Arrange
            Result<int> result = TestError;

            // Act
            int value = result.GetValueOrDefault(20);

            // Assert
            await Assert.That(value).IsEqualTo<int>((int)20);
        }

        [Test]
        public async Task GetValueOrDefault_WithDefaultValueFactory_OnSuccess_ReturnsValueAsync()
        {
            // Arrange
            Result<int> result = 10;

            // Act
            int value = result.GetValueOrDefault(Factory);

            // Assert
            await Assert.That(value).IsEqualTo<int>((int)10);
            return;

            static int Factory() => 30;
        }

        [Test]
        public async Task GetValueOrDefault_WithDefaultValueFactory_OnFailure_ReturnsDefaultValueAsync()
        {
            // Arrange
            Result<int> result = TestError;

            // Act
            int value = result.GetValueOrDefault(Factory);

            // Assert
            await Assert.That(value).IsEqualTo<int>((int)30);
            return;

            static int Factory() => 30;
        }

        [Test]
        public async Task GetValueOrDefaultAsync_OnSuccess_ReturnsValueAsync()
        {
            // Arrange
            Result<int> result = 10;

            // Act
            int value = await result
                .GetValueOrDefaultAsync(async () =>
                {
                    await Task.Delay(1).ConfigureAwait(false);
                    return 20;
                })
                .ConfigureAwait(false);

            // Assert
            await Assert.That(value).IsEqualTo<int>((int)10);
        }
    }
}
