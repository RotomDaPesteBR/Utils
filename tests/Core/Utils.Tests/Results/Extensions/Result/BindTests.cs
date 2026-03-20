using LightningArc.Utils.Results;
using Xunit;

namespace LightningArc.Utils.Tests.Results
{
    public class BindExtensionsTests
    {
        private static readonly Error TestError = Error.Application.Internal("Test Error");

        [Fact]
        public void Bind_With_Value_OnSuccess_ChainsOperation()
        {
            // Arrange
            Result<int> result = 10;
            static Result<double> func(int x) => Result.Success(x / 2.0);

            // Act
            var boundResult = result.Bind(func);

            // Assert
            Assert.True(boundResult.IsSuccess);
            Assert.Equal(5.0, boundResult.Value);
        }

        [Fact]
        public void Bind_With_Value_OnFailure_ShortCircuits()
        {
            // Arrange
            Result<int> result = TestError;
            static Result<double> func(int x) => Result.Success(x / 2.0);

            // Act
            var boundResult = result.Bind(func);

            // Assert
            Assert.True(boundResult.IsFailure);
            Assert.Equal(TestError, boundResult.Error);
        }

        [Fact]
        public void Bind_With_Value_OnSuccess_ChainsOperation_CanFail()
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
            Assert.Equal("1", mappedResult.Value);
        }

        [Fact]
        public void Bind_No_Value_OnSuccess_ChainsOperation()
        {
            // Arrange
            Result result = Result.Success();
            static Result<double> func() => Result.Success(5.0);

            // Act
            var boundResult = result.Bind(func);

            // Assert
            Assert.True(boundResult.IsSuccess);
            Assert.Equal(5.0, boundResult.Value);
        }

        [Fact]
        public async Task BindAsync_With_Value_OnSuccess_ChainsOperation()
        {
            // Arrange
            Result<int> result = 10;
            static async Task<Result<double>> func(int x)
            {
                await Task.Delay(1);
                return Result.Success(x / 2.0);
            }

            // Act
            var boundResult = await result.BindAsync(func);

            // Assert
            Assert.True(boundResult.IsSuccess);
            Assert.Equal(5.0, boundResult.Value);
        }

        [Fact]
        public async Task BindAsync_No_Value_OnSuccess_ChainsOperation()
        {
            // Arrange
            Result result = Result.Success();
            static async Task<Result<double>> func()
            {
                await Task.Delay(1);
                return Result.Success(5.0);
            }

            // Act
            var boundResult = await result.BindAsync(func);

            // Assert
            Assert.True(boundResult.IsSuccess);
            Assert.Equal(5.0, boundResult.Value);
        }
    }
}
