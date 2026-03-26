
using LightningArc.Results;
using LightningArc.Results.Localization;
using System.Collections.Generic;

namespace LightningArc.Results.Tests
{
    [NotInParallel]
    public class ErrorTests
    {
        [Test]
        public async Task ErrorDetail_Constructor_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            const string context = "FieldName";
            const string message = "Invalid value";

            // Act
            ErrorDetail errorDetail = new(context, message);

            // Assert
            await Assert.That(errorDetail.Context).IsEqualTo(context);
            await Assert.That(errorDetail.Message).IsEqualTo(message);
        }

        [Test]
        public async Task ErrorDetail_ImplicitConversion_ShouldCreateCorrectly()
        {
            // Arrange
            const string context = "Email";
            const string message = "Email is required";

            // Act
            ErrorDetail errorDetail = (context, message);

            // Assert
            await Assert.That(errorDetail.Context).IsEqualTo(context);
            await Assert.That(errorDetail.Message).IsEqualTo(message);
        }

        [Test]
        public async Task Application_Internal_ShouldCreateCorrectError()
        {
            // Arrange
            const string message = "Something went wrong";
            var details = new List<ErrorDetail> { ("Detail1", "Message1") };

            // Act
            Error error = Error.Application.Internal(message, details);

            // Assert
            await Assert.That(error.Code).IsEqualTo(Error.Application.CodePrefix * 1000 + (int)Error.Application.Codes.Internal);
            await Assert.That(error.Message).IsEqualTo(message);
            await Assert.That(error.Details).IsEquivalentTo(details);
        }

        [Test]
        public async Task Application_InvalidParameter_ShouldCreateCorrectError()
        {
            // Arrange
            const string message = "Invalid ID";

            // Act
            Error error = Error.Application.InvalidParameter(message);

            // Assert
            await Assert.That(error.Code).IsEqualTo(Error.Application.CodePrefix * 1000 + (int)Error.Application.Codes.InvalidParameter);
            await Assert.That(error.Message).IsEqualTo(message);
            await Assert.That(error.Details).IsEmpty();
        }

        [Test]
        public async Task Application_InvalidOperation_ShouldCreateCorrectError()
        {
            LocalizationManager.Configure("pt-BR");

            // Act
            Error error = Error.Application.InvalidOperation();

            // Assert
            await Assert.That(error.Code).IsEqualTo(Error.Application.CodePrefix * 1000 + (int)Error.Application.Codes.InvalidOperation);
            await Assert.That(error.Message).IsEqualTo("A operação solicitada é inválida no estado atual do sistema.");
        }

        [Test]
        public async Task Application_TaskCanceled_ShouldCreateCorrectError()
        {
            // Act
            Error error = Error.Application.TaskCanceled();

            // Assert
            await Assert.That(error.Code).IsEqualTo(Error.Application.CodePrefix * 1000 + (int)Error.Application.Codes.TaskCanceled);
        }

        [Test]
        public async Task Application_NotImplemented_ShouldCreateCorrectError()
        {
            // Act
            Error error = Error.Application.NotImplemented();

            // Assert
            await Assert.That(error.Code).IsEqualTo(Error.Application.CodePrefix * 1000 + (int)Error.Application.Codes.NotImplemented);
        }

        [Test]
        public async Task Validation_InvalidParameter_ShouldCreateCorrectError()
        {
            // Arrange
            LocalizationManager.Configure("en-US");

            // Act
            Error error = Error.Validation.InvalidParameter();

            // Assert
            await Assert.That(error.Code).IsEqualTo(Error.Validation.CodePrefix * 1000 + (int)Error.Validation.Codes.InvalidParameter);
            await Assert.That(error.Message).IsEqualTo("One or more parameters in the input are invalid.");
        }

        [Test]
        public async Task Validation_InvalidParameter_Portuguese_ShouldCreateCorrectError()
        {
            // Arrange
            LocalizationManager.Configure("pt-BR");

            // Act
            Error error = Error.Validation.InvalidParameter();

            // Assert
            await Assert.That(error.Message).IsEqualTo("Um ou mais parâmetros na entrada são inválidos.");
        }

        [Test]
        public async Task Code_Property_ShouldCalculateCorrectly()
        {
            // This test confirms the general calculation logic using one of the specific error types.
            // Arrange
            Error error = Error.Application.Internal();
            int expectedCode = Error.Application.CodePrefix * 1000 + (int)Error.Application.Codes.Internal;

            // Act
            int actualCode = error.Code;

            // Assert
            await Assert.That(actualCode).IsEqualTo(expectedCode);
        }
    }
}


