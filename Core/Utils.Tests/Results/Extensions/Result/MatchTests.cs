using LightningArc.Utils.Results;
using Xunit;

namespace LightningArc.Utils.Tests.Results
{
    public class MatchExtensionsTests
    {
        private static readonly Error TestError = Error.Application.Internal("Test Error");

        [Fact]
        public void Match_With_Value_OnSuccess_ExecutesSuccessFunc()
        {
            // Arrange
            Result<string> result = "ok";

            // Act
            var matchedValue = result.Match(
                success: s => "SUCCESS",
                failure: e => "FAILURE"
            );

            // Assert
            Assert.Equal("SUCCESS", matchedValue);
        }

        [Fact]
        public void Match_With_Value_OnFailure_ExecutesFailureFunc()
        {
            // Arrange
            Result<string> result = TestError;

            // Act
            var matchedValue = result.Match(
                success: s => "SUCCESS",
                failure: e => "FAILURE"
            );

            // Assert
            Assert.Equal("FAILURE", matchedValue);
        }

        [Fact]
        public void Match_No_Value_OnSuccess_ExecutesSuccessFunc()
        {
            // Arrange
            Result result = Result.Success();

            // Act
            var matchedValue = result.Match(
                success: () => "SUCCESS",
                failure: e => "FAILURE"
            );

            // Assert
            Assert.Equal("SUCCESS", matchedValue);
        }

        [Fact]
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
            Assert.True(matchedValue.IsSuccess);
            Assert.Equal("SUCCESS", matchedValue.Value);
        }

        [Fact]
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
            Assert.True(matchedValue.IsSuccess);
            Assert.Equal("SUCCESS", matchedValue.Value);
        }
    }
}