using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using Goal.Seedwork.Infra.Http.Controllers;
using Goal.Seedwork.Infra.Http.Controllers.Results;
using Xunit;

namespace Goal.Seedwork.Infra.Http.Tests.Controllers
{
    public class ApiController_Paged
    {
        [Fact]
        public void WithCollection_ReturnsOkPagedCollectionResult()
        {
            // Arrange
            var controller = new TestController();
            var all = new List<Test>()
            {
                new Test { Name = "Test1", Age = 20 },
                new Test { Name = "Test2", Age = 20 },
                new Test { Name = "Test3", Age = 20 },
                new Test { Name = "Test4", Age = 20 },
            };
            IEnumerable<Test> page = all.Take(2).ToList();
            var pagedList = new PagedList<Test>(page, all.Count);

            // Act
            OkPagedCollectionResult actionResult = controller.PagedWrapper(pagedList);

            // Assert
            actionResult.Should().BeOfType<OkPagedCollectionResult>();
            actionResult.As<OkPagedCollectionResult>().Value.Should().BeOfType<PagedResponse>();

            PagedResponse pagedResponse = actionResult.As<OkPagedCollectionResult>().Value.As<PagedResponse>();

            pagedResponse.TotalCount.Should().Be(all.Count);
            pagedResponse.Items.As<IEnumerable<Test>>().Should().HaveCount(2);
            pagedResponse.Items.As<IEnumerable<Test>>().ElementAt(0).Name.Should().Be("Test1");
            pagedResponse.Items.As<IEnumerable<Test>>().ElementAt(1).Name.Should().Be("Test2");
        }

        public class TestController : ApiController
        {
            public OkPagedCollectionResult PagedWrapper(IPagedCollection collection)
                => Paged(collection);
        }

        public class Test
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
