using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.Client.Documents.Session;
using Raven.TestDriver;
using Xunit;

namespace Goal.Seedwork.Infra.Data.Extensions.RavenDB.Tests;

public class Ordering_OrderByDescending : RavenTestDriver
{
    public Ordering_OrderByDescending()
    {
        // ConfigureServer() must be set before calling GetDocumentStore()
        // and can only be set once per test run.
        ConfigureServer(new TestServerOptions
        {
            DataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RavenDBTestDir")
        });
    }
    // This allows us to modify the conventions of the store we get from 'GetDocumentStore'
    protected override void PreInitialize(IDocumentStore documentStore)
        => documentStore.Conventions.MaxNumberOfRequestsPerSession = 50;

    [Fact]
    public void ReturnOrderByDescendingGivenSimpleProperty()
    {
        using IDocumentStore store = GetDocumentStore();
        store.ExecuteIndex(new TestDocumentByName());

        using (IDocumentSession session = store.OpenSession())
        {
            session.Store(new TestDocument { Name = "Goodbye..." });
            session.Store(new TestDocument { Name = "Hello world!" });
            session.SaveChanges();
        }

        WaitForIndexing(store);

        // Sometimes we want to debug the test itself. This method redirects us to the studio
        // so that we can see if the code worked as expected (in this case, created two documents).
        // WaitForUserToContinueTheTest(store);

        using (IDocumentSession session = store.OpenSession())
        {
            var result = session
                .Query<TestDocument, TestDocumentByName>()
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
        store.ExecuteIndex(new TestDocumentByName());
        store.ExecuteIndex(new TestDocumentByTestName());

        using (IDocumentSession session = store.OpenSession())
        {
            session.Store(new TestDocument { Name = "Goodbye...", Test = new TestDocument { Name = "Bye..." } });
            session.Store(new TestDocument { Name = "Hello world!", Test = new TestDocument { Name = "Hi!" } });
            session.SaveChanges();
        }

        WaitForIndexing(store);

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

public class TestDocumentByName : AbstractIndexCreationTask<TestDocument>
{
    public TestDocumentByName()
    {
        Map = docs => from doc in docs select new { doc.Name };
        Indexes.Add(x => x.Name, FieldIndexing.Search);
    }
}

public class TestDocumentByTestName : AbstractIndexCreationTask<TestDocument>
{
    public TestDocumentByTestName()
    {
        Map = docs => from doc in docs select new { doc.Test.Name };
        Indexes.Add(x => x.Test.Name, FieldIndexing.Search);
    }
}

public class TestDocument
{
    public string Name { get; set; }
    public TestDocument Test { get; set; }
}
