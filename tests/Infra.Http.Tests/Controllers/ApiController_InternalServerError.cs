using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Notifications;
using Goal.Seedwork.Infra.Http.Controllers;
using Goal.Seedwork.Infra.Http.Controllers.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Goal.Seedwork.Infra.Http.Tests.Controllers;

public class ApiController_InternalServerError
{
    [Fact]
    public void WithObjectResult_ReturnsInternalServerErrorObjectResult()
    {
        // Arrange
        var controller = new TestController();
        var result = Notification.InternalError("001", "Internal error");

        // Act
        ActionResult actionResult = controller.InternalServerErrorWrapper(result);

        // Assert
        actionResult.Should().BeOfType<InternalServerErrorObjectResult>();
        actionResult.As<InternalServerErrorObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        actionResult.As<InternalServerErrorObjectResult>().Value.Should().Be(result);
    }

    [Fact]
    public void ReturnsInternalServerErrorResult()
    {
        // Arrange
        var controller = new TestController();

        // Act
        ActionResult actionResult = controller.InternalServerErrorWrapper();

        // Assert
        actionResult.Should().BeOfType<InternalServerErrorResult>();
        actionResult.As<InternalServerErrorResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }

    public class TestController : ApiController
    {
        public ActionResult InternalServerErrorWrapper(object result)
            => InternalServerError(result);

        public ActionResult InternalServerErrorWrapper()
            => InternalServerError();
    }
}
