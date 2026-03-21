using LightningArc.Utils.Results;

namespace LightningArc.Utils.Tests.Results
{
    public class MatchExtensionsTests
    {
        private static readonly Error TestError = Error.Application.Internal("Test Error");

        [Test]
        public async Task Match_With_Value_OnSuccess_ExecutesSuccessFunc()
        {
            // Arrange
            Result<string> result = "ok";

            // Act
            string matchedValue = result.Match(
                success: s => "SUCCESS",
                failure: e => "FAILURE"
            );

            // Assert
            await Assert.That(matchedValue).IsEqualTo("SUCCESS");
        }

        [Test]
        public async Task Match_With_Value_OnFailure_ExecutesFailureFunc()
        {
            // Arrange
            Result<string> result = TestError;

            // Act
            string matchedValue = result.Match(
                success: s => "SUCCESS",
                failure: e => "FAILURE"
            );

            // Assert
            await Assert.That(matchedValue).IsEqualTo("FAILURE");
        }

        [Test]
        public async Task Match_No_Value_OnSuccess_ExecutesSuccessFunc()
        {
            // Arrange
            Result result = Result.Success();

            // Act
            string matchedValue = result.Match(
                success: () => "SUCCESS",
                failure: e => "FAILURE"
            );

            // Assert
            await Assert.That(matchedValue).IsEqualTo("SUCCESS");
        }

        [Test]
        public async Task MatchAsync_With_Value_OnSuccess_ExecutesSuccessFunc()
        {
            // Arrange
            Result<string> result = "ok";

            // Act
            var matchedValue = await result.MatchAsync(
                success: async s =>
                {
                    await Task.Delay(1);
                    return Result.Success("SUCCESS");
                },
                failure: async e =>
                {
                    await Task.Delay(1);
                    return e;
                }
            );

            // Assert
            await Assert.That(matchedValue.IsSuccess).IsTrue();
            await Assert.That(matchedValue.Value).IsEqualTo("SUCCESS");
        }

        [Test]
        public async Task MatchAsync_No_Value_To_Generic_OnSuccess_ExecutesSuccessFunc()
        {
            // Arrange
            Result result = Result.Success();

            // Act
            Result<string> matchedValue = await result.MatchAsync<string>(
                success: async s =>
                {
                    await Task.Delay(1);
                    return "SUCCESS";
                },
                failure: async e =>
                {
                    await Task.Delay(1);
                    return e;
                }
            );

            // Assert
            await Assert.That(matchedValue.IsSuccess).IsTrue();
            await Assert.That(matchedValue.Value).IsEqualTo("SUCCESS");
        }
    }
}