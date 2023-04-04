using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Raven.TestDriver;
using Xunit;

namespace Goal.Seedwork.Infra.Data.Extensions.RavenDB.Tests;

public class Ordering_OrderByDescending : RavenTestDriver
{
    protected override void PreInitialize(IDocumentStore documentStore)
        => documentStore.Conventions.MaxNumberOfRequestsPerSession = 50;

    [Fact]
    public void ReturnOrderByDescendingGivenSimpleProperty()
    {
        using IDocumentStore store = GetDocumentStore();

        using (IDocumentSession session = store.OpenSession())
        {
            session.Store(new TestDocument { Name = "Goodbye..." });
            session.Store(new TestDocument { Name = "Hello world!" });
            session.SaveChanges();
        }

        // Sometimes we want to debug the test itself. This method redirects us to the studio
        // so that we can see if the code worked as expected (in this case, created two documents).
        // WaitForUserToContinueTheTest(store);

        using (IDocumentSession session = store.OpenSession())
        {
            var result = session
                .Query<TestDocument>()
                .OrderByDescending("Name")
                .ToList();

            result.Should().NotBeNull().And.BeOfType<List<TestDocument>>().And.NotBeEmpty().And.HaveCount(2);
            result.First().Name.Should().Be("Hello world!");
            result.Last().Name.Should().Be("Goodbye...");
        }
    }

    [Fact]
    public void ReturnOrderByDescendingGivenComplexProperty()
    {
        using IDocumentStore store = GetDocumentStore();

        using (IDocumentSession session = store.OpenSession())
        {
            session.Store(new TestDocument { Name = "Goodbye...", Test = new TestDocument { Name = "Bye..." } });
            session.Store(new TestDocument { Name = "Hello world!", Test = new TestDocument { Name = "Hi!" } });
            session.SaveChanges();
        }

        // Sometimes we want to debug the test itself. This method redirects us to the studio
        // so that we can see if the code worked as expected (in this case, created two documents).
        // WaitForUserToContinueTheTest(store);

        using (IDocumentSession session = store.OpenSession())
        {
            var result = session
                .Query<TestDocument>()
                .OrderByDescending("Test.Name")
                .ToList();

            result.Should().NotBeNull().And.BeOfType<List<TestDocument>>().And.NotBeEmpty().And.HaveCount(2);
            result.First().Name.Should().Be("Hello world!");
            result.Last().Name.Should().Be("Goodbye...");
        }
    }
}

public class TestDocument
{
    public string Name { get; set; }
    public TestDocument Test { get; set; }
}
