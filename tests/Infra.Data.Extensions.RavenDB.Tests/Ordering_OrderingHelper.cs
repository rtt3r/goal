using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Raven.TestDriver;
using Xunit;

namespace Goal.Seedwork.Infra.Data.Extensions.RavenDB.Tests;

public class Ordering_OrderingHelper : RavenTestDriver
{
    protected override void PreInitialize(IDocumentStore documentStore)
        => documentStore.Conventions.MaxNumberOfRequestsPerSession = 50;

    [Fact]
    public void Returns_IOrderedQueryable()
    {
        using IDocumentStore store = GetDocumentStore();

        using IDocumentSession session = store.OpenSession();
        var result = session
            .Query<Foo>()
            .OrderBy("MyProperty", SortDirection.Asc)
            .ToList();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<List<Foo>>();
    }

    [Fact]
    public void Sorts_By_First_Level_Ascending()
    {
        // Arrange
        using IDocumentStore store = GetDocumentStore();

        using (IDocumentSession session = store.OpenSession())
        {
            session.Store(new Foo { MyProperty = 3 });
            session.Store(new Foo { MyProperty = 1 });
            session.Store(new Foo { MyProperty = 2 });
            session.SaveChanges();
        }

        // Sometimes we want to debug the test itself. This method redirects us to the studio
        // so that we can see if the code worked as expected (in this case, created two documents).
        // WaitForUserToContinueTheTest(store);

        using (IDocumentSession session = store.OpenSession())
        {
            // Act
            var result = session
                .Query<Foo>()
                .OrderBy("MyProperty", SortDirection.Asc)
                .ToList();

            // Assert
            result[0].MyProperty.Should().Be(1);
            result[1].MyProperty.Should().Be(2);
            result[2].MyProperty.Should().Be(3);
        }
    }

    [Fact]
    public void Sorts_By_First_Level_Descending()
    {
        // Arrange
        using IDocumentStore store = GetDocumentStore();

        using (IDocumentSession session = store.OpenSession())
        {
            session.Store(new Foo { MyProperty = 3 });
            session.Store(new Foo { MyProperty = 1 });
            session.Store(new Foo { MyProperty = 2 });
            session.SaveChanges();
        }

        // Sometimes we want to debug the test itself. This method redirects us to the studio
        // so that we can see if the code worked as expected (in this case, created two documents).
        // WaitForUserToContinueTheTest(store);

        using (IDocumentSession session = store.OpenSession())
        {
            // Act
            var result = session
                .Query<Foo>()
                .OrderBy("MyProperty", SortDirection.Desc)
                .ToList();

            // Assert
            result[0].MyProperty.Should().Be(3);
            result[1].MyProperty.Should().Be(2);
            result[2].MyProperty.Should().Be(1);
        }
    }

    [Fact]
    public void Sorts_By_Second_Level_Ascending()
    {
        // Arrange
        using IDocumentStore store = GetDocumentStore();

        using (IDocumentSession session = store.OpenSession())
        {
            session.Store(new Foo { OtherFoo = new OtherFoo { MyProperty = 2 } });
            session.Store(new Foo { OtherFoo = new OtherFoo { MyProperty = 1 } });
            session.Store(new Foo { OtherFoo = new OtherFoo { MyProperty = 3 } });
            session.SaveChanges();
        }

        // Sometimes we want to debug the test itself. This method redirects us to the studio
        // so that we can see if the code worked as expected (in this case, created two documents).
        // WaitForUserToContinueTheTest(store);

        using (IDocumentSession session = store.OpenSession())
        {
            // Act
            var result = session
                .Query<Foo>()
                .OrderBy("OtherFoo.MyProperty", SortDirection.Asc)
                .ToList();

            // Assert
            result[0].OtherFoo.MyProperty.Should().Be(1);
            result[1].OtherFoo.MyProperty.Should().Be(2);
            result[2].OtherFoo.MyProperty.Should().Be(3);
        }
    }

    [Fact]
    public void Sorts_By_Second_Level_Descending()
    {
        // Arrange
        using IDocumentStore store = GetDocumentStore();

        using (IDocumentSession session = store.OpenSession())
        {
            session.Store(new Foo { OtherFoo = new OtherFoo { MyProperty = 2 } });
            session.Store(new Foo { OtherFoo = new OtherFoo { MyProperty = 1 } });
            session.Store(new Foo { OtherFoo = new OtherFoo { MyProperty = 3 } });
            session.SaveChanges();
        }

        // Sometimes we want to debug the test itself. This method redirects us to the studio
        // so that we can see if the code worked as expected (in this case, created two documents).
        // WaitForUserToContinueTheTest(store);

        using (IDocumentSession session = store.OpenSession())
        {
            // Act
            var result = session
                .Query<Foo>()
                .OrderBy("OtherFoo.MyProperty", SortDirection.Desc)
                .ToList();

            // Assert
            result[0].OtherFoo.MyProperty.Should().Be(3);
            result[1].OtherFoo.MyProperty.Should().Be(2);
            result[2].OtherFoo.MyProperty.Should().Be(1);
        }
    }

    public class Foo
    {
        public int MyProperty { get; set; }
        public OtherFoo OtherFoo { get; set; }
    }

    public class OtherFoo
    {
        public int MyProperty { get; set; }
    }
}
