using FluentAssertions;
using Goal.Seedwork.Infra.Http.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Xunit;

namespace Goal.Seedwork.Infra.Http.Tests.Swagger;

public class AppendAuthorizeToSummaryOperationFilter_Apply
{
    [Fact]
    public void ApplyAuthorizationInformationToOperation()
    {
        //Arrange 
        var filter = new AppendAuthorizeToSummaryOperationFilter();
        var operation = new OpenApiOperation();
        var context = new OperationFilterContext(
            new ApiDescription(),
            new SchemaGenerator(new SchemaGeneratorOptions(), null),
            null,
            typeof(AuthorizedController).GetMethod(nameof(AuthorizedController.Get)));

        //Act
        filter.Apply(operation, context);

        //Assert
        operation.Responses.Should().ContainKey("401");
        operation.Responses.Should().ContainKey("403");
        operation.Summary.Should().Contain("(Auth policies: policy1, policy2; roles: role1, role2)");
    }

    [Fact]
    public void ApplyAnonymousInformationToOperation()
    {
        //Arrange 
        var filter = new AppendAuthorizeToSummaryOperationFilter();
        var operation = new OpenApiOperation();
        var context = new OperationFilterContext(
            new ApiDescription(),
            new SchemaGenerator(new SchemaGeneratorOptions(), null),
            null,
            typeof(AnonymousController).GetMethod(nameof(AnonymousController.Get)));

        //Act
        filter.Apply(operation, context);

        //Assert
        operation.Responses.Should().HaveCount(0);
        operation.Summary.Should().BeNullOrWhiteSpace();
    }

    public class AuthorizedController : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = "policy1, policy2", Roles = "role1, role2")]
        public IActionResult Get() => Ok();
    }

    public class AnonymousController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => Ok();
    }
}