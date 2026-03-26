using LightningArc.Results;

namespace LightningArc.Results.Tests
{
    public class TapExtensionsTests
    {
        private static readonly Error TestError = Error.Application.Internal("Test Error");

        [Test]
        public async Task Tap_With_Value_OnSuccess_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            Result<int> result = 42;

            // Act
            result.Tap(Action);

            // Assert
            await Assert.That(executed).IsTrue();
            return;

            void Action(int x) => executed = true;
        }

        [Test]
        public async Task Tap_With_Value_OnFailure_DoesNotExecuteAction()
        {
            // Arrange
            bool executed = false;
            Result<int> result = TestError;

            // Act
            result.Tap(Action);

            // Assert
            await Assert.That(executed).IsFalse();
            return;

            void Action(int x) => executed = true;
        }

        [Test]
        public async Task Tap_With_Value_And_Parameterless_Action_OnSuccess_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            Result<int> result = 42;

            // Act
            result.Tap(Action);

            // Assert
            await Assert.That(executed).IsTrue();
            return;

            void Action(int value) => executed = true;
        }

        [Test]
        public async Task Tap_No_Value_OnSuccess_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            Result result = Result.Success();

            // Act
            result.Tap((Action)Action);

            // Assert
            await Assert.That(executed).IsTrue();
            return;

            void Action() => executed = true;
        }

        [Test]
        public async Task TapAsync_With_Value_OnSuccess_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            Result<string> result = "test";

            // Act
            await result.TapAsync(Action);

            // Assert
            await Assert.That(executed).IsTrue();
            return;

            async Task Action(string s)
            {
                await Task.Delay(1);
                executed = true;
            }
        }

        [Test]
        public async Task TapAsync_With_Value_And_Parameterless_Action_OnSuccess_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            Result<string> result = "test";

            // Act
            await result.TapAsync(Action);

            // Assert
            await Assert.That(executed).IsTrue();
            return;

            async Task Action(string value)
            {
                await Task.Delay(1);
                executed = true;
            }
        }

        [Test]
        public async Task TapAsync_No_Value_OnSuccess_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            Result result = Result.Success();

            // Act
            await result.TapAsync(Action);

            // Assert
            await Assert.That(executed).IsTrue();
            return;

            async Task Action()
            {
                await Task.Delay(1);
                executed = true;
            }
        }
    }
}


