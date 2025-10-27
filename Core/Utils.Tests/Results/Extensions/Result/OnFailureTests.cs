using LightningArc.Utils.Results;
using Xunit;

namespace LightningArc.Utils.Tests.Results
{
    public class OnFailureExtensionsTests
    {
        private static readonly Error TestError = Error.Application.Internal("Test Error");

        [Fact]
        public void OnFailure_With_Value_OnFailure_ExecutesAction()
        {
            // Arrange
            var executed = false;
            Result<int> result = TestError;
            void action(Error e) => executed = true;

            // Act
            result.OnFailure(action);

            // Assert
            Assert.True(executed);
        }

        [Fact]
        public void OnFailure_With_Value_OnSuccess_DoesNotExecuteAction()
        {
            // Arrange
            var executed = false;
            Result<int> result = 123;
            void action(Error e) => executed = true;

            // Act
            result.OnFailure(action);

            // Assert
            Assert.False(executed);
        }

        [Fact]
        public void OnFailure_With_Value_And_Parameterless_Action_OnFailure_ExecutesAction()
        {
            // Arrange
            var executed = false;
            Result<int> result = TestError;
            void action(Error error) => executed = true;

            // Act
            result.OnFailure(action);

            // Assert
            Assert.True(executed);
        }

        [Fact]
        public void OnFailure_No_Value_OnFailure_ExecutesAction()
        {
            // Arrange
            var executed = false;
            Result result = TestError;
            void action() => executed = true;

            // Act
            result.OnFailure((Action)action);

            // Assert
            Assert.True(executed);
        }

        [Fact]
        public async Task OnFailureAsync_With_Value_OnFailure_ExecutesAction()
        {
            // Arrange
            var executed = false;
            Result<int> result = TestError;
            async Task action(Error e)
            {
                await Task.Delay(1);
                executed = true;
            }

            // Act
            await result.OnFailureAsync(action);

            // Assert
            Assert.True(executed);
        }

        [Fact]
        public async Task OnFailureAsync_With_Value_And_Parameterless_Action_OnFailure_ExecutesAction()
        {
            // Arrange
            var executed = false;
            Result<int> result = TestError;
            async Task action(Error error)
            {
                await Task.Delay(1);
                executed = true;
            }

            // Act
            await result.OnFailureAsync(action);

            // Assert
            Assert.True(executed);
        }

        [Fact]
        public async Task OnFailureAsync_No_Value_OnFailure_ExecutesAction()
        {
            // Arrange
            var executed = false;
            Result result = TestError;
            async Task action()
            {
                await Task.Delay(1);
                executed = true;
            }

            // Act
            await result.OnFailureAsync((Func<Task>)action);

            // Assert
            Assert.True(executed);
        }
    }
}