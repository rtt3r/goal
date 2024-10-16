using FluentAssertions;
using Goal.Infra.Http.Controllers;
using Goal.Infra.Http.Controllers.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Goal.Infra.Http.Tests.Controllers;

public class ApiController_ServiceUnavailable
{
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
