using FluentAssertions;
using Goal.Seedwork.Domain.Tests.Mocks;
using Xunit;

namespace Goal.Seedwork.Domain.Tests.ValueObjects
{
    public class Entity_EqualityOperator
    {
        [Fact]
        public void ReturnFalseGivenNotNullObjects()
        {
            //Given
            var obj1 = new ValueObject1 { Id = 1, Value = "value" };
            var obj2 = new ValueObject1 { Id = 2, Value = "value1" };

            //When
            bool areEquals = obj1 == obj2;

            //Then
            areEquals.Should().BeFalse();
        }

        [Fact]
        public void ReturnTrueGivenNotNullObjects()
        {
            //Given
            var obj1 = new ValueObject1 { Id = 1, Value = "value" };
            var obj2 = new ValueObject1 { Id = 1, Value = "value" };

            //When
            bool areEquals = obj1 == obj2;

            //Then
            areEquals.Should().BeTrue();
        }

        [Fact]
        public void ReturnFalseGivenNullLeftObjects()
        {
            //Given
            ValueObject1 obj1 = null;
            var obj2 = new ValueObject1 { Id = 2, Value = "value1" };

            //When
            bool areEquals = obj1 == obj2;

            //Then
            areEquals.Should().BeFalse();
        }

        [Fact]
        public void ReturnFalseGivenNullRightObjects()
        {
            //Given
            var obj1 = new ValueObject1 { Id = 1, Value = "value" };
            ValueObject1 obj2 = null;

            //When
            bool areEquals = obj1 == obj2;

            //Then
            areEquals.Should().BeFalse();
        }

        [Fact]
        public void ReturnTrueGivenNullBothObjects()
        {
            //Given
            ValueObject1 obj1 = null;
            ValueObject1 obj2 = null;

            //When
            bool areEquals = obj1 == obj2;

            //Then
            areEquals.Should().BeTrue();
        }
    }
}
