using System.Linq;
using FluentAssertions;
using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Crosscutting.Extensions;
using Goal.Infra.Crosscutting.Tests.Mocks;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Extensions;

public class Enumerable_SelectPaged
{
    [Fact]
    public void MapPagedCollectionSuccessfully()
    {
        IPagedList<TestObject1> page = new PagedList<TestObject1>(new[] { new TestObject1 { Id = 1 } }, 1);
        IPagedList<int> map = page.Select(p => p.Id);

        map.Should().NotBeNull().And.HaveCount(1).And.Match(p => p.FirstOrDefault() == 1);
    }
}
