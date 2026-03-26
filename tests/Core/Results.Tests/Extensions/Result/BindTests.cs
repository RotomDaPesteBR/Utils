using LightningArc.Results;

namespace LightningArc.Results.Tests
{
    public class BindExtensionsTests
    {
        private static readonly Error TestError = Error.Application.Internal("Test Error");

        [Test]
        public async Task Bind_With_Value_OnSuccess_ChainsOperation()
        {
            // Arrange
            Result<int> result = 10;

            // Act
            var boundResult = result.Bind(Func);

            // Assert
            await Assert.That(boundResult.IsSuccess).IsTrue();
            await Assert.That(boundResult.Value).IsEqualTo(5.0);
            return;

            static Result<double> Func(int x) => Result.Success(x / 2.0);
        }

        [Test]
        public async Task Bind_With_Value_OnFailure_ShortCircuits()
        {
            // Arrange
            Result<int> result = TestError;

            // Act
            var boundResult = result.Bind(Func);

            // Assert
            await Assert.That(boundResult.IsFailure).IsTrue();
            await Assert.That(boundResult.Error).IsEqualTo(TestError);
            return;

            static Result<double> Func(int x) => Result.Success(x / 2.0);
        }

        [Test]
        public async Task Bind_With_Value_OnSuccess_ChainsOperation_CanFail()
        {
            // Arrange
            int[] values = [1, 2, 3];
            Result<List<string>> result = values.Select(v => v.ToString()).ToList();

            // Act
            var mappedResult = result.Bind(v =>
            {
                string? value = v.FirstOrDefault();

                return value is not null
                    ? Result.Success(value)
                    : Error.Resource.NotFound($"Não foi encontrado");
            });

            // Assert
            await Assert.That(mappedResult.Value).IsEqualTo("1");
        }

        [Test]
        public async Task Bind_No_Value_OnSuccess_ChainsOperation()
        {
            // Arrange
            Result result = Result.Success();

            // Act
            var boundResult = result.Bind(Func);

            // Assert
            await Assert.That(boundResult.IsSuccess).IsTrue();
            await Assert.That(boundResult.Value).IsEqualTo(5.0);
            return;

            static Result<double> Func() => Result.Success(5.0);
        }

        [Test]
        public async Task BindAsync_With_Value_OnSuccess_ChainsOperation()
        {
            // Arrange
            Result<int> result = 10;

            // Act
            var boundResult = await result.BindAsync(Func);

            // Assert
            await Assert.That(boundResult.IsSuccess).IsTrue();
            await Assert.That(boundResult.Value).IsEqualTo(5.0);
            return;

            static async Task<Result<double>> Func(int x)
            {
                await Task.Delay(1);
                return Result.Success(x / 2.0);
            }
        }

        [Test]
        public async Task BindAsync_No_Value_OnSuccess_ChainsOperation()
        {
            // Arrange
            Result result = Result.Success();

            // Act
            var boundResult = await result.BindAsync(Func);

            // Assert
            await Assert.That(boundResult.IsSuccess).IsTrue();
            await Assert.That(boundResult.Value).IsEqualTo(5.0);
            return;

            static async Task<Result<double>> Func()
            {
                await Task.Delay(1);
                return Result.Success(5.0);
            }
        }
    }
}


