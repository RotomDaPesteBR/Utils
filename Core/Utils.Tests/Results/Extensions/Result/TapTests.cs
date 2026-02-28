using LightningArc.Utils.Results;
using Xunit;

namespace LightningArc.Utils.Tests.Results
{
    public class TapExtensionsTests
    {
        private static readonly Error TestError = Error.Application.Internal("Test Error");

        [Fact]
        public void Tap_With_Value_OnSuccess_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            Result<int> result = 42;
            void action(int x) => executed = true;

            // Act
            result.Tap(action);

            // Assert
            Assert.True(executed);
        }

        [Fact]
        public void Tap_With_Value_OnFailure_DoesNotExecuteAction()
        {
            // Arrange
            bool executed = false;
            Result<int> result = TestError;
            void action(int x) => executed = true;

            // Act
            result.Tap(action);

            // Assert
            Assert.False(executed);
        }

        [Fact]
        public void Tap_With_Value_And_Parameterless_Action_OnSuccess_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            Result<int> result = 42;
            void action(int value) => executed = true;

            // Act
            result.Tap(action);

            // Assert
            Assert.True(executed);
        }

        [Fact]
        public void Tap_No_Value_OnSuccess_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            Result result = Result.Success();
            void action() => executed = true;

            // Act
            result.Tap((Action)action);

            // Assert
            Assert.True(executed);
        }

        [Fact]
        public async Task TapAsync_With_Value_OnSuccess_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            Result<string> result = "test";
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

        [Fact]
        public async Task TapAsync_With_Value_And_Parameterless_Action_OnSuccess_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            Result<string> result = "test";
            async Task action(string value)
            {
                await Task.Delay(1);
                executed = true;
            }

            // Act
            await result.TapAsync(action);

            // Assert
            Assert.True(executed);
        }

        [Fact]
        public async Task TapAsync_No_Value_OnSuccess_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            Result result = Result.Success();
            async Task action()
            {
                await Task.Delay(1);
                executed = true;
            }

            // Act
            await result.TapAsync(action);

            // Assert
            Assert.True(executed);
        }
    }
}