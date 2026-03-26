
using LightningArc.Results;

namespace LightningArc.Results.Tests
{
    public class TaskResultTests
    {
        private static readonly Error TestError = Error.Application.Internal("Test Error");

        [Test]
        public async Task Await_SuccessTaskResult_ShouldReturnSuccessResult()
        {
            // Arrange
            const int expectedValue = 123;
            var taskResult = TaskResult.Success(expectedValue);

            // Act
            var result = await taskResult;

            // Assert
            await Assert.That(result.IsSuccess).IsTrue();
            await Assert.That(result.Value).IsEqualTo(expectedValue);
        }

        [Test]
        public async Task Await_FailureTaskResult_ShouldReturnFailureResult()
        {
            // Arrange
            var taskResult = TaskResult.Failure<int>(TestError);

            // Act
            var result = await taskResult;

            // Assert
            await Assert.That(result.IsFailure).IsTrue();
            await Assert.That(result.Error).IsEqualTo(TestError);
        }

        [Test]
        public async Task ImplicitConversion_FromTask_ShouldBeAwaitable()
        {
            // Arrange
            const string expectedValue = "test";
            Task<Result<string>> task = Task.FromResult(Result.Success(expectedValue));

            // Act
            TaskResult<string> taskResult = task;
            var result = await taskResult;

            // Assert
            await Assert.That(result.IsSuccess).IsTrue();
            await Assert.That(result.Value).IsEqualTo(expectedValue);
        }

        [Test]
        public async Task FromTask_WithSuccessResult_ShouldReturnSuccess()
        {
            // Arrange
            var task = Task.FromResult(Result.Success());

            // Act
            var taskResult = TaskResult.FromTask(task);
            var result = await taskResult;

            // Assert
            await Assert.That(result.IsSuccess).IsTrue();
            await Assert.That(result.Value).IsNotNull();
            await Assert.That(result.Value).IsTypeOf<object>();
        }

        [Test]
        public async Task FromTask_WithFailureResult_ShouldReturnFailure()
        {
            // Arrange
            var task = Task.FromResult(Result.Failure(TestError));

            // Act
            var taskResult = TaskResult.FromTask(task);
            var result = await taskResult;

            // Assert
            await Assert.That(result.IsFailure).IsTrue();
            await Assert.That(result.Error).IsEqualTo(TestError);
        }

        [Test]
        public async Task NonGeneric_Success_ShouldReturnSuccess()
        {
            // Arrange
            var taskResult = TaskResult.Success();

            // Act
            var result = await taskResult;

            // Assert
            await Assert.That(result.IsSuccess).IsTrue();
        }

        [Test]
        public async Task NonGeneric_Failure_ShouldReturnFailure()
        {
            // Arrange
            var taskResult = TaskResult.Failure(TestError);

            // Act
            var result = await taskResult;

            // Assert
            await Assert.That(result.IsFailure).IsTrue();
            await Assert.That(result.Error).IsEqualTo(TestError);
        }
    }
}


