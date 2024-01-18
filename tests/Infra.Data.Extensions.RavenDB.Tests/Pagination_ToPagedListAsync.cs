using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using Goal.Infra.Data.Extensions.RavenDB.Tests.Mocks;
using Goal.Infra.Data.Raven;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Raven.TestDriver;
using Xunit;

namespace Goal.Infra.Data.Extensions.RavenDB.Tests;

public class Pagination_ToPagedListAsync : RavenTestDriver
{
    protected override void PreInitialize(IDocumentStore documentStore)
        => documentStore.Conventions.MaxNumberOfRequestsPerSession = 50;

    [Fact]
    public async Task ToPagedListWithReminderSuccessfully()
    {
        // Arrange
        using IDocumentStore store = GetDocumentStore();

        IQueryable<TestObject1> query = GetQuery(55);

        using (IAsyncDocumentSession session = store.OpenAsyncSession())
        {
            foreach (TestObject1 item in query)
            {
                await session.StoreAsync(item);
            }

            await session.SaveChangesAsync();
        }

        // Sometimes we want to debug the test itself. This method redirects us to the studio
        // so that we can see if the code worked as expected (in this case, created two documents).
        // WaitForUserToContinueTheTest(store);

        using (IAsyncDocumentSession session = store.OpenAsyncSession())
        {
            // Act
            var pagination = new PageSearch(0, 10);

            IPagedList<TestObject1> result = await session
                .Query<TestObject1>()
                .ToPagedListAsync(pagination);

            // Assert
            result.Should().NotBeNull().And.HaveCount(10);
            result.ElementAt(0).Id.Should().Be("0001");
            result.TotalCount.Should().Be(55);
        }
    }

    [Fact]
    public async Task ReturnListOrderedAscendingGivenIndexAndSize()
    {
        // Arrange
        using IDocumentStore store = GetDocumentStore();

        IQueryable<TestObject1> query = GetQuery(100);

        using (IAsyncDocumentSession session = store.OpenAsyncSession())
        {
            foreach (TestObject1 item in query)
            {
                await session.StoreAsync(item);
            }

            await session.SaveChangesAsync();
        }

        // Sometimes we want to debug the test itself. This method redirects us to the studio
        // so that we can see if the code worked as expected (in this case, created two documents).
        // WaitForUserToContinueTheTest(store);

        using (IAsyncDocumentSession session = store.OpenAsyncSession())
        {
            // Act
            var pagination = new PageSearch(0, 10, "Id", SortDirection.Asc);

            IPagedList<TestObject1> result = await session
                .Query<TestObject1>()
                .ToPagedListAsync(pagination);

            // Assert
            result.Should().NotBeNull().And.HaveCount(10);
            result.ElementAt(0).Id.Should().Be("0001");
            result.TotalCount.Should().Be(100);
        }
    }

    [Fact]
    public async Task ReturnListOrderedDescendingGivenIndexAndSize()
    {
        // Arrange
        using IDocumentStore store = GetDocumentStore();

        IQueryable<TestObject1> query = GetQuery(100);

        using (IAsyncDocumentSession session = store.OpenAsyncSession())
        {
            foreach (TestObject1 item in query)
            {
                await session.StoreAsync(item);
            }

            await session.SaveChangesAsync();
        }

        // Sometimes we want to debug the test itself. This method redirects us to the studio
        // so that we can see if the code worked as expected (in this case, created two documents).
        // WaitForUserToContinueTheTest(store);

        using (IAsyncDocumentSession session = store.OpenAsyncSession())
        {
            // Act
            var pagination = new PageSearch(0, 10, "Id", SortDirection.Desc);

            IPagedList<TestObject1> result = await session
                .Query<TestObject1>()
                .ToPagedListAsync(pagination);

            // Assert
            result.Should().NotBeNull().And.HaveCount(10);
            result.ElementAt(0).Id.Should().Be("0100");
            result.TotalCount.Should().Be(100);
        }
    }

    [Fact]
    public async Task ReturnPageListGivenZeroSize()
    {
        // Arrange
        using IDocumentStore store = GetDocumentStore();

        IQueryable<TestObject1> query = GetQuery(100);

        using (IAsyncDocumentSession session = store.OpenAsyncSession())
        {
            foreach (TestObject1 item in query)
            {
                await session.StoreAsync(item);
            }

            await session.SaveChangesAsync();
        }

        // Sometimes we want to debug the test itself. This method redirects us to the studio
        // so that we can see if the code worked as expected (in this case, created two documents).
        // WaitForUserToContinueTheTest(store);

        using (IAsyncDocumentSession session = store.OpenAsyncSession())
        {
            // Act
            var pagination = new PageSearch(0, 0);

            IPagedList<TestObject1> result = await session
                .Query<TestObject1>()
                .ToPagedListAsync(pagination);

            // Assert
            result.Should().NotBeNull();
            result.TotalCount.Should().Be(100);
        }
    }

    private static IQueryable<TestObject1> GetQuery(int length)
    {
        var query = new List<TestObject1>();

        for (int i = 1; i <= length; i++)
        {
            query.Add(new TestObject1 { Id = $"{i}".PadLeft(4, '0'), TestObject2Id = $"{i}".PadLeft(4, '0'), TestObject2 = new TestObject2 { Id = $"{i}".PadLeft(4, '0') } });
        }

        return query.AsQueryable();
    }
}
