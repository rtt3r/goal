using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Notifications;
using Goal.Infra.Http.Controllers;
using Goal.Infra.Http.Controllers.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Goal.Infra.Http.Tests.Controllers;

public class ApiController_ServiceUnavailable
{
    [Fact]
    public void WithObjectResult_ReturnsServiceUnavailableObjectResult()
    {
        // Arrange
        var controller = new TestController();
        var result = Notification.ExternalError("001", "External error");

        // Act
        ActionResult actionResult = controller.ServiceUnavailableWrapper(result);

        // Assert
        actionResult.Should().BeOfType<ServiceUnavailableObjectResult>();
        actionResult.As<ServiceUnavailableObjectResult>().StatusCode.Should().Be(StatusCodes.Status503ServiceUnavailable);
        actionResult.As<ServiceUnavailableObjectResult>().Value.Should().Be(result);
    }

    [Fact]
    public void ReturnsServiceUnavailableResult()
    {
        // Arrange
        var controller = new TestController();

        // Act
        ActionResult actionResult = controller.ServiceUnavailableWrapper();

        // Assert
        actionResult.Should().BeOfType<ServiceUnavailableResult>();
        actionResult.As<ServiceUnavailableResult>().StatusCode.Should().Be(StatusCodes.Status503ServiceUnavailable);
    }

    public class TestController : ApiController
    {
        public ActionResult ServiceUnavailableWrapper(object result)
            => ServiceUnavailable(result);

        public ActionResult ServiceUnavailableWrapper()
            => ServiceUnavailable();
    }
}
