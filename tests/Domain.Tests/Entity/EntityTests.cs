using FluentAssertions;
using Goal.Seedwork.Domain.Aggregates;
using Xunit;

namespace Goal.Seedwork.Domain.Tests.Entity
{
    public class EntityTests
    {
        private class TestEntity : Entity<int>
        {
            public TestEntity(int id)
            {
                Id = id;
            }
        }

        [Fact]
        public void Equals_SameObject_ReturnsTrue()
        {
            // Arrange
            var entity = new TestEntity(1);

            // Act
            var result = entity.Equals(entity);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_NullObject_ReturnsFalse()
        {
            // Arrange
            var entity = new TestEntity(1);

            // Act
            var result = entity.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_DifferentType_ReturnsFalse()
        {
            // Arrange
            var entity = new TestEntity(1);

            // Act
            var result = entity.Equals("not an entity");

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_SameTypeAndId_ReturnsTrue()
        {
            // Arrange
            var entity1 = new TestEntity(1);
            var entity2 = new TestEntity(1);

            // Act
            var result = entity1.Equals(entity2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_SameTypeDifferentId_ReturnsFalse()
        {
            // Arrange
            var entity1 = new TestEntity(1);
            var entity2 = new TestEntity(2);

            // Act
            var result = entity1.Equals(entity2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void GetHashCode_SameTypeAndId_ReturnsSameHash()
        {
            // Arrange
            var entity1 = new TestEntity(1);
            var entity2 = new TestEntity(1);

            // Act
            var hash1 = entity1.GetHashCode();
            var hash2 = entity2.GetHashCode();

            // Assert
            hash1.Should().Be(hash2);
        }

        [Fact]
        public void OperatorEquals_SameObjects_ReturnsTrue()
        {
            // Arrange
            var entity = new TestEntity(1);

            // Act
            var result = entity == entity;

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

        [Fact]
        public void OperatorNotEquals_SameObjects_ReturnsFalse()
        {
            // Arrange
            var entity = new TestEntity(1);

            // Act
            var result = entity != entity;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void OperatorNotEquals_DifferentObjects_ReturnsTrue()
        {
            // Arrange
            var entity1 = new TestEntity(1);
            var entity2 = new TestEntity(2);

            // Act
            var result = entity1 != entity2;

            // Assert
            result.Should().BeTrue();
        }
    }
}
