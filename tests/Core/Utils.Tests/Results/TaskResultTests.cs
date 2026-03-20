
using LightningArc.Utils.Results;
using Xunit;

namespace LightningArc.Utils.Tests.Results
{
    public class TaskResultTests
    {
        private static readonly Error TestError = Error.Application.Internal("Test Error");

        [Fact]
        public async Task Await_SuccessTaskResult_ShouldReturnSuccessResult()
        {
            // Arrange
            const int expectedValue = 123;
            var taskResult = TaskResult.Success(expectedValue);

            // Act
            var result = await taskResult;

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedValue, result.Value);
        }

        [Fact]
        public async Task Await_FailureTaskResult_ShouldReturnFailureResult()
        {
            // Arrange
            var taskResult = TaskResult.Failure<int>(TestError);

            // Act
            var result = await taskResult;

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(TestError, result.Error);
        }

        [Fact]
        public async Task ImplicitConversion_FromTask_ShouldBeAwaitable()
        {
            // Arrange
            const string expectedValue = "test";
            Task<Result<string>> task = Task.FromResult(Result.Success(expectedValue));

            // Act
            TaskResult<string> taskResult = task;
            var result = await taskResult;

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedValue, result.Value);
        }

        [Fact]
        public async Task FromTask_WithSuccessResult_ShouldReturnSuccess()
        {
            // Arrange
            var task = Task.FromResult(Result.Success());

            // Act
            var taskResult = TaskResult.FromTask(task);
            var result = await taskResult;

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.IsType<object>(result.Value);
        }

        [Fact]
        public async Task FromTask_WithFailureResult_ShouldReturnFailure()
        {
            // Arrange
            var task = Task.FromResult(Result.Failure(TestError));

            // Act
            var taskResult = TaskResult.FromTask(task);
            var result = await taskResult;

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(TestError, result.Error);
        }

        [Fact]
        public async Task NonGeneric_Success_ShouldReturnSuccess()
        {
            // Arrange
            var taskResult = TaskResult.Success();

            // Act
            var result = await taskResult;

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task NonGeneric_Failure_ShouldReturnFailure()
        {
            // Arrange
            var taskResult = TaskResult.Failure(TestError);

            // Act
            var result = await taskResult;

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(TestError, result.Error);
        }
    }
}
