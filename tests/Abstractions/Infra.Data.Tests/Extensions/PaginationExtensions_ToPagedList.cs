using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Data.Extensions;
using Goal.Infra.Data.Tests.Mocks;
using Moq;
using Xunit;

namespace Goal.Infra.Data.Tests.Extensions;

public class PaginationExtensions_ToPagedList
{
    [Fact]
    public async Task PaginateSuccessfullyGivenEnumerable()
    {
        IQueryable<Test> query = GetQuery(55);
        Mock<IQueryable<Test>> mock = query.BuildMock();

        var pagination = new PageSearch(0, 10);
        IPagedList<Test> paginateResult = await mock.Object.ToPagedListAsync(pagination);

        paginateResult.Should().NotBeNull().And.BeOfType<PagedList<Test>>();
        paginateResult.As<PagedList<Test>>().Should().HaveCount(10).And.HaveElementAt(0, query.ElementAt(0));
        paginateResult.As<PagedList<Test>>().TotalCount.Should().Be(query.Count());
    }

    [Fact]
    public async Task PaginateSuccessfullyGivenQueryable()
    {
        IQueryable<Test> query = GetQuery(55);
        Mock<IQueryable<Test>> mock = query.BuildMock();

        var pagination = new PageSearch(0, 10);
        IPagedList<Test> paginateResult = await mock.Object.ToPagedListAsync(pagination);

        paginateResult.Should().NotBeNull().And.BeOfType<PagedList<Test>>();
        paginateResult.As<PagedList<Test>>().Should().HaveCount(10).And.HaveElementAt(0, query.ElementAt(0));
        paginateResult.As<PagedList<Test>>().TotalCount.Should().Be(query.Count());
    }

    [Fact]
    public async Task ReturnListGivenZeroSizeAsync()
    {
        IQueryable<Test> query = GetQuery();
        Mock<IQueryable<Test>> mock = query.BuildMock();

        var pagination = new PageSearch(0, 0);
        IPagedList<Test> paginateResult = await mock.Object.ToPagedListAsync(pagination);

        paginateResult.Should().NotBeNull().And.BeOfType<PagedList<Test>>();
        paginateResult.As<PagedList<Test>>().TotalCount.Should().Be(100);
    }

    private static IQueryable<Test> GetQuery() => GetQuery(100);

    private static IQueryable<Test> GetQuery(int length)
    {
        var query = new List<Test>();

        for (int i = 1; i <= length; i++)
        {
            query.Add(new Test(i));
        }

        return query.AsQueryable();
    }
}