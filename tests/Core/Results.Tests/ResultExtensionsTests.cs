using LightningArc.Results;

namespace LightningArc.Results.Tests
{
    public class ResultExtensionsTests
    {
        private static readonly Error TestError = Error.Application.Internal("Test Error");
        private static readonly Error AnotherError = Error.Application.InvalidParameter(
            "Another Error"
        );

        // --- Tap Tests ---

        [Test]
        public async Task Tap_OnSuccess_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            var result = Result.Success(42);

            // Act
            result.Tap(Action);

            // Assert
            await Assert.That(executed).IsTrue();
            return;

            void Action(int x) => executed = true;
        }

        [Test]
        public async Task Tap_OnFailure_DoesNotExecuteAction()
        {
            // Arrange
            bool executed = false;
            var result = Result<int>.Failure(TestError);

            // Act
            result.Tap(Action);

            // Assert
            await Assert.That(executed).IsFalse();
            return;

            void Action(int x) => executed = true;
        }

        [Test]
        public async Task TapAsync_OnSuccess_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            var result = Result.Success("test");

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

        // --- Map Tests ---

        [Test]
        public async Task Map_OnSuccess_TransformsValue()
        {
            // Arrange
            var result = Result.Success(5);

            // Act
            var mappedResult = result.Map(Mapper);

            // Assert
            await Assert.That(mappedResult.IsSuccess).IsTrue();
            await Assert.That(mappedResult.Value).IsEqualTo("5");
            return;

            static string Mapper(int x) => x.ToString();
        }

        [Test]
        public async Task Map_OnFailure_PropagatesError()
        {
            // Arrange
            var result = Result<int>.Failure(TestError);

            // Act
            var mappedResult = result.Map(Mapper);

            // Assert
            await Assert.That(mappedResult.IsFailure).IsTrue();
            await Assert.That(mappedResult.Error).IsEqualTo(TestError);
            return;

            static string Mapper(int x) => x.ToString();
        }

        // --- Bind Tests ---

        [Test]
        public async Task Bind_OnSuccess_ChainsOperation()
        {
            // Arrange
            var result = Result.Success(10);

            // Act
            var boundResult = result.Bind(Func);

            // Assert
            await Assert.That(boundResult.IsSuccess).IsTrue();
            await Assert.That(boundResult.Value).IsEqualTo(5.0);
            return;

            static Result<double> Func(int x) => Result.Success(x / 2.0);
        }

        [Test]
        public async Task Bind_OnFailure_ShortCircuits()
        {
            // Arrange
            var result = Result<int>.Failure(TestError);

            // Act
            var boundResult = result.Bind(Func);

            // Assert
            await Assert.That(boundResult.IsFailure).IsTrue();
            await Assert.That(boundResult.Error).IsEqualTo(TestError);
            return;

            static Result<double> Func(int x) => Result.Success(x / 2.0);
        }

        // --- Match Tests ---

        [Test]
        public async Task Match_OnSuccess_ExecutesSuccessFunc()
        {
            // Arrange
            var result = Result.Success("ok");

            // Act
            string matchedValue = result.Match(success: s => "SUCCESS", failure: e => "FAILURE");

            // Assert
            await Assert.That(matchedValue).IsEqualTo("SUCCESS");
        }

        [Test]
        public async Task Match_OnFailure_ExecutesFailureFunc()
        {
            // Arrange
            var result = Result<string>.Failure(TestError);

            // Act
            string matchedValue = result.Match(success: s => "SUCCESS", failure: e => "FAILURE");

            // Assert
            await Assert.That(matchedValue).IsEqualTo("FAILURE");
        }

        // --- OnFailure Tests ---

        [Test]
        public async Task OnFailure_OnFailure_ExecutesAction()
        {
            // Arrange
            bool executed = false;
            var result = Result<int>.Failure(TestError);

            // Act
            result.OnFailure(Action);

            // Assert
            await Assert.That(executed).IsTrue();
            return;

            void Action(Error e) => executed = true;
        }

        [Test]
        public async Task OnFailure_OnSuccess_DoesNotExecuteAction()
        {
            // Arrange
            bool executed = false;
            var result = Result.Success(123);

            // Act
            result.OnFailure(Action);

            // Assert
            await Assert.That(executed).IsFalse();
            return;

            void Action(Error e) => executed = true;
        }

        // --- MapError Tests ---

        [Test]
        public async Task MapError_OnFailure_TransformsError()
        {
            // Arrange
            var result = Result<int>.Failure(TestError);

            // Act
            var mappedResult = result.MapError(Func);

            // Assert
            await Assert.That(mappedResult.IsFailure).IsTrue();
            await Assert.That(mappedResult.Error).IsEqualTo(AnotherError);
            return;

            static Error Func(Error e) => AnotherError;
        }

        [Test]
        public async Task MapError_OnSuccess_DoesNotTransform()
        {
            // Arrange
            var result = Result.Success(100);

            // Act
            var mappedResult = result.MapError(Func);

            // Assert
            await Assert.That(mappedResult.IsSuccess).IsTrue();
            await Assert.That(mappedResult.Value).IsEqualTo(100);
            return;

            static Error Func(Error e) => AnotherError;
        }
    }
}


