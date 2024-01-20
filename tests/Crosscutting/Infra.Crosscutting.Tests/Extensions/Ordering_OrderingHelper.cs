using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Crosscutting.Extensions;
using Xunit;

namespace Goal.Infra.Crosscutting.Tests.Extensions;

public class Ordering_OrderingHelper
{
    [Fact]
    public void Returns_IOrderedQueryable()
    {
        // Arrange
        IQueryable<Foo> queryable = new List<Foo>().AsQueryable();

        // Act
        IOrderedQueryable<Foo> result = queryable.OrderBy("MyProperty", SortDirection.Asc);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeAssignableTo<IOrderedQueryable<Foo>>();
    }

    [Fact]
    public void Sorts_By_First_Level_Ascending()
    {
        // Arrange
        IQueryable<Foo> items = new List<Foo>()
        {
            new Foo { MyProperty = 3 },
            new Foo { MyProperty = 1 },
            new Foo { MyProperty = 2 }
        }.AsQueryable();

        // Act
        var result = items.OrderBy("MyProperty", SortDirection.Asc).ToList();

        // Assert
        result[0].MyProperty.Should().Be(1);
        result[1].MyProperty.Should().Be(2);
        result[2].MyProperty.Should().Be(3);
    }

    [Fact]
    public void Sorts_By_First_Level_Descending()
    {
        // Arrange
        IQueryable<Foo> items = new List<Foo>()
        {
            new Foo { MyProperty = 3 },
            new Foo { MyProperty = 1 },
            new Foo { MyProperty = 2 }
        }.AsQueryable();

        // Act
        var result = items.OrderBy("MyProperty", SortDirection.Desc).ToList();

        // Assert
        result[0].MyProperty.Should().Be(3);
        result[1].MyProperty.Should().Be(2);
        result[2].MyProperty.Should().Be(1);
    }

    [Fact]
    public void Sorts_By_Second_Level_Ascending()
    {
        // Arrange
        IQueryable<Foo> items = new List<Foo>()
        {
            new Foo { OtherFoo = new OtherFoo { MyProperty = 2 } },
            new Foo { OtherFoo = new OtherFoo { MyProperty = 1 } },
            new Foo { OtherFoo = new OtherFoo { MyProperty = 3 } }
        }.AsQueryable();

        // Act
        var result = items.OrderBy("OtherFoo.MyProperty", SortDirection.Asc).ToList();

        // Assert
        result[0].OtherFoo.MyProperty.Should().Be(1);
        result[1].OtherFoo.MyProperty.Should().Be(2);
        result[2].OtherFoo.MyProperty.Should().Be(3);
    }

    [Fact]
    public void Sorts_By_Second_Level_Descending()
    {
        // Arrange
        IQueryable<Foo> items = new List<Foo>()
        {
            new Foo { OtherFoo = new OtherFoo { MyProperty = 2 } },
            new Foo { OtherFoo = new OtherFoo { MyProperty = 1 } },
            new Foo { OtherFoo = new OtherFoo { MyProperty = 3 } }
        }.AsQueryable();

        // Act
        var result = items.OrderBy("OtherFoo.MyProperty", SortDirection.Desc).ToList();

        // Assert
        result[0].OtherFoo.MyProperty.Should().Be(3);
        result[1].OtherFoo.MyProperty.Should().Be(2);
        result[2].OtherFoo.MyProperty.Should().Be(1);
    }

    public class Foo
    {
        public int MyProperty { get; set; }
        public OtherFoo OtherFoo { get; set; } = null!;
    }

    public class OtherFoo
    {
        public int MyProperty { get; set; }
    }
}
