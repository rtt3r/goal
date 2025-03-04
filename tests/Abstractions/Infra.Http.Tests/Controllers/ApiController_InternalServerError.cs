using FluentAssertions;
using Goal.Infra.Http.Controllers;
using Goal.Infra.Http.Controllers.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Goal.Infra.Http.Tests.Controllers;

public class ApiController_InternalServerError
{
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
