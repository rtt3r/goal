using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Crosscutting.Extensions;
using Goal.Infra.Crosscutting.Tests.Mocks;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Pagination;

public class Pagination_ToPagedList
{
    [Fact]
    public void ToPagedListWithReminderSuccessfully()
    {
        IQueryable<TestObject1> values = GetQuery(55);
        var pagination = new PageSearch(0, 10);

        var paginateResult = values.ToPagedList(pagination) as PagedList<TestObject1>;

        paginateResult.Should().NotBeNull().And.HaveCount(10).And.HaveElementAt(0, values.ElementAt(0));
    }

    [Fact]
    public void ReturnListOrderedAscendingGivenIndexAndSize()
    {
        IQueryable<TestObject1> values = GetQuery();
        var pagination = new PageSearch(0, 10, "Id", SortDirection.Asc);

        var paginateResult = values.ToPagedList(pagination) as PagedList<TestObject1>;

        paginateResult.Should().NotBeNull().And.HaveCount(10).And.HaveElementAt(0, values.ElementAt(0));
        paginateResult?.TotalCount.Should().Be(values.Count());
    }

    [Fact]
    public void ReturnListOrderedDescendingGivenIndexAndSize()
    {
        IQueryable<TestObject1> values = GetQuery();
        var pagination = new PageSearch(0, 10, "Id", SortDirection.Desc);

        var paginateResult = values.ToPagedList(pagination) as PagedList<TestObject1>;

        paginateResult.Should().NotBeNull().And.HaveCount(10).And.HaveElementAt(0, values.ElementAt(99));
        paginateResult?.TotalCount.Should().Be(values.Count());
    }

    [Fact]
    public void ReturnPageListGivenZeroSize()
    {
        IQueryable<TestObject1> values = GetQuery();
        var pagination = new PageSearch(0, 0);

        var paginateResult = values.ToPagedList(pagination) as PagedList<TestObject1>;

        paginateResult.Should().NotBeNull();
        paginateResult?.TotalCount.Should().Be(100);
    }

    private static IQueryable<TestObject1> GetQuery() => GetQuery(100);

    private static IQueryable<TestObject1> GetQuery(int length)
    {
        var query = new List<TestObject1>();

        for (int i = 1; i <= length; i++)
        {
            query.Add(new TestObject1 { Id = i, TestObject2Id = i, TestObject2 = new TestObject2 { Id = i } });
        }

        return query.AsQueryable();
    }
}
