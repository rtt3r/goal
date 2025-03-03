using FluentAssertions;
using Goal.Infra.Http.Controllers.Requests;
using Xunit;

namespace Goal.Infra.Http.Tests.Controllers;

public class PageSearchRequest_Instance
{
    [Fact]
    public void DefaultValues_AreSet_Correctly()
    {
        // Arrange & Act
        var request = new PageSearchRequest();

        // Assert
        request.PageIndex.Should().Be(0);
        request.PageSize.Should().Be(int.MaxValue);
    }

    [Fact]
    public void Properties_CanBe_Set_And_Retrieved_Correctly()
    {
        // Arrange
        var request = new PageSearchRequest
        {
            // Act
            PageIndex = 1,
            PageSize = 10
        };

        // Assert
        request.PageIndex.Should().Be(1);
        request.PageSize.Should().Be(10);
    }
}
