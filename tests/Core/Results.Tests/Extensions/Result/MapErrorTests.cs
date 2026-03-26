using LightningArc.Results;

namespace LightningArc.Results.Tests
{
    public class MapErrorExtensionsTests
    {
        private static readonly Error TestError = Error.Application.Internal("Test Error");
        private static readonly Error AnotherError = Error.Application.InvalidParameter(
            "Another Error"
        );

        [Test]
        public async Task MapError_With_Value_OnFailure_TransformsError()
        {
            // Arrange
            Result<int> result = TestError;

            // Act
            var mappedResult = result.MapError(Func);

            // Assert
            await Assert.That(mappedResult.IsFailure).IsTrue();
            await Assert.That(mappedResult.Error).IsEqualTo(AnotherError);
            return;

            static Error Func(Error e) => AnotherError;
        }

        [Test]
        public async Task MapError_With_Value_OnSuccess_DoesNotTransform()
        {
            // Arrange
            Result<int> result = 100;

            // Act
            var mappedResult = result.MapError(Func);

            // Assert
            await Assert.That(mappedResult.IsSuccess).IsTrue();
            await Assert.That(mappedResult.Value).IsEqualTo(100);
            return;

            static Error Func(Error e) => AnotherError;
        }

        [Test]
        public async Task MapError_No_Value_OnFailure_TransformsError()
        {
            // Arrange
            Result result = TestError;

            // Act
            Result mappedResult = result.MapError(Func);

            // Assert
            await Assert.That(mappedResult.IsFailure).IsTrue();
            await Assert.That(mappedResult.Error).IsEqualTo(AnotherError);
            return;

            static Error Func(Error e) => AnotherError;
        }

        [Test]
        public async Task MapErrorAsync_With_Value_OnFailure_TransformsError()
        {
            // Arrange
            Result<int> result = TestError;

            // Act
            var mappedResult = await result.MapErrorAsync(Func);

            // Assert
            await Assert.That(mappedResult.IsFailure).IsTrue();
            await Assert.That(mappedResult.Error).IsEqualTo(AnotherError);
            return;

            static async Task<Error> Func(Error e)
            {
                await Task.Delay(1);
                return AnotherError;
            }
        }

        [Test]
        public async Task MapErrorAsync_No_Value_OnFailure_TransformsError()
        {
            // Arrange
            Result result = TestError;

            // Act
            Result mappedResult = await result.MapErrorAsync(Func);

            // Assert
            await Assert.That(mappedResult.IsFailure).IsTrue();
            await Assert.That(mappedResult.Error).IsEqualTo(AnotherError);
            return;

            static async Task<Error> Func(Error e)
            {
                await Task.Delay(1);
                return AnotherError;
            }
        }
    }
}


