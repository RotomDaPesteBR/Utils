
using System;
using LightningArc.Utils.Results;
using Xunit;

namespace LightningArc.Utils.Tests.Results
{
    public class SuccessTests
    {
        [Fact]
        public void Ok_ShouldCreateOkSuccess_WithDefaultMessage()
        {
            // Act
            var success = Success.Ok();

            // Assert
            Assert.IsType<Success.OkSuccess>(success);
            Assert.Equal(100, success.Code);
            Assert.Equal("Operação concluída com sucesso.", success.Message);
        }

        [Fact]
        public void Ok_ShouldCreateOkSuccess_WithCustomMessage()
        {
            // Arrange
            const string message = "Custom OK message";

            // Act
            var success = Success.Ok(message);

            // Assert
            Assert.IsType<Success.OkSuccess>(success);
            Assert.Equal(100, success.Code);
            Assert.Equal(message, success.Message);
        }

        [Fact]
        public void Created_ShouldCreateCreatedSuccess_WithDefaultMessage()
        {
            // Act
            var success = Success.Created();

            // Assert
            Assert.IsType<Success.CreatedSuccess>(success);
            Assert.Equal(101, success.Code);
            Assert.Equal("Recurso criado com sucesso.", success.Message);
        }

        [Fact]
        public void Created_ShouldCreateCreatedSuccess_WithCustomMessage()
        {
            // Arrange
            const string message = "Item was created";

            // Act
            var success = Success.Created(message);

            // Assert
            Assert.IsType<Success.CreatedSuccess>(success);
            Assert.Equal(101, success.Code);
            Assert.Equal(message, success.Message);
        }

        [Fact]
        public void Accepted_ShouldCreateAcceptedSuccess_WithDefaultMessage()
        {
            // Act
            var success = Success.Accepted();

            // Assert
            Assert.IsType<Success.AcceptedSuccess>(success);
            Assert.Equal(102, success.Code);
            Assert.Equal("A solicitação foi aceita para processamento.", success.Message);
        }

        [Fact]
        public void Accepted_ShouldCreateAcceptedSuccess_WithCustomMessage()
        {
            // Arrange
            const string message = "Request accepted";

            // Act
            var success = Success.Accepted(message);

            // Assert
            Assert.IsType<Success.AcceptedSuccess>(success);
            Assert.Equal(102, success.Code);
            Assert.Equal(message, success.Message);
        }

        [Fact]
        public void NoContent_ShouldCreateNoContentSuccess_WithNullMessage()
        {
            // Act
            var success = Success.NoContent();

            // Assert
            Assert.IsType<Success.NoContentSuccess>(success);
            Assert.Equal(103, success.Code);
            Assert.Null(success.Message);
        }

        [Fact]
        public void NoContent_ShouldCreateNoContentSuccess_WithCustomMessage()
        {
            // Arrange
            const string message = "Should be null, but testing anyway";

            // Act
            var success = Success.NoContent(message);

            // Assert
            Assert.IsType<Success.NoContentSuccess>(success);
            Assert.Equal(103, success.Code);
            Assert.Equal(message, success.Message);
        }
    }
}
