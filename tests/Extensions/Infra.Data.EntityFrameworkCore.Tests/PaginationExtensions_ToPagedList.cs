using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using Goal.Infra.Data.EntityFrameworkCore;
using Goal.Infra.Data.Extensions.EFCore.Tests.Mock;
using Moq;
using Xunit;

namespace Goal.Infra.Data.Extensions.EFCore.Tests;

public class PaginationExtensions_ToPagedList
{
    [Fact]
    public async Task PaginateSuccessfullyGivenEnumerable()
    {
        IQueryable<TestObject1> query = GetQuery(55);
        Mock<IQueryable<TestObject1>> mock = query.BuildMock();

        var pagination = new PageSearch(0, 10);
        IPagedList<TestObject1> paginateResult = await mock.Object.ToPagedListAsync(pagination);

        paginateResult.Should().NotBeNull().And.BeOfType<PagedList<TestObject1>>();
        paginateResult.As<PagedList<TestObject1>>().Should().HaveCount(10).And.HaveElementAt(0, query.ElementAt(0));
        paginateResult.As<PagedList<TestObject1>>().TotalCount.Should().Be(query.Count());
    }

    [Fact]
    public async Task PaginateSuccessfullyGivenQueryable()
    {
        IQueryable<TestObject1> query = GetQuery(55);
        Mock<IQueryable<TestObject1>> mock = query.BuildMock();

        var pagination = new PageSearch(0, 10);
        IPagedList<TestObject1> paginateResult = await mock.Object.ToPagedListAsync(pagination);

        paginateResult.Should().NotBeNull().And.BeOfType<PagedList<TestObject1>>();
        paginateResult.As<PagedList<TestObject1>>().Should().HaveCount(10).And.HaveElementAt(0, query.ElementAt(0));
        paginateResult.As<PagedList<TestObject1>>().TotalCount.Should().Be(query.Count());
    }

    [Fact]
    public async Task ReturnListOrderedAscendingAsyncGivenIndexAndSize()
    {
        IQueryable<TestObject1> query = GetQuery();
        Mock<IQueryable<TestObject1>> mock = query.BuildMock();

        var pagination = new PageSearch(0, 10, "Id", SortDirection.Asc);
        IPagedList<TestObject1> paginateResult = await mock.Object.ToPagedListAsync(pagination);

        paginateResult.Should().NotBeNull().And.BeOfType<PagedList<TestObject1>>();
        paginateResult.As<PagedList<TestObject1>>().Should().HaveCount(10).And.HaveElementAt(0, query.ElementAt(0));
        paginateResult.As<PagedList<TestObject1>>().TotalCount.Should().Be(query.Count());
    }

    [Fact]
    public async Task ReturnListOrderedDescendingAsyncGivenIndexAndSize()
    {
        IQueryable<TestObject1> query = GetQuery();
        Mock<IQueryable<TestObject1>> mock = query.BuildMock();

        var pagination = new PageSearch(0, 10, "Id", SortDirection.Desc);
        IPagedList<TestObject1> paginateResult = await mock.Object.ToPagedListAsync(pagination);

        paginateResult.Should().NotBeNull().And.BeOfType<PagedList<TestObject1>>();
        paginateResult.As<PagedList<TestObject1>>().Should().HaveCount(10).And.HaveElementAt(0, query.ElementAt(99));
        paginateResult.As<PagedList<TestObject1>>().TotalCount.Should().Be(100);
    }

    [Fact]
    public async Task ReturnListGivenZeroSizeAsync()
    {
        IQueryable<TestObject1> query = GetQuery();
        Mock<IQueryable<TestObject1>> mock = query.BuildMock();

        var pagination = new PageSearch(0, 0);
        IPagedList<TestObject1> paginateResult = await mock.Object.ToPagedListAsync(pagination);

        paginateResult.Should().NotBeNull().And.BeOfType<PagedList<TestObject1>>();
        paginateResult.As<PagedList<TestObject1>>().TotalCount.Should().Be(100);
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