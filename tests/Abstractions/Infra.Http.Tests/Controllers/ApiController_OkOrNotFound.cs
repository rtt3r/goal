using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Goal.Infra.Http.Controllers;
using Xunit;

namespace Goal.Infra.Http.Tests.Controllers;

public class ApiController_OkOrNotFound
{
    [Fact]
    public void OkOrNotFound_WithNonNullObject_ReturnsOkResult()
    {
        // Arrange
        var controller = new TestController();
        object value = new { Name = "John", Age = 30 };

        // Act
        IActionResult result = controller.OkOrNotFoundWrapper(value);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        result.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
        result.As<OkObjectResult>().Value.Should().BeSameAs(value);
    }

    [Fact]
    public void OkOrNotFound_WithNullObject_ReturnsNotFoundResult()
    {
        // Arrange
        var controller = new TestController();
        object? value = null;

        // Act
        IActionResult result = controller.OkOrNotFoundWrapper(value);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
        result.As<NotFoundResult>().StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public void OkOrNotFound_WithNonNullObjectAndMessage_ReturnsOkResult()
    {
        // Arrange
        var controller = new TestController();
        object value = new { Name = "John", Age = 30 };
        string message = "The value was found successfully.";

        // Act
        IActionResult result = controller.OkOrNotFoundWrapper(value, message);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        result.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
        result.As<OkObjectResult>().Value.Should().BeSameAs(value);
    }

    [Fact]
    public void OkOrNotFound_WithNullObjectAndMessage_ReturnsNotFoundObjectResult()
    {
        // Arrange
        var controller = new TestController();
        object? value = null;
        string message = "The value was not found.";

        // Act
        IActionResult result = controller.OkOrNotFoundWrapper(value, message);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        result.As<NotFoundObjectResult>().StatusCode.Should().Be(StatusCodes.Status404NotFound);
        result.As<NotFoundObjectResult>().Value.Should().Be(message);
    }

    [Fact]
    public void OkOrNotFound_WithNonNullValue_ReturnsOkResult()
    {
        // Arrange
        var controller = new TestController();
        Test value = new() { Name = "John", Age = 30 };

        // Act
        ActionResult<Test> result = controller.OkOrNotFoundWrapper<Test>(value);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
        result.Result.As<OkObjectResult>().Value.Should().BeSameAs(value);
    }

    [Fact]
    public void OkOrNotFound_WithNullValue_ReturnsNotFoundResult()
    {
        // Arrange
        var controller = new TestController();
        Test? value = null;

        // Act
        ActionResult<Test> result = controller.OkOrNotFoundWrapper<Test>(value);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
        result.Result.As<NotFoundResult>().StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public void OkOrNotFound_WithNonNullValueAndMessage_ReturnsOkResult()
    {
        // Arrange
        var controller = new TestController();
        Test value = new() { Name = "John", Age = 30 };
        string message = "The value was found successfully.";

        // Act
        ActionResult<Test> result = controller.OkOrNotFoundWrapper<Test>(value, message);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        result.Result.As<OkObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
        result.Result.As<OkObjectResult>().Value.Should().BeSameAs(value);
    }

    [Fact]
    public void OkOrNotFound_WithNullValueAndMessage_ReturnsNotFoundObjectResult()
    {
        // Arrange
        var controller = new TestController();
        Test? value = null;
        string message = "The value was not found.";

        // Act
        ActionResult<Test> result = controller.OkOrNotFoundWrapper<Test>(value, message);

        // Assert
        result.Result.Should().BeOfType<NotFoundObjectResult>();
        result.Result.As<NotFoundObjectResult>().StatusCode.Should().Be(StatusCodes.Status404NotFound);
        result.Result.As<NotFoundObjectResult>().Value.Should().Be("The value was not found.");
    }

    public class TestController : ApiController
    {
        public IActionResult OkOrNotFoundWrapper(object? value, string message)
            => OkOrNotFound(value, message);

        public IActionResult OkOrNotFoundWrapper(object? value)
            => OkOrNotFound(value);

        public ActionResult<T> OkOrNotFoundWrapper<T>(T? value, string message)
            => OkOrNotFound<T>(value, message);

        public ActionResult<T> OkOrNotFoundWrapper<T>(T? value)
            => OkOrNotFound<T>(value);
    }

    public class Test
    {
        public string? Name { get; set; }
        public int Age { get; set; }
    }
}
