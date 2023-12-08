using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using Goal.Seedwork.Infra.Crosscutting.Extensions;
using Goal.Seedwork.Infra.Data.Extensions.RavenDB.Tests.Mocks;
using Raven.Client.Documents.Session;
using Raven.Client.Documents;
using Xunit;
using Raven.TestDriver;

namespace Goal.Seedwork.Infra.Data.Extensions.RavenDB.Tests;

public class Pagination_ToPagedList : RavenTestDriver
{
    protected override void PreInitialize(IDocumentStore documentStore)
        => documentStore.Conventions.MaxNumberOfRequestsPerSession = 50;

    [Fact]
    public void ToPagedListWithReminderSuccessfully()
    {
        // Arrange
        using IDocumentStore store = GetDocumentStore();

        IQueryable<TestObject1> query = GetQuery(55);

        using (IDocumentSession session = store.OpenSession())
        {
            query.ForEach(obj => session.Store(obj));
            session.SaveChanges();
        }

        // Sometimes we want to debug the test itself. This method redirects us to the studio
        // so that we can see if the code worked as expected (in this case, created two documents).
        // WaitForUserToContinueTheTest(store);

        using (IDocumentSession session = store.OpenSession())
        {
            // Act
            var pagination = new PageSearch(0, 10);

            var result = session
                .Query<TestObject1>()
                .ToPagedList(pagination);

            // Assert
            result.Should().NotBeNull().And.HaveCount(10);
            result.ElementAt(0).Id.Should().Be("0001");
            result.TotalCount.Should().Be(55);
        }
    }

    [Fact]
    public void ReturnListOrderedAscendingGivenIndexAndSize()
    {
        // Arrange
        using IDocumentStore store = GetDocumentStore();

        IQueryable<TestObject1> query = GetQuery(100);

        using (IDocumentSession session = store.OpenSession())
        {
            query.ForEach(obj => session.Store(obj));
            session.SaveChanges();
        }

        // Sometimes we want to debug the test itself. This method redirects us to the studio
        // so that we can see if the code worked as expected (in this case, created two documents).
        // WaitForUserToContinueTheTest(store);

        using (IDocumentSession session = store.OpenSession())
        {
            // Act
            var pagination = new PageSearch(0, 10, "Id", SortDirection.Asc);

            var result = session
                .Query<TestObject1>()
                .ToPagedList(pagination);

            // Assert
            result.Should().NotBeNull().And.HaveCount(10);
            result.ElementAt(0).Id.Should().Be("0001");
            result.TotalCount.Should().Be(100);
        }
    }

    [Fact]
    public void ReturnListOrderedDescendingGivenIndexAndSize()
    {
        // Arrange
        using IDocumentStore store = GetDocumentStore();

        IQueryable<TestObject1> query = GetQuery(100);

        using (IDocumentSession session = store.OpenSession())
        {
            query.ForEach(obj => session.Store(obj));
            session.SaveChanges();
        }

        // Sometimes we want to debug the test itself. This method redirects us to the studio
        // so that we can see if the code worked as expected (in this case, created two documents).
        // WaitForUserToContinueTheTest(store);

        using (IDocumentSession session = store.OpenSession())
        {
            // Act
            var pagination = new PageSearch(0, 10, "Id", SortDirection.Desc);

            var result = session
                .Query<TestObject1>()
                .ToPagedList(pagination);

            // Assert
            result.Should().NotBeNull().And.HaveCount(10);
            result.ElementAt(0).Id.Should().Be("0100");
            result.TotalCount.Should().Be(100);
        }
    }

    [Fact]
    public void ReturnPageListGivenZeroSize()
    {
        // Arrange
        using IDocumentStore store = GetDocumentStore();

        IQueryable<TestObject1> query = GetQuery(100);

        using (IDocumentSession session = store.OpenSession())
        {
            query.ForEach(obj => session.Store(obj));
            session.SaveChanges();
        }

        // Sometimes we want to debug the test itself. This method redirects us to the studio
        // so that we can see if the code worked as expected (in this case, created two documents).
        // WaitForUserToContinueTheTest(store);

        using (IDocumentSession session = store.OpenSession())
        {
            // Act
            var pagination = new PageSearch(0, 0);

            var result = session
                .Query<TestObject1>()
                .ToPagedList(pagination);

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
