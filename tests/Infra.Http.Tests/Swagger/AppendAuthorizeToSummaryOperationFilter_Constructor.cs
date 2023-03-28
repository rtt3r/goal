using System.Collections.Generic;
using System;
using FluentAssertions;
using Goal.Seedwork.Infra.Http.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Xunit;
using System.Linq;

namespace Goal.Seedwork.Infra.Http.Tests.Swagger
{
    public class AppendAuthorizeToSummaryOperationFilter_Constructor
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
                typeof(MockController).GetMethod(nameof(MockController.Get)));

            //Act
            filter.Apply(operation, context);

            //Assert
            operation.Responses.Should().ContainKey("401");
            operation.Responses.Should().ContainKey("403");
            operation.Summary.Should().Contain("(Auth policies: policy1, policy2; roles: role1, role2)");
        }

        public class MockController : ControllerBase
        {
            [HttpGet]
            [Authorize(Policy = "policy1, policy2", Roles = "role1, role2")]
            public IActionResult Get()
            {
                return Ok();
            }
        }
    }
}