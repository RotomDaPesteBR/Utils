using LightningArc.Utils.Results;

namespace LightningArc.Utils.Tests.Results
{
    public class MapExtensionsTests
    {
        private static readonly Error TestError = Error.Application.Internal("Test Error");

        [Test]
        public async Task Map_With_Value_OnSuccess_TransformsValue()
        {
            // Arrange
            Result<int> result = 5;

            // Act
            var mappedResult = result.Map(Mapper);

            // Assert
            await Assert.That(mappedResult.IsSuccess).IsTrue();
            await Assert.That(mappedResult.Value).IsEqualTo("5");
            return;

            static string Mapper(int x) => x.ToString();
        }

        [Test]
        public async Task Map_With_Value_OnFailure_PropagatesError()
        {
            // Arrange
            Result<int> result = TestError;

            // Act
            var mappedResult = result.Map(Mapper);

            // Assert
            await Assert.That(mappedResult.IsFailure).IsTrue();
            await Assert.That(mappedResult.Error).IsEqualTo(TestError);
            return;

            static string Mapper(int x) => x.ToString();
        }

        [Test]
        public async Task Map_No_Value_OnSuccess_TransformsValue()
        {
            // Arrange
            Result result = Result.Success();

            // Act
            var mappedResult = result.Map(Mapper);

            // Assert
            await Assert.That(mappedResult.IsSuccess).IsTrue();
            await Assert.That(mappedResult.Value).IsEqualTo("success");
            return;

            static string Mapper() => "success";
        }

        [Test]
        public async Task MapAsync_With_Value_OnSuccess_TransformsValue()
        {
            // Arrange
            Result<int> result = 5;

            // Act
            var mappedResult = await result.MapAsync(Mapper);

            // Assert
            await Assert.That(mappedResult.IsSuccess).IsTrue();
            await Assert.That(mappedResult.Value).IsEqualTo("5");
            return;

            static async Task<string> Mapper(int x)
            {
                await Task.Delay(1);
                return x.ToString();
            }
        }

        [Test]
        public async Task MapAsync_No_Value_OnSuccess_TransformsValue()
        {
            // Arrange
            Result result = Result.Success();

            // Act
            var mappedResult = await result.MapAsync(Mapper);

            // Assert
            await Assert.That(mappedResult.IsSuccess).IsTrue();
            await Assert.That(mappedResult.Value).IsEqualTo("success");
            return;

            static async Task<string> Mapper()
            {
                await Task.Delay(1);
                return "success";
            }
        }
    }
}
