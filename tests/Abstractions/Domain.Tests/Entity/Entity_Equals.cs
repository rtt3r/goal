using FluentAssertions;
using Goal.Domain.Aggregates;
using Goal.Domain.Tests.Mocks;
using Xunit;

namespace Goal.Domain.Tests.Entity;

public class Entity_Equals
{
    private class TestEntity : Entity<int>
    {
        public TestEntity(int id)
        {
            Id = id;
        }
    }

    [Fact]
    public void Equals_NotEntity_ReturnsFalse()
    {
        //Given
        IEntity entity = new EntityTest("8309d707-91b4-4494-b3cc-dc5f349fa816");
        object obj = new();

        //When
        bool areEquals = entity.Equals(obj);

        //Then
        areEquals.Should().BeFalse();
    }

    [Fact]
    public void Equals_IntransientSameReference_ReturnsTrue()
    {
        //Given
        IEntity entity1 = new EntityTest("8309d707-91b4-4494-b3cc-dc5f349fa816");
        IEntity entity2 = entity1;

        //When
        bool areEquals = entity1.Equals(entity2);

        //Then
        areEquals.Should().BeTrue();
    }

    [Fact]
    public void Equals_LeftTransient_ReturnsFalse()
    {
        //Given
        IEntity entity1 = new EntityTest();
        IEntity entity2 = new EntityTest("8309d707-91b4-4494-b3cc-dc5f349fa816");

        //When
        bool areEquals = entity1.Equals(entity2);

        //Then
        areEquals.Should().BeFalse();
    }

    [Fact]
    public void Equals_RightTransient_ReturnsFalse()
    {
        //Given
        IEntity entity1 = new EntityTest("8309d707-91b4-4494-b3cc-dc5f349fa816");
        IEntity entity2 = new EntityTest();

        //When
        bool areEquals = entity1.Equals(entity2);

        //Then
        areEquals.Should().BeFalse();
    }

    [Fact]
    public void Equals_BothIntransient_ReturnsTrue()
    {
        //Given
        IEntity entity1 = new EntityTest("8309d707-91b4-4494-b3cc-dc5f349fa816");
        IEntity entity2 = new EntityTest("8309d707-91b4-4494-b3cc-dc5f349fa816");

        //When
        bool areEquals = entity1.Equals(entity2);

        //Then
        areEquals.Should().BeTrue();
    }

    [Fact]
    public void Equals_BothIntransient_ReturnsFalse()
    {
        //Given
        IEntity entity1 = new EntityTest("8309d707-91b4-4494-b3cc-dc5f349fa816");
        IEntity entity2 = new EntityTest("09927327-c55b-4c17-8171-ca32131f4357");

        //When
        bool areEquals = entity1.Equals(entity2);

        //Then
        areEquals.Should().BeFalse();
    }

    [Fact]
    public void Equals_BothTransientSameReference_ReturnsTrue()
    {
        //Given
        var entity1 = new EntityTest();
        EntityTest entity2 = entity1;

        //When
        bool areEquals = entity1.Equals(entity2);

        //Then
        areEquals.Should().BeTrue();
    }

    [Fact]
    public void Equals_SameObject_ReturnsTrue()
    {
        // Arrange
        var entity = new TestEntity(1);

        // Act
        bool result = entity.Equals(entity);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Equals_NullObject_ReturnsFalse()
    {
        // Arrange
        var entity = new TestEntity(1);

        // Act
        bool result = entity.Equals(null);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Equals_DifferentType_ReturnsFalse()
    {
        // Arrange
        var entity = new TestEntity(1);

        // Act
        bool result = entity.Equals("not an entity");

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
        bool result = entity1.Equals(entity2);

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
        bool result = entity1.Equals(entity2);

        // Assert
        result.Should().BeFalse();
    }
}
