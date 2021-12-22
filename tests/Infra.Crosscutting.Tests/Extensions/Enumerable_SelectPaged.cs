using System.Linq;
using FluentAssertions;
using Vantage.Infra.Crosscutting.Collections;
using Vantage.Infra.Crosscutting.Extensions;
using Vantage.Infra.Crosscutting.Tests.Mocks;
using Xunit;

namespace Vantage.Infra.Crosscutting.Tests.Extensions
{
    public class Enumerable_SelectPaged
    {
        [Fact]
        public void MapPagedCollectionSuccessfully()
        {
            IPagedCollection<TestObject1> page = new PagedList<TestObject1>(new[] { new TestObject1 { Id = 1 } }, 1);
            IPagedCollection<int> map = page.SelectPaged(p => p.Id);

            map.Should().NotBeNull().And.HaveCount(1).And.Match(p => p.FirstOrDefault() == 1);
        }
    }
}
