using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goal.Seedwork.Infra.Http.Controllers.Requests;
using Xunit;
using Goal.Seedwork.Infra.Http.Extensions;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Collections;

namespace Goal.Seedwork.Infra.Http.Tests.Extensions
{
    public class PaginationExtensions_ToPageSearch
    {
        [Fact]
        public void ReturnsNewPageSearch_WhenRequestIsNull()
        {
            // Arrange
            PageSearchRequest request = null;

            // Act
            var result = request.ToPageSearch();

            // Assert
            result.Should().NotBeNull();
            result.PageIndex.Should().Be(0);
            result.PageSize.Should().Be(int.MaxValue);
            result.SortBy.Should().BeNull();
            result.SortDirection.Should().Be(SortDirection.Asc);
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
}
