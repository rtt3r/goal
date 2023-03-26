using System;
using FluentAssertions;
using Goal.Seedwork.Domain.Tests.Mocks;
using Xunit;

namespace Goal.Seedwork.Domain.Tests.Entity
{
    public class Entity_EqualityOperator
    {
        private class TestEntity : Domain.Aggregates.Entity<int>
        {
            public TestEntity(int id)
            {
                Id = id;
            }
        }

        [Fact]
        public void OperatorEquals_NotNullObjects_ReturnsFalse()
        {
            //Given
            var obj1 = new EntityTest(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));
            var obj2 = new EntityTest(Guid.Parse("9ae0bc81-9586-4c47-b695-b59a6f584bd2"));

            //When
            bool areEquals = obj1 == obj2;

            //Then
            areEquals.Should().BeFalse();
        }

        [Fact]
        public void OperatorEquals_NotNullObjects_ReturnsTrue()
        {
            //Given
            var obj1 = new EntityTest(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));
            var obj2 = new EntityTest(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));

            //When
            bool areEquals = obj1 == obj2;

            //Then
            areEquals.Should().BeTrue();
        }

        [Fact]
        public void OperatorEquals_NullLeftObjects_ReturnsFalse()
        {
            //Given
            EntityTest obj1 = null;
            var obj2 = new EntityTest(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));

            //When
            bool areEquals = obj1 == obj2;

            //Then
            areEquals.Should().BeFalse();
        }

        [Fact]
        public void OperatorEquals_NullRightObjects_ReturnsFalse()
        {
            //Given
            var obj1 = new EntityTest(Guid.Parse("8309d707-91b4-4494-b3cc-dc5f349fa816"));
            EntityTest obj2 = null;

            //When
            bool areEquals = obj1 == obj2;

            //Then
            areEquals.Should().BeFalse();
        }

        [Fact]
        public void OperatorEquals_NullBothObjects_ReturnsTrue()
        {
            //Given
            EntityTest obj1 = null;
            EntityTest obj2 = null;

            //When
            bool areEquals = obj1 == obj2;

            //Then
            areEquals.Should().BeTrue();
        }

        [Fact]
        public void OperatorEquals_SameObjects_ReturnsTrue()
        {
            // Arrange
            var entity = new TestEntity(1);

            // Act
#pragma warning disable CS1718 // Comparison made to same variable
            var result = entity == entity;
#pragma warning restore CS1718 // Comparison made to same variable

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void OperatorEquals_DifferentObjects_ReturnsFalse()
        {
            // Arrange
            var entity1 = new TestEntity(1);
            var entity2 = new TestEntity(2);

            // Act
            var result = entity1 == entity2;

            // Assert
            result.Should().BeFalse();
        }
    }
}
