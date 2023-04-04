using FluentAssertions;
using Goal.Seedwork.Infra.Http.Swagger;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Xunit;

namespace Goal.Seedwork.Infra.Http.Tests.Swagger;

public class LowerCaseDocumentFilter_Apply
{
    [Fact]
    public void Should_Lowercase_Path_Keys()
    {
        // Arrange
        var filter = new LowerCaseDocumentFilter();
        var swaggerDoc = new OpenApiDocument { Paths = new OpenApiPaths() };
        var context = new DocumentFilterContext(
            new[] { new ApiDescription() },
            new SchemaGenerator(new SchemaGeneratorOptions(), null),
            null);

        swaggerDoc.Paths.Add("/Test/Path/{Param1}/SubPath/{Param2}/", new OpenApiPathItem());

        // Act
        filter.Apply(swaggerDoc, context);

        // Assert
        swaggerDoc.Paths.Keys.Should().ContainSingle("/test/path/{Param1}/subpath/{Param2}/");
    }

    [Fact]
    public void Should_Not_Create_New_Path_Items()
    {
        // Arrange
        var filter = new LowerCaseDocumentFilter();
        var swaggerDoc = new OpenApiDocument { Paths = new OpenApiPaths() };
        var context = new DocumentFilterContext(
            new[] { new ApiDescription() },
            new SchemaGenerator(new SchemaGeneratorOptions(), null),
            null);

        swaggerDoc.Paths.Add("/Test/Path", new OpenApiPathItem());

        // Act
        filter.Apply(swaggerDoc, context);

        // Assert
        swaggerDoc.Paths.Keys.Should().ContainSingle("/test/path");
    }

    [Fact]
    public void Should_Remove_Original_Path_Items()
    {
        // Arrange
        var filter = new LowerCaseDocumentFilter();
        var swaggerDoc = new OpenApiDocument { Paths = new OpenApiPaths() };
        var context = new DocumentFilterContext(
            new[] { new ApiDescription() },
            new SchemaGenerator(new SchemaGeneratorOptions(), null),
            null);

        swaggerDoc.Paths.Add("/Test/Path", new OpenApiPathItem());

        // Act
        filter.Apply(swaggerDoc, context);

        // Assert
        swaggerDoc.Paths.Keys.Should().NotContain("/Test/Path");
    }
}