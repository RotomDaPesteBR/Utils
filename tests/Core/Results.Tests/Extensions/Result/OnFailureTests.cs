using LightningArc.Results;

namespace LightningArc.Results.Tests
{
    public class OnFailureExtensionsTests
    {
        private static readonly Error TestError = Error.Application.Internal("Test Error");

        [Test]
        public async Task OnFailure_With_Value_OnFailure_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            Result<int> result = TestError;

            // Act
            result.OnFailure(Action);

            // Assert
            await Assert.That(executed).IsTrue();
            return;

            void Action(Error e) => executed = true;
        }

        [Test]
        public async Task OnFailure_With_Value_OnSuccess_DoesNotExecuteAction()
        {
            // Arrange
            bool executed = false;
            Result<int> result = 123;

            // Act
            result.OnFailure(Action);

            // Assert
            await Assert.That(executed).IsFalse();
            return;

            void Action(Error e) => executed = true;
        }

        [Test]
        public async Task OnFailure_With_Value_And_Parameterless_Action_OnFailure_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            Result<int> result = TestError;

            // Act
            result.OnFailure(Action);

            // Assert
            await Assert.That(executed).IsTrue();
            return;

            void Action(Error error) => executed = true;
        }

        [Test]
        public async Task OnFailure_No_Value_OnFailure_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            Result result = TestError;

            // Act
            result.OnFailure((Action)Action);

            // Assert
            await Assert.That(executed).IsTrue();
            return;

            void Action() => executed = true;
        }

        [Test]
        public async Task OnFailureAsync_With_Value_OnFailure_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            Result<int> result = TestError;

            // Act
            await result.OnFailureAsync(Action);

            // Assert
            await Assert.That(executed).IsTrue();
            return;

            async Task Action(Error e)
            {
                await Task.Delay(1);
                executed = true;
            }
        }

        [Test]
        public async Task OnFailureAsync_With_Value_And_Parameterless_Action_OnFailure_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            Result<int> result = TestError;

            // Act
            await result.OnFailureAsync(Action);

            // Assert
            await Assert.That(executed).IsTrue();
            return;

            async Task Action(Error error)
            {
                await Task.Delay(1);
                executed = true;
            }
        }

        [Test]
        public async Task OnFailureAsync_No_Value_OnFailure_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            Result result = TestError;

            // Act
            await result.OnFailureAsync((Func<Task>)Action);

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


