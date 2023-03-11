using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using Goal.Seedwork.Infra.Crosscutting.Extensions;
using Goal.Seedwork.Infra.Crosscutting.Tests.Mocks;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Paginng
{
    public class Pagination_PaginateList
    {
        [Fact]
        public void PaginateListWithReminderSuccessfully()
        {
            var values = GetQuery(55).ToList();
            var pagination = new PageSearch(0, 10);

            var paginateResult = values.PaginateList(pagination) as PagedList<TestObject1>;

            paginateResult.Should().NotBeNull().And.HaveCount(10).And.HaveElementAt(0, values.ElementAt(0));
        }


        [Fact]
        public void PaginateListAsyncWithReminderSuccessfully()
        {
            var values = GetQuery(55).ToList();
            var pagination = new PageSearch(0, 10);

            var paginateResult = values.PaginateListAsync(pagination).GetAwaiter().GetResult() as PagedList<TestObject1>;

            paginateResult.Should().NotBeNull().And.HaveCount(10).And.HaveElementAt(0, values.ElementAt(0));
            paginateResult.TotalCount.Should().Be(values.Count);
        }

        [Fact]
        public void ReturnListOrderedAscendingGivenIndexAndSize()
        {
            var values = GetQuery().ToList();
            var pagination = new PageSearch(0, 10, "Id", SortDirection.Asc);

            var paginateResult = values.PaginateList(pagination) as PagedList<TestObject1>;

            paginateResult.Should().NotBeNull().And.HaveCount(10).And.HaveElementAt(0, values.ElementAt(0));
            paginateResult.TotalCount.Should().Be(values.Count);
        }

        [Fact]
        public void ReturnListOrderedAscendingAsyncGivenIndexAndSize()
        {
            var values = GetQuery().ToList();
            var pagination = new PageSearch(0, 10, "Id", SortDirection.Asc);

            var paginateResult = values.PaginateListAsync(pagination).GetAwaiter().GetResult() as PagedList<TestObject1>;

            paginateResult.Should().NotBeNull().And.HaveCount(10).And.HaveElementAt(0, values.ElementAt(0));
            paginateResult.TotalCount.Should().Be(values.Count);
        }

        [Fact]
        public void ReturnListOrderedDescendingGivenIndexAndSize()
        {
            var values = GetQuery().ToList();
            var pagination = new PageSearch(0, 10, "Id", SortDirection.Desc);

            var paginateResult = values.PaginateList(pagination) as PagedList<TestObject1>;

            paginateResult.Should().NotBeNull().And.HaveCount(10).And.HaveElementAt(0, values.ElementAt(99));
            paginateResult.TotalCount.Should().Be(values.Count);
        }

        [Fact]
        public void ReturnListOrderedDescendingAsyncGivenIndexAndSize()
        {
            var values = GetQuery().ToList();
            var pagination = new PageSearch(0, 10, "Id", SortDirection.Desc);

            var paginateResult = values.PaginateListAsync(pagination).GetAwaiter().GetResult() as PagedList<TestObject1>;

            paginateResult.Should().NotBeNull().And.HaveCount(10).And.HaveElementAt(0, values.ElementAt(99));
            paginateResult.TotalCount.Should().Be(100);
        }

        [Fact]
        public void ReturnPageListGivenZeroSize()
        {
            IEnumerable<TestObject1> values = GetQuery();
            var pagination = new PageSearch(0, 0);

            var paginateResult = values.PaginateList(pagination) as PagedList<TestObject1>;

            paginateResult.Should().NotBeNull();
            paginateResult.TotalCount.Should().Be(100);
        }

        [Fact]
        public void ReturnListGivenZeroSizeAsync()
        {
            IEnumerable<TestObject1> values = GetQuery();
            var pagination = new PageSearch(0, 0);

            var paginateResult = values.PaginateListAsync(pagination).GetAwaiter().GetResult() as PagedList<TestObject1>;

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
    }
}
