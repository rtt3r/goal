using FluentAssertions;
using Goal.Application.Commands;
using Goal.Infra.Crosscutting.Notifications;
using Goal.Infra.Http.Controllers;
using Goal.Infra.Http.Controllers.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Goal.Infra.Http.Tests.Controllers;

public class ApiController_CommandFailure
{
    [Fact]
    public void CommandFailure_WithInputValidation_ReturnsBadRequestResult()
    {
        // Arrange
        var controller = new TestController();
        ICommandResult commandResult = CommandResult.Failure(Notification.InputValidation("001", "Invalid input", "body"));

        // Act
        ActionResult actionResult = controller.CommandFailureWrapper(commandResult);

        // Assert
        actionResult.Should().BeOfType<BadRequestObjectResult>();
        actionResult.As<BadRequestObjectResult>().StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        actionResult.As<BadRequestObjectResult>().Value.Should().Be(commandResult.Notifications);
    }

    [Fact]
    public void CommandFailure_WithResourceNotFound_ReturnsNotFoundResult()
    {
        // Arrange
        var controller = new TestController();
        ICommandResult commandResult = CommandResult.Failure(Notification.ResourceNotFound("001", "Not found"));

        // Act
        ActionResult actionResult = controller.CommandFailureWrapper(commandResult);

        // Assert
        actionResult.Should().BeOfType<NotFoundObjectResult>();
        actionResult.As<NotFoundObjectResult>().StatusCode.Should().Be(StatusCodes.Status404NotFound);
        actionResult.As<NotFoundObjectResult>().Value.Should().Be(commandResult.Notifications);
    }

    [Fact]
    public void CommandFailure_WithDomainViolation_ReturnsUnprocessableEntityResult()
    {
        // Arrange
        var controller = new TestController();
        ICommandResult commandResult = CommandResult.Failure(Notification.DomainViolation("001", "Domain violation"));

        // Act
        ActionResult actionResult = controller.CommandFailureWrapper(commandResult);

        // Assert
        actionResult.Should().BeOfType<UnprocessableEntityObjectResult>();
        actionResult.As<UnprocessableEntityObjectResult>().StatusCode.Should().Be(StatusCodes.Status422UnprocessableEntity);
        actionResult.As<UnprocessableEntityObjectResult>().Value.Should().Be(commandResult.Notifications);
    }

    [Fact]
    public void CommandFailure_WithExternalError_ReturnsServiceUnavailableResult()
    {
        // Arrange
        var controller = new TestController();
        ICommandResult commandResult = CommandResult.Failure(Notification.ExternalError("001", "External error"));

        // Act
        ActionResult actionResult = controller.CommandFailureWrapper(commandResult);

        // Assert
        actionResult.Should().BeOfType<ServiceUnavailableObjectResult>();
        actionResult.As<ServiceUnavailableObjectResult>().StatusCode.Should().Be(StatusCodes.Status503ServiceUnavailable);
        actionResult.As<ServiceUnavailableObjectResult>().Value.Should().Be(commandResult.Notifications);
    }

    [Fact]
    public void CommandFailure_WithNoFailures_ReturnsInternalServerErrorResult()
    {
        // Arrange
        var controller = new TestController();
        ICommandResult commandResult = CommandResult.Failure(Notification.InternalError("001", "Internal error"));

        // Act
        ActionResult actionResult = controller.CommandFailureWrapper(commandResult);

        // Assert
        actionResult.Should().BeOfType<InternalServerErrorObjectResult>();
        actionResult.As<InternalServerErrorObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        actionResult.As<InternalServerErrorObjectResult>().Value.Should().Be(commandResult.Notifications);
    }

    public class TestController : ApiController
    {
        public ActionResult CommandFailureWrapper(ICommandResult result)
            => CommandFailure(result);
    }

    public class Test
    {
        public string? Name { get; set; }
        public int Age { get; set; }
    }
}
