using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Collections;
using Goal.Seedwork.Infra.Crosscutting.Extensions;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Extensions
{
    public class Ordering_OrderingHelper
    {
        [Fact]
        public void Returns_IOrderedQueryable()
        {
            // Arrange
            IQueryable<MyEntity> queryable = new List<MyEntity>().AsQueryable();

            // Act
            IOrderedQueryable<MyEntity> result = queryable.OrderBy("MyProperty", SortDirection.Asc);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IOrderedQueryable<MyEntity>>();
        }

        [Fact]
        public void Sorts_By_First_Level_Ascending()
        {
            // Arrange
            IQueryable<MyEntity> items = new List<MyEntity>()
            {
                new MyEntity { MyProperty = 3 },
                new MyEntity { MyProperty = 1 },
                new MyEntity { MyProperty = 2 }
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
            IQueryable<MyEntity> items = new List<MyEntity>()
            {
                new MyEntity { MyProperty = 3 },
                new MyEntity { MyProperty = 1 },
                new MyEntity { MyProperty = 2 }
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
            IQueryable<MyEntity> items = new List<MyEntity>()
            {
                new MyEntity { MyOtherEntity = new MyOtherEntity { MyProperty = 2 } },
                new MyEntity { MyOtherEntity = new MyOtherEntity { MyProperty = 1 } },
                new MyEntity { MyOtherEntity = new MyOtherEntity { MyProperty = 3 } }
            }.AsQueryable();

            // Act
            var result = items.OrderBy("MyOtherEntity.MyProperty", SortDirection.Asc).ToList();

            // Assert
            result[0].MyOtherEntity.MyProperty.Should().Be(1);
            result[1].MyOtherEntity.MyProperty.Should().Be(2);
            result[2].MyOtherEntity.MyProperty.Should().Be(3);
        }

        [Fact]
        public void Sorts_By_Second_Level_Descending()
        {
            // Arrange
            IQueryable<MyEntity> items = new List<MyEntity>()
            {
                new MyEntity { MyOtherEntity = new MyOtherEntity { MyProperty = 2 } },
                new MyEntity { MyOtherEntity = new MyOtherEntity { MyProperty = 1 } },
                new MyEntity { MyOtherEntity = new MyOtherEntity { MyProperty = 3 } }
            }.AsQueryable();

            // Act
            var result = items.OrderBy("MyOtherEntity.MyProperty", SortDirection.Desc).ToList();

            // Assert
            result[0].MyOtherEntity.MyProperty.Should().Be(3);
            result[1].MyOtherEntity.MyProperty.Should().Be(2);
            result[2].MyOtherEntity.MyProperty.Should().Be(1);
        }
    }

    public class MyEntity
    {
        public int MyProperty { get; set; }
        public MyOtherEntity MyOtherEntity { get; set; }
    }

    public class MyOtherEntity
    {
        public int MyProperty { get; set; }
    }
}
