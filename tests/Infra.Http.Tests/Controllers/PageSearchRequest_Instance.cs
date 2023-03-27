using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using Goal.Seedwork.Infra.Http.Controllers.Requests;
using Xunit;

namespace Goal.Seedwork.Infra.Http.Tests.Controllers
{
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
            request.SortBy.Should().BeNull();
            request.SortDirection.Should().BeNull();
        }

        [Fact]
        public void Properties_CanBe_Set_And_Retrieved_Correctly()
        {
            // Arrange
            var request = new PageSearchRequest
            {
                // Act
                PageIndex = 1,
                PageSize = 10,
                SortBy = "name",
                SortDirection = SortDirection.Desc
            };

            // Assert
            request.PageIndex.Should().Be(1);
            request.PageSize.Should().Be(10);
            request.SortBy.Should().Be("name");
            request.SortDirection.Should().Be(SortDirection.Desc);
        }
    }
}
