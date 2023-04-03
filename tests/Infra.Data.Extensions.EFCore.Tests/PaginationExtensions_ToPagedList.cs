using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using Goal.Seedwork.Infra.Data.Extensions.EFCore;
using Goal.Seedwork.Infra.Data.Tests.Mock;
using Moq;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Goal.Seedwork.Infra.Data.Tests;

public class PaginationExtensions_ToPagedList
{
    [Fact]
    public void PaginateSuccessfullyGivenEnumerable()
    {
        IQueryable<TestObject1> query = GetQuery(55);
        var mock = query.BuildMock();

        var pagination = new PageSearch(0, 10);
        var paginateResult = mock.Object.ToPagedListAsync(pagination).GetAwaiter().GetResult() as PagedList<TestObject1>;

        paginateResult.Should().NotBeNull().And.HaveCount(10).And.HaveElementAt(0, query.ElementAt(0));
        paginateResult.TotalCount.Should().Be(query.Count());
    }

    [Fact]
    public void PaginateSuccessfullyGivenQueryable()
    {
        IQueryable<TestObject1> query = GetQuery(55);
        var mock = query.BuildMock();

        var pagination = new PageSearch(0, 10);
        var paginateResult = mock.Object.ToPagedListAsync(pagination).GetAwaiter().GetResult() as PagedList<TestObject1>;

        paginateResult.Should().NotBeNull().And.HaveCount(10).And.HaveElementAt(0, query.ElementAt(0));
        paginateResult.TotalCount.Should().Be(query.Count());
    }

    [Fact]
    public void ReturnListOrderedAscendingAsyncGivenIndexAndSize()
    {
        IQueryable<TestObject1> query = GetQuery();
        var mock = query.BuildMock();

        var pagination = new PageSearch(0, 10, "Id", SortDirection.Asc);
        var paginateResult = mock.Object.ToPagedListAsync(pagination).GetAwaiter().GetResult() as PagedList<TestObject1>;

        paginateResult.Should().NotBeNull().And.HaveCount(10).And.HaveElementAt(0, query.ElementAt(0));
        paginateResult.TotalCount.Should().Be(query.Count());
    }

    [Fact]
    public void ReturnListOrderedDescendingAsyncGivenIndexAndSize()
    {
        IQueryable<TestObject1> query = GetQuery();
        var mock = query.BuildMock();

        var pagination = new PageSearch(0, 10, "Id", SortDirection.Desc);
        var paginateResult = mock.Object.ToPagedListAsync(pagination).GetAwaiter().GetResult() as PagedList<TestObject1>;

        paginateResult.Should().NotBeNull().And.HaveCount(10).And.HaveElementAt(0, query.ElementAt(99));
        paginateResult.TotalCount.Should().Be(100);
    }

    [Fact]
    public void ReturnListGivenZeroSizeAsync()
    {
        IQueryable<TestObject1> query = GetQuery();
        var mock = query.BuildMock();

        var pagination = new PageSearch(0, 0);
        var paginateResult = mock.Object.ToPagedListAsync(pagination).GetAwaiter().GetResult() as PagedList<TestObject1>;

        paginateResult.Should().NotBeNull();
        paginateResult.TotalCount.Should().Be(100);
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

    [Display]
    public class TestObject1
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int TestObject2Id { get; set; }
        public TestObject2 TestObject2 { get; set; }

        public TestObject1()
            : base()
        {
        }

        public TestObject1(int id)
            : base()
        {
            Id = id;
        }
    }

    public class TestObject2
    {
        public int Id { get; set; }
    }
}