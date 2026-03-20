
using LightningArc.Utils.Results;
using Xunit;

namespace LightningArc.Utils.Tests.Results
{
    public class ResultTests
    {
        private static readonly Error TestError = Error.Application.Internal("Test Error");

        [Fact]
        public void IsSuccess_ShouldBeTrue_ForSuccessResult()
        {
            // Arrange
            Result result = Result.Success();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
        }

        [Fact]
        public void IsFailure_ShouldBeTrue_ForFailureResult()
        {
            // Arrange
            Result result = Result.Failure(TestError);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
        }

        [Fact]
        public void Error_ShouldThrowException_WhenResultIsSuccess()
        {
            // Arrange
            Result result = Result.Success();

            // Act & Assert
            Assert.Throws<ResultAccessFailedException>(() => result.Error);
        }

        [Fact]
        public void SuccessDetails_ShouldThrowException_WhenResultIsFailure()
        {
            // Arrange
            Result result = Result.Failure(TestError);

            // Act & Assert
            Assert.Throws<ResultAccessFailedException>(() => result.SuccessDetails);
        }

        [Fact]
        public void Value_ShouldThrowException_WhenResultIsFailure()
        {
            // Arrange
            var result = Result<int>.Failure(TestError);

            // Act & Assert
            Assert.Throws<ResultAccessFailedException>(() => result.Value);
        }

        [Fact]
        public void ImplicitConversion_FromError_ShouldCreateFailureResult()
        {
            // Arrange
            Result result = TestError;

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(TestError, result.Error);
        }

        [Fact]
        public void ImplicitConversion_FromValue_ShouldCreateSuccessResult()
        {
            // Arrange
            const int value = 42;
            Result<int> result = value;

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(value, result.Value);
        }

        [Fact]
        public void Success_FactoryMethods_ShouldCreateSuccessResults()
        {
            // Assert
            Assert.IsType<Success.OkSuccess>(Result.Success().SuccessDetails);
            Assert.IsType<Success.CreatedSuccess>(Result.Created().SuccessDetails);
            Assert.IsType<Success.AcceptedSuccess>(Result.Accepted().SuccessDetails);
            Assert.IsType<Success.NoContentSuccess>(Result.NoContent().SuccessDetails);
        }

        [Fact]
        public void SuccessOfT_FactoryMethods_ShouldCreateSuccessResults()
        {
            // Arrange
            const string value = "test";

            // Assert
            Assert.IsType<Success<string>.OkSuccess>(Result.Success(value).SuccessDetails);
            Assert.IsType<Success<string>.CreatedSuccess>(Result.Created(value).SuccessDetails);
            Assert.IsType<Success<string>.AcceptedSuccess>(Result.Accepted(value).SuccessDetails);
            Assert.IsType<Success<string>.NoContentSuccess>(Result.NoContent(value).SuccessDetails);
        }

        [Fact]
        public void Code_ShouldReturnCorrectCode_ForSuccess()
        {
            // Arrange
            Result okResult = Result.Success();
            Result createdResult = Result.Created();

            // Assert
            Assert.Equal(Success.Ok().Code, okResult.Code);
            Assert.Equal(Success.Created().Code, createdResult.Code);
        }

        [Fact]
        public void Code_ShouldReturnCorrectCode_ForFailure()
        {
            // Arrange
            Result result = Result.Failure(TestError);

            // Assert
            Assert.Equal(TestError.Code, result.Code);
        }

        [Fact]
        public void Value_ShouldReturnCorrectValue_ForSuccessResultOfT()
        {
            // Arrange
            const double value = 3.14;
            var result = Result.Success(value);

            // Assert
            Assert.Equal(value, result.Value);
        }
    }
}
