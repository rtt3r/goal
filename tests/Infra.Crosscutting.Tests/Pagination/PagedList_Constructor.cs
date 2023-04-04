using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using Goal.Seedwork.Infra.Crosscutting.Tests.Mocks;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Pagination;

public class PagedList_Constructor
{
    [Fact]
    public void ReturnNewPagedListGivenValidParameters()
    {
        IEnumerable<TestObject1> items = Enumerable.Repeat(new TestObject1(), 20);

        var pagging = new PagedList<TestObject1>(items, items.Count());

        pagging.TotalCount.Should().Be(20);
        pagging.Count().Should().Be(20);
    }

    [Fact]
    public void ReturnNewPagedListGivenNullList()
    {
        var pagging = new PagedList<TestObject1>(null, 20);

        pagging.TotalCount.Should().Be(20);
        pagging.Count().Should().Be(0);
    }

    [Fact]
    public void ReturnExplicitEnumeratorGivenAnyValue()
    {
        IEnumerable<TestObject1> items = Enumerable.Repeat(new TestObject1(), 20);
        var pagging = new PagedList<TestObject1>(items, 20);

        IEnumerator enumerableEnumerator = (pagging as IEnumerable).GetEnumerator();

        enumerableEnumerator.Should().NotBeNull();
    }
}
