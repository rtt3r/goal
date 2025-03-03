using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Crosscutting.Extensions;
using Goal.Infra.Crosscutting.Tests.Mocks;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Pagination;

public class Pagination_Paginate
{
    [Fact]
    public void ReturnPaginatedGivenEmptyConstructor()
    {
        IQueryable<TestObject1> values = GetQuery();
        var pagination = new PageSearch();

        var paginateResult = values.Paginate(pagination).ToList();

        paginateResult.Should().NotBeNull().And.HaveCount(100).And.HaveElementAt(0, values.ElementAt(0));
    }

    [Fact]
    public void ReturnPaginatedGivenIndexAndSize()
    {
        IQueryable<TestObject1> values = GetQuery();
        var pagination = new PageSearch(0, 10);

        var paginateResult = values.Paginate(pagination).ToList();

        paginateResult.Should().NotBeNull().And.HaveCount(10).And.HaveElementAt(0, values.ElementAt(0));
    }

    [Fact]
    public void ReturnPaginatedGivenPageSizeZero()
    {
        IQueryable<TestObject1> values = GetQuery();
        var pagination = new PageSearch(0, 0);

        var paginateResult = values.Paginate(pagination).ToList();

        paginateResult.Should().NotBeNull().And.HaveCount(100);
    }

    [Fact]
    public void Paginate_WithPageSearch_ShouldReturnCorrectPage()
    {
        List<int> data = [.. Enumerable.Range(1, 100)];

        var pageSearch = new PageSearch(1, 10);
        var result = data.AsQueryable().Paginate(pageSearch).ToList();

        result.Should().HaveCount(10);
        result.First().Should().Be(11);
        result.Last().Should().Be(20);
    }

    [Fact]
    public void Paginate_WithPageIndexAndPageSize_ShouldReturnCorrectPage()
    {
        List<int> data = [.. Enumerable.Range(1, 100)];

        var result = data.AsQueryable().Paginate(1, 10).ToList();

        result.Should().HaveCount(10);
        result.First().Should().Be(11);
        result.Last().Should().Be(20);
    }

    [Fact]
    public void ThrowExceptionGivenNull()
    {
        Action act = () =>
        {
            IQueryable<TestObject1> values = GetQuery();
            values.Paginate(null!);
        };

        act.Should().Throw<ArgumentNullException>().WithParameterName("pageSearch");
    }

    private static IQueryable<TestObject1> GetQuery()
        => GetQuery(100);

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