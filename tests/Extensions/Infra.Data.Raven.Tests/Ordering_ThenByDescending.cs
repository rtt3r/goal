using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Goal.Infra.Crosscutting.Extensions;
using Goal.Infra.Data.Extensions.RavenDB.Tests.Mocks;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Raven.TestDriver;
using Xunit;

namespace Goal.Infra.Data.Extensions.RavenDB.Tests;

public class Ordering_ThenByDescending : RavenTestDriver
{
    protected override void PreInitialize(IDocumentStore documentStore)
        => documentStore.Conventions.MaxNumberOfRequestsPerSession = 50;

    [Fact]
    public void ReturnThenByDescendingGivenSimpleProperty()
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
            var result = session
                .Query<TestObject1>()
                .OrderByDescending("Id").ThenByDescending("TestObject2Id")
                .ToList();

            // Assert
            result.Should().NotBeNull().And.BeOfType<List<TestObject1>>().And.NotBeEmpty().And.HaveSameCount(query);
            result.First().Id.Should().Be(query.Last().Id);
            result.Last().Id.Should().Be(query.First().Id);
        }
    }

    [Fact]
    public void ReturnThenByDescendingGivenComplexProperty()
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
            var result = session
                .Query<TestObject1>()
                .OrderByDescending("Id").ThenByDescending("TestObject2.Id")
                .ToList();

            // Assert
            result.Should().NotBeNull().And.BeOfType<List<TestObject1>>().And.NotBeEmpty().And.HaveSameCount(query);
            result.First().Id.Should().Be(query.Last().Id);
            result.Last().Id.Should().Be(query.First().Id);
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
