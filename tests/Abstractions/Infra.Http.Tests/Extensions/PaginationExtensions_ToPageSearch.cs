using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using Goal.Infra.Http.Controllers.Requests;
using Goal.Infra.Http.Extensions;
using Xunit;

namespace Goal.Infra.Http.Tests.Extensions;

public class PaginationExtensions_ToPageSearch
{
    [Fact]
    public void ReturnsNewPageSearch_WhenRequestIsNull()
    {
        // Arrange
        PageSearchRequest? request = null;

        // Act
        var result = request?.ToPageSearch();

        // Assert
        result?.Should().NotBeNull();
        result?.PageIndex.Should().Be(0);
        result?.PageSize.Should().Be(int.MaxValue);
        result?.SortBy.Should().BeNull();
        result?.SortDirection.Should().Be(SortDirection.Asc);
    }

    [Fact]
    public void ReturnsNewPageSearch_WhenRequestIsNotNull()
    {
        // Arrange
        var request = new PageSearchRequest
        {
            PageIndex = 1,
            PageSize = 10,
            SortBy = "name",
            SortDirection = SortDirection.Desc
        };

        // Act
        var result = request.ToPageSearch();

        // Assert
        result.Should().NotBeNull();
        result.PageIndex.Should().Be(request.PageIndex);
        result.PageSize.Should().Be(request.PageSize);
        result.SortBy.Should().Be(request.SortBy);
        result.SortDirection.Should().Be(request.SortDirection.Value);
    }
}
