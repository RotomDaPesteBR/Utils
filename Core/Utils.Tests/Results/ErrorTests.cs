
using LightningArc.Utils.Results;
using LightningArc.Utils.Results.Localization;
using System.Collections.Generic;
using Xunit;

namespace LightningArc.Utils.Tests.Results
{
    public class ErrorTests
    {
        [Fact]
        public void ErrorDetail_Constructor_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            const string context = "FieldName";
            const string message = "Invalid value";

            // Act
            var errorDetail = new ErrorDetail(context, message);

            // Assert
            Assert.Equal(context, errorDetail.Context);
            Assert.Equal(message, errorDetail.Message);
        }

        [Fact]
        public void ErrorDetail_ImplicitConversion_ShouldCreateCorrectly()
        {
            // Arrange
            const string context = "Email";
            const string message = "Email is required";

            // Act
            ErrorDetail errorDetail = (context, message);

            // Assert
            Assert.Equal(context, errorDetail.Context);
            Assert.Equal(message, errorDetail.Message);
        }

        [Fact]
        public void Application_Internal_ShouldCreateCorrectError()
        {
            // Arrange
            const string message = "Something went wrong";
            var details = new List<ErrorDetail> { ("Detail1", "Message1") };

            // Act
            var error = Error.Application.Internal(message, details);

            // Assert
            Assert.Equal(Error.Application.CodePrefix * 1000 + (int)Error.Application.Codes.Internal, error.Code);
            Assert.Equal(message, error.Message);
            Assert.Equal(details, error.Details);
        }

        [Fact]
        public void Application_InvalidParameter_ShouldCreateCorrectError()
        {
            // Arrange
            const string message = "Invalid ID";

            // Act
            var error = Error.Application.InvalidParameter(message);

            // Assert
            Assert.Equal(Error.Application.CodePrefix * 1000 + (int)Error.Application.Codes.InvalidParameter, error.Code);
            Assert.Equal(message, error.Message);
            Assert.Empty(error.Details);
        }

        [Fact]
        public void Application_InvalidOperation_ShouldCreateCorrectError()
        {
            LocalizationManager.Configure("pt-BR");

            // Act
            var error = Error.Application.InvalidOperation();

            // Assert
            Assert.Equal(Error.Application.CodePrefix * 1000 + (int)Error.Application.Codes.InvalidOperation, error.Code);
            Assert.Equal("A operação solicitada é inválida no estado atual do sistema.", error.Message);
        }

        [Fact]
        public void Application_TaskCanceled_ShouldCreateCorrectError()
        {
            // Act
            var error = Error.Application.TaskCanceled();

            // Assert
            Assert.Equal(Error.Application.CodePrefix * 1000 + (int)Error.Application.Codes.TaskCanceled, error.Code);
        }

        [Fact]
        public void Application_NotImplemented_ShouldCreateCorrectError()
        {
            // Act
            var error = Error.Application.NotImplemented();

            // Assert
            Assert.Equal(Error.Application.CodePrefix * 1000 + (int)Error.Application.Codes.NotImplemented, error.Code);
        }

        [Fact]
        public void Code_Property_ShouldCalculateCorrectly()
        {
            // This test confirms the general calculation logic using one of the specific error types.
            // Arrange
            var error = Error.Application.Internal();
            var expectedCode = Error.Application.CodePrefix * 1000 + (int)Error.Application.Codes.Internal;

            // Act
            var actualCode = error.Code;

            // Assert
            Assert.Equal(expectedCode, actualCode);
        }
    }
}
