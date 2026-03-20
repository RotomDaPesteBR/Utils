
using LightningArc.Utils.Results;
using Xunit;

namespace LightningArc.Utils.Tests.Results
{
    public class ResultExtensionsTests
    {
        private static readonly Error TestError = Error.Application.Internal("Test Error");
        private static readonly Error AnotherError = Error.Application.InvalidParameter("Another Error");

        // --- Tap Tests ---

        [Fact]
        public void Tap_OnSuccess_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            var result = Result.Success(42);
            void action(int x) => executed = true;

            // Act
            result.Tap(action);

            // Assert
            Assert.True(executed);
        }

        [Fact]
        public void Tap_OnFailure_DoesNotExecuteAction()
        {
            // Arrange
            bool executed = false;
            var result = Result<int>.Failure(TestError);
            void action(int x) => executed = true;

            // Act
            result.Tap(action);

            // Assert
            Assert.False(executed);
        }

        [Fact]
        public async Task TapAsync_OnSuccess_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            var result = Result.Success("test");
            async Task action(string s)
            {
                await Task.Delay(1);
                executed = true;
            }

            // Act
            await result.TapAsync(action);

            // Assert
            Assert.True(executed);
        }

        // --- Map Tests ---

        [Fact]
        public void Map_OnSuccess_TransformsValue()
        {
            // Arrange
            var result = Result.Success(5);
            static string mapper(int x) => x.ToString();

            // Act
            var mappedResult = result.Map(mapper);

            // Assert
            Assert.True(mappedResult.IsSuccess);
            Assert.Equal("5", mappedResult.Value);
        }

        [Fact]
        public void Map_OnFailure_PropagatesError()
        {
            // Arrange
            var result = Result<int>.Failure(TestError);
            static string mapper(int x) => x.ToString();

            // Act
            var mappedResult = result.Map(mapper);

            // Assert
            Assert.True(mappedResult.IsFailure);
            Assert.Equal(TestError, mappedResult.Error);
        }

        // --- Bind Tests ---

        [Fact]
        public void Bind_OnSuccess_ChainsOperation()
        {
            // Arrange
            var result = Result.Success(10);
            static Result<double> func(int x) => Result.Success(x / 2.0);

            // Act
            var boundResult = result.Bind(func);

            // Assert
            Assert.True(boundResult.IsSuccess);
            Assert.Equal(5.0, boundResult.Value);
        }

        [Fact]
        public void Bind_OnFailure_ShortCircuits()
        {
            // Arrange
            var result = Result<int>.Failure(TestError);
            static Result<double> func(int x) => Result.Success(x / 2.0);

            // Act
            var boundResult = result.Bind(func);

            // Assert
            Assert.True(boundResult.IsFailure);
            Assert.Equal(TestError, boundResult.Error);
        }

        // --- Match Tests ---

        [Fact]
        public void Match_OnSuccess_ExecutesSuccessFunc()
        {
            // Arrange
            var result = Result.Success("ok");

            // Act
            string matchedValue = result.Match(
                success: s => "SUCCESS",
                failure: e => "FAILURE"
            );

            // Assert
            Assert.Equal("SUCCESS", matchedValue);
        }

        [Fact]
        public void Match_OnFailure_ExecutesFailureFunc()
        {
            // Arrange
            var result = Result<string>.Failure(TestError);

            // Act
            string matchedValue = result.Match(
                success: s => "SUCCESS",
                failure: e => "FAILURE"
            );

            // Assert
            Assert.Equal("FAILURE", matchedValue);
        }

        // --- OnFailure Tests ---

        [Fact]
        public void OnFailure_OnFailure_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            var result = Result<int>.Failure(TestError);
            void action(Error e) => executed = true;

            // Act
            result.OnFailure(action);

            // Assert
            Assert.True(executed);
        }

        [Fact]
        public void OnFailure_OnSuccess_DoesNotExecuteAction()
        {
            // Arrange
            bool executed = false;
            var result = Result.Success(123);
            void action(Error e) => executed = true;

            // Act
            result.OnFailure(action);

            // Assert
            Assert.False(executed);
        }

        // --- MapError Tests ---

        [Fact]
        public void MapError_OnFailure_TransformsError()
        {
            // Arrange
            var result = Result<int>.Failure(TestError);
            static Error func(Error e) => AnotherError;

            // Act
            var mappedResult = result.MapError(func);

            // Assert
            Assert.True(mappedResult.IsFailure);
            Assert.Equal(AnotherError, mappedResult.Error);
        }

        [Fact]
        public void MapError_OnSuccess_DoesNotTransform()
        {
            // Arrange
            var result = Result.Success(100);
            static Error func(Error e) => AnotherError;

            // Act
            var mappedResult = result.MapError(func);

            // Assert
            Assert.True(mappedResult.IsSuccess);
            Assert.Equal(100, mappedResult.Value);
        }
    }
}
